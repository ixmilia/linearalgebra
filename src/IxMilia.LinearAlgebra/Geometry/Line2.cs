using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public static class Line2
    {
        public static Line2<int> CreateInt32(int a, int b, int c) => new Line2<int>(Int32AlgebraicComputer.Instance, a, b, c);

        public static Line2<double> CreateDouble(double a, double b, double c) => new Line2<double>(DoubleAlgebraicComputer.Instance, a, b, c);

        public static Vector2<T> Cross<T>(Line2<T> l1, Line2<T> l2)
        {
            if (l1 == null)
            {
                throw new ArgumentNullException(nameof(l1));
            }

            if (l2 == null)
            {
                throw new ArgumentNullException(nameof(l2));
            }

            return Matrix.Cross(l1.Computer, l1.A, l1.B, l1.C, l2.A, l2.B, l2.C).AsVector2();
        }

        public static Vector2<T> Intersection<T>(Line2<T> l1, Line2<T> l2)
        {
            var result = Cross(l1, l2);
            result.TryNormalize();
            return result;
        }

        public static Line2<T> FromPoints<T>(Vector2<T> v1, Vector2<T> v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return Matrix.Cross(v1.Computer, v1.X, v1.Y, v1.W, v2.X, v2.Y, v2.W).AsLine2();
        }

        public static Line2<T> XEquals<T>(IAlgebraicComputer<T> computer, T xIntercept)
        {
            return new Line2<T>(computer, computer.Negate(computer.One), computer.Zero, xIntercept);
        }
        
        public static Line2<T> YEquals<T>(IAlgebraicComputer<T> computer, T yIntercept)
        {
            return new Line2<T>(computer, computer.Zero, computer.Negate(computer.One), yIntercept);
        }

        public static Line2<T> FromSlopeAndYIntercept<T>(IAlgebraicComputer<T> computer, T slope, T yIntercept)
        {
            // y = slope * x + yIntercept
            //   V
            // -slope * x + 1 y + -yIntercept = 0
            return new Line2<T>(computer, computer.Negate(slope), computer.One, computer.Negate(yIntercept));
        }

        public static Line2<T> CreateInfinity<T>(IAlgebraicComputer<T> computer) => new Line2<T>(computer, computer.Zero, computer.Zero, computer.One);
    }
}
