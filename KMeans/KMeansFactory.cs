using System;
using System.Collections.Generic;
using System.Linq;

namespace KMeans
{
    /// <summary>
    /// An n-Dimentional K kMeans Factory, with the number of dimensions being the vector of doubles.  Uses euclidean distance and mean as distance and centroid functions respectively.
    /// </summary>
    public static class KMeansFactory
    {
        #region Distance & Centroid Function for double[]

       public static double EuclideanDistace(double[] d1, double[] d2)    //distance function
        {
            double dist = 0;

            for (int i = 0; i < d1.Length; i++)
            {
                dist += Math.Pow(d1[i] - d2[i], 2);
            }

            return dist;
        }

        //Make a centroid from the collection of stuffs.  We use a Mean here, but there's nothing says we can't use the Median.
        public static double[] MeanAsCentroid(IEnumerable<double[]> collection)
        {
            int vectorSize = collection.First().Length;    //length of first array/vector in the list

            double[] newCentroid = new double[vectorSize];

            int count = 0;

            foreach (double[] curVector in collection)
            {
                for (int i = 0; i < vectorSize; i++)
                {
                    newCentroid[i] += curVector[i];
                }

                count++;
            }

            for (int i = 0; i < vectorSize; i++)
            {
                newCentroid[i] /= count;
            }

            return newCentroid;
        }

        #endregion

        public static BaseKMeans<double[]> CreateParallel()
        {
            BaseKMeans<double[]> nDimkMeans = new ParallelKMeans<double[]>(EuclideanDistace, MeanAsCentroid);

            return nDimkMeans;
        }

        public static BaseKMeans<double[]> CreateSequential()
        {
            BaseKMeans<double[]> nDimkMeans = new SequentialKMeans<double[]>(EuclideanDistace, MeanAsCentroid);

            return nDimkMeans;
        }
    }
}
