using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Line2 : Matrix
    {
        public double A
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public double B
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public double C
        {
            get { return this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public bool IsInfinity => A == 0.0 && B == 0.0 && C == 1.0;

        private Line2()
            : base(3, 1)
        {
        }

        public Line2(double a, double b, double c)
            : this()
        {
            if (a == 0.0 && b == 0.0 && c == 0.0)
            {
                throw new InvalidOperationException("The specified line is not valid");
            }

            A = a;
            B = b;
            C = c;
        }

        public bool ContainsPoint(Vector2 point)
        {
            var scalar = (Transpose * point).Scalar;
            return scalar == 0.0;
        }

        public bool IsParallelTo(Line2 other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Intersection(this, other).IsInfinity;
        }

        public static Vector2 Cross(Line2 l1, Line2 l2)
        {
            if (l1 == null)
            {
                throw new ArgumentNullException(nameof(l1));
            }

            if (l2 == null)
            {
                throw new ArgumentNullException(nameof(l2));
            }

            return Cross(l1.A, l1.B, l1.C, l2.A, l2.B, l2.C).AsVector2();
        }

        public static Vector2 Intersection(Line2 l1, Line2 l2)
        {
            var result = Cross(l1, l2);
            result.TryNormalize();
            return result;
        }

        public static Line2 FromPoints(Vector2 v1, Vector2 v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return Cross(v1.X, v1.Y, v1.W, v2.X, v2.Y, v2.W).AsLine2();
        }

        public static Line2 XEquals(double xIntercept)
        {
            return new Line2(-1, 0, xIntercept);
        }

        public static Line2 YEquals(double yIntercept)
        {
            return new Line2(0, -1, yIntercept);
        }

        public static Line2 FromSlopeAndYIntercept(double slope, double yIntercept)
        {
            // y = slope * x + yIntercept
            //   V
            // -slope * x + 1 y + -yIntercept = 0
            return new Line2(-slope, 1, -yIntercept);
        }

        public static Line2 Infinity => new Line2(0, 0, 1);
    }
}
