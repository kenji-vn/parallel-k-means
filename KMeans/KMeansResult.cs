using System;
using System.Collections.Generic;

namespace KMeans
{
    /// <summary>
    /// Contains the results of each step of the kMeans algorithm
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class KMeansResult<T>
    {
        internal KMeansResult( int k, T[] inputs, T[] currentMeans )
        {
            this.Iterations = 0;
            this.CentroidMovementCount = int.MaxValue;

            this.K = k;

            this.Input = new T[inputs.Length];
            this.Result = new T[currentMeans.Length];

            inputs.CopyTo( this.Input, 0 );
            currentMeans.CopyTo( this.Result, 0 );
        }

        internal KMeansResult(KMeansResult<T> result)
            : this( result.K, result.Input, result.Result )
        {
        }

        /// <summary>
        /// The number of clusters/means
        /// </summary>
        internal int K { get; private set; }

        /// <summary>
        /// The input set of items to cluster.
        /// </summary>
        internal T[] Input { get; private set; }

        /// <summary>
        /// The resulting means.
        /// </summary>
        internal T[] Result { get; private set; }

        /// <summary>
        /// The number of Centroids that have moved since the last step.
        /// </summary>
        internal int CentroidMovementCount { get; set; }

        /// <summary>
        /// The number of Iterations.
        /// </summary>
        internal int Iterations { get; set; }

        /// <summary>
        /// The clustered items for each input.  For example, the IEnumerable returned for ResultToInputMap[0] will contain all the points which have the 0th input as the centroid.
        /// </summary>
        internal IEnumerable<T>[] ResultToInputMap { get; set; }

        /// <summary>
        /// The convergence state.
        /// </summary>
        internal bool Converged { get { return (this.CentroidMovementCount < 1); } }
    }
}
