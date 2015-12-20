using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KMeans
{
    /// <summary>
    /// A Parallel k-Means implementation.  Uses the .NET TPL and does dynamic load balancing and partitioning.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ParallelKMeans<T> : BaseKMeans<T>
    {
        public ParallelKMeans( DistanceDelegate<T> distance, CentroidDelegate<T> centroid )
            : base( distance, centroid )
        {
        }

        protected override T[] GenerateInitalCentroids( int k, T[] inputs )
        {
            Random r = new Random();

            //General the Initial Centroids ... in PARALLEL!
            return inputs.AsParallel().Distinct().OrderBy( x => r.Next() ).Take( k ).ToArray();
        }

        internal override void DoStep( KMeansResult<T> resInput )
        {
            if( resInput.Converged )
                return;

            //A note on Parallelizing this: We have the option to use Partitioners for the smaller loop bodies (like init) but it really isn't worth it...
            //The TPL Automagically does dynamic load balancing and partitioning of stuffs, it doesn't just partition it all up evenly by processor at the beginning.
            
            //Note on ConcurrentBag: It's optimized for concurrency so that locking within the 'hyperobject' is reduced.  Each thread has it's own bag within the bag that is added to first
            ConcurrentBag<T>[] inputMap = new ConcurrentBag<T>[resInput.K];

            Parallel.For( 0, resInput.K,    //For each mean...
                    ( i ) =>
                    {
                        inputMap[i] = new ConcurrentBag<T>();   //Init -- might as well do it in parallel.  It's faster to just do this, let GC do it's thing later.
                    } );




            Parallel.For( 0, resInput.Input.Length, //For each Input...
                ( i ) =>
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
                            break;  //Exit the For loop, there's nothing else that can be closer.
                    }

                    ConcurrentBag<T> myInputList = inputMap[meanIndex];

                    myInputList.Add( curInput );
                    //Find the closest mean, add it to the mean's list of associated inputs.
                } );
            



            int totalEpsilon = 0;

            //For each set of inputs within each cluster...
            Parallel.For<int>( 0, resInput.K, () => 0,  // For each mean...
                ( i, _loopState, epsilonSubtotal ) =>
                {
                    ConcurrentBag<T> myInputList = inputMap[i];

                    T newCentroid = Centroid( myInputList );
                    T lastCentroid = resInput.Result[i];

                    double epsilon = Distance( newCentroid, lastCentroid ); //The distance it moved from the last point

                    if( epsilon > 0 )
                        epsilonSubtotal++;

                    resInput.Result[i] = newCentroid;

                    return epsilonSubtotal;  //We Create the subtotal so that we don't have to have EACH update of the total touch the hyper object.  only touch the hyperobject when done with each thread.

                },
                ( epsilonSubtotal ) =>
                {
                    Interlocked.Add( ref totalEpsilon, epsilonSubtotal );   //We are fnished with our specific thread.  Add our epsilon Subtotal to the total Epsilon count.
                }
             );

            resInput.CentroidMovementCount = totalEpsilon;
            resInput.ResultToInputMap = inputMap;
        }
    }
}
