using System;
using System.Collections.Generic;
using System.Linq;
using KMeans;

namespace KMeansExample
{
    /// <summary>
    /// An Exmaple class to demonstrate a new kMeans Factory for our specific implementation for clustering upon a custom Class/struct 
    /// </summary>
    public static class PointKMeansFactory
    {
        public static BaseKMeans<Point> Create( bool parallel )
        {
            CentroidDelegate<Point> pointCentroidFunc =
                delegate(IEnumerable<Point> collection)
                {
                    double sumX = 0;
                    double sumY = 0;

                    int count = 0;

                    foreach (Point curPoint in collection)
                    {
                        sumX += curPoint.X;
                        sumY += curPoint.Y;

                        count++;
                    }

                    Point newCentroid = new Point(sumX / count, sumY / count);

                    return newCentroid;
                };

            BaseKMeans<Point> pointKMeans;

            if (parallel)
            {
                pointKMeans = new ParallelKMeans<Point>((p1, p2) => p1.DistanceSquared(p2), pointCentroidFunc);
            }
            else
            {
                pointKMeans = new SequentialKMeans<Point>((p1, p2) => p1.DistanceSquared(p2), pointCentroidFunc);
            }

            return pointKMeans;
        }
    }
}
