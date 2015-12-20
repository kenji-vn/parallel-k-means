using System;
using System.Collections.Generic;

namespace KMeans
{
    /// <summary>
    /// The distance function defining the distance between two inputs of type 'T'.  Common distance functions used are the Euclidean distance or Manhattan distance.
    /// </summary>
    /// <typeparam name="T">The type to compare.</typeparam>
    /// <param name="input1">The first input.</param>
    /// <param name="input2">The second input.</param>
    /// <returns>A double representing the distance between the two inputs.</returns>
    public delegate double DistanceDelegate<T>( T input1, T input2 );

    /// <summary>
    /// The centroid function defining the location of the centroid of a given collection of type 'T'.  Often the mean of the elements, but could also be median or other centroid determining functions.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="collection">The collection of type 'T'.</param>
    /// <returns>Returns the centroid of the collection as a type 'T'.</returns>
    public delegate T CentroidDelegate<T>( IEnumerable<T> collection );

    /// <summary>
    /// The base k-Means class.
    /// </summary>
    /// <typeparam name="T">The type of the item to cluster.</typeparam>
    public abstract class BaseKMeans<T>
    {
        /// <summary>
        /// Performs a single step of the k-means algorithm.
        /// </summary>
        /// <param name="resInput">The current result set</param>
        internal abstract void DoStep(KMeansResult<T> resInput);

        /// <summary>
        /// Generates the inital set of Centroids from the given input.  Implementation specific, often a random subset of input.
        /// </summary>
        /// <param name="k">The number of clusters</param>
        /// <param name="inputs">The imputs</param>
        /// <returns>An array of 'T' of length 'k', containing the initial set of centroids.</returns>
        protected abstract T[] GenerateInitalCentroids(int k, T[] inputs);

        /// <summary>
        /// The distance function defining the distance between two inputs of type 'T'.  Common distance functions used are the Euclidean distance or Manhattan distance.
        /// </summary>
        protected DistanceDelegate<T> Distance { get; private set; }

        /// <summary>
        /// The centroid function defining the location of the centroid of a given collection of type 'T'.
        /// </summary>
        protected CentroidDelegate<T> Centroid { get; private set; }

        /// <summary>
        /// Base kMeans constructor.
        /// </summary>
        /// <param name="distance">The distance delegate.</param>
        /// <param name="centroid">The centroid delegate.</param>
        protected BaseKMeans( DistanceDelegate<T> distance, CentroidDelegate<T> centroid )
        {
            this.Distance = distance;
            this.Centroid = centroid;            
        }

        /// <summary>
        /// Performs a single step of the KMeans Algorithm on the specified input.
        /// </summary>
        /// <param name="resInput">The input set, often the result of the prior step.</param>
        internal void Step( KMeansResult<T> resInput )
        {
            resInput.Iterations++;

            DoStep( resInput );
        }

        /// <summary>
        /// Runs the k-Means algorithm on the specified input set with 'k' clusters.
        /// </summary>
        /// <param name="k">The number of clusters.</param>
        /// <param name="inputs">The input set.  Size of this collection should be greater than k.</param>
        /// <returns>The means of the clusters.</returns>
        public T[] Run( int k, T[] inputs )
        {
            return this.Run( k, inputs, GenerateInitalCentroids( k, inputs ) );
        }

        /// <summary>
        /// Runs the k-Means algorithm on the specified input set with 'k' clusters.
        /// </summary>
        /// <param name="k">The number of clusters.</param>
        /// <param name="inputs">The input set.  Size of this collection should be greater than k.</param>
        /// <param name="initialMeans">The inital means to use.</param>
        /// <returns>The centroids of the clusters.</returns>
        /// <returns></returns>
        public T[] Run( int k, T[] inputs, T[] initialMeans )
        {
            if (k != initialMeans.Length)
            {
                throw new ArgumentOutOfRangeException("initialMeans", "Initial Means count must be equal to 'k'");
            }

            if (k <= 0 || k >= inputs.Length)
            {
                throw new ArgumentOutOfRangeException("k", "'k' must be greater than zero and less than the number of inputs");
            }

            KMeansResult<T> result = new KMeansResult<T>( k, inputs, initialMeans );

            while( !result.Converged )
            {
                Step( result );
            }

            /*
            if (result.Converged)
            {
                System.Diagnostics.Debug.WriteLine("Converges after {0} iterations", result.Iterations);
            }
             * */

            return result.Result;
        }
    }
}
