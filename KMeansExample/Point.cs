using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KMeansExample
{
    public struct Point
    {
        public Point( double x, double y )
        {
            this.X = x;
            this.Y = y;
        }

        public double X;
        public double Y;

        public double DistanceSquared( Point p )
        {
            double xDelt = this.X - p.X;
            double yDelt = this.Y - p.Y;

            return (xDelt * xDelt) + (yDelt * yDelt);
        }

        public override string ToString()
        {
            return String.Format( "X:{0:0.00}, Y:{1:0.00}", this.X, this.Y );
        }

        public override bool Equals( object obj )
        {
            if( obj is Point )
            {
                Point p = (Point)obj;

                bool res = (this.X == p.X && this.Y == p.Y);
                return res;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }
    }
}
