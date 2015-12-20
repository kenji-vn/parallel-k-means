using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using KMeans;


namespace KMeansExample
{
    /// <summary>
    /// This is an alternate Parallel Implementation of the kMeans Algorithm.  This uses static partitioning, as opposed to the normal divide/conquering dynamic partitioning
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParallelImplStaticPartitioning<T> : BaseKMeans<T>
    {
        public ParallelImplStaticPartitioning( DistanceDelegate<T> distance, CentroidDelegate<T> centroid )
            : base( distance, centroid )
        {
        }

        protected override T[] GenerateInitalCentroids(int k, T[] inputs)
        {
            Random r = new Random();

            //in PARALLEL!
            return inputs.AsParallel().Distinct().OrderBy( x => r.Next() ).Take( k ).ToArray();
        }

        internal override void DoStep( KMeansResult<T> resInput )
        {
            if( resInput.Converged )
                return;

            //A note on Parallelizing this: We COULD use Partitioners for the smaller loop bodies (like init) but it may not be worth it...
            //The TPL Automagically does dynamic load balancing and partitioning of stuffs, it doesn't just partition it all up evenly by processor at the beginning.

            ConcurrentBag<T>[] inputMap = new ConcurrentBag<T>[resInput.K];//resInput.ResultToInputMap as ConcurrentBag<T>[];
            //Note on ConcurrentBag: It's optimized for concurrency so that locking within the 'hyperobject' is reduced.  Each thread has it's own bag within the bag that is added to first


            var inputMapPartitioner = Partitioner.Create( 0, resInput.K );    //Create a partitioner WITHOUT load balancing.

            Parallel.ForEach( inputMapPartitioner, ( range, _loopstate ) =>
            {
                for( int i = range.Item1; i < range.Item2; i++ )
                {
                    inputMap[i] = new ConcurrentBag<T>();   //Init -- might as well do it in parallel.  It's faster to just do this, let GC do it's thing later.
                }

            } );


            var inputPartitioner = Partitioner.Create( 0, resInput.Input.Length );    //Create a partitioner WITHOUT load balancing, we assume near-even processing time for each set of stuffs

            Parallel.ForEach( inputPartitioner, ( range, _loopstate ) =>
            {
                for( int i = range.Item1; i < range.Item2; i++ )
                {
                    T curInput = resInput.Input[i];


                    double minDist = double.MaxValue;
                    int meanIndex = -1;

                    for( int j = 0; j < resInput.K; j++ )  //This loop finds the closest centroid to each input.
                    {
                        double curDist = Distance( curInput, resInput.Result[j] );

                        if( curDist < minDist )
                        {
                            minDist = curDist;
                            meanIndex = j;
                        }

                        if( curDist == 0 )
                            break;  //Exit the For loop, there's nothing else that can be closer.  //TODO: Question: If later implementing a custom distance function, and it retursn negative... assert?
                    }

                    ConcurrentBag<T> myInputList = inputMap[meanIndex];// resInput.ResultToInputMap[meanIndex];

                    myInputList.Add( curInput );
                    //Find the closest mean, add it to the mean's list of associated inputs.
                }
            } );



            int totalEpsilon = 0;

            inputMapPartitioner = Partitioner.Create( 0, resInput.K );    //Create a partitioner WITHOUT load balancing.

            //For each set of inputs within each cluster...
            Parallel.ForEach( inputMapPartitioner, () => 0,  // For each mean...
                ( range, _loopState, epsilonSubtotal ) =>
                {
                    for( int i = range.Item1; i < range.Item2; i++ )
                    {
                        ConcurrentBag<T> myInputList = inputMap[i]; // resInput.ResultToInputMap[i];

                        T newCentroid = Centroid( myInputList );
                        T lastCentroid = resInput.Result[i];

                        double epsilon = Distance( newCentroid, lastCentroid ); //The distance it moved from the last point

                        //epsilonSubtotal += (int)epsilon;// Cast as int to truncate.  If things are moving less than 1, don't worry 'bout 'em.
                        if( epsilon > 0 )
                            epsilonSubtotal++;


                        resInput.Result[i] = newCentroid;
                    }

                    return epsilonSubtotal;  //We Create the subtotal so that we don't have to have EACH update of the total touch the hyper object.  only touch the hyperobject when done with each thread.

                },
                ( epsilonSubtotal ) =>
                {
                    Interlocked.Add( ref totalEpsilon, epsilonSubtotal );
                }
             );

            resInput.CentroidMovementCount = totalEpsilon;
            resInput.ResultToInputMap = inputMap;
        }
    }
}
