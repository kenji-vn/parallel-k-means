using System;
using System.Collections.Generic;
using System.Linq;

namespace KMeans
{
    /// <summary>
    /// A Sequential k-Means implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequentialKMeans<T> : BaseKMeans<T>
    {
        public SequentialKMeans( DistanceDelegate<T> distance, CentroidDelegate<T> centroid )
            : base( distance, centroid )
        {
        }

        protected override T[] GenerateInitalCentroids(int k, T[] inputs)
        {
            Random r = new Random();

            return inputs.Distinct().OrderBy( x => r.Next() ).Take( k ).ToArray();
        }

        internal override void DoStep( KMeansResult<T> resInput )
        {
            if( resInput.Converged )
                return;

            LinkedList<T>[] inputMap = new LinkedList<T>[resInput.K];

            for( int i = 0; i < resInput.K; i++ )
            {
                inputMap[i] = new LinkedList<T>();   //Init the lists.    (Let the old ones be GC'd later, whenever)
            }




            for( int i = 0; i < resInput.Input.Length; i++ )
            {
                T curInput = resInput.Input[i];
                double minDist = double.MaxValue;
                int meanIndex = -1;

                for( int j = 0; j < resInput.K; j++ )  //This loop finds the closest centroid to each input. // Note: K == CurrentMeans.Length
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

                LinkedList<T> myInputList = inputMap[meanIndex];

                myInputList.AddLast( curInput );
                //Find the closest mean, add it to the mean's list of associated inputs.
            }





            int totalEpsilon = 0;

            for( int i = 0; i < resInput.K; i++ )   //For each set of  inputs within each cluster...
            {
                LinkedList<T> myInputList = inputMap[i];

                T newCentroid = Centroid( myInputList );
                T lastCentroid = resInput.Result[i];

                double epsilon = Distance( newCentroid, lastCentroid ); //The distance it moved from the last point

                if( epsilon > 0 )
                    totalEpsilon++;

                resInput.Result[i] = newCentroid;
            }



            resInput.CentroidMovementCount = totalEpsilon;
            resInput.ResultToInputMap = inputMap;
        }
    }
}
