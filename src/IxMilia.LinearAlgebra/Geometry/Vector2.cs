using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public static class Vector2
    {
        public static Vector2<int> CreateInt32(int x, int y) => new Vector2<int>(Int32AlgebraicComputer.Instance, x, y);
        public static Vector2<int> CreateInt32(int x, int y, int w) => new Vector2<int>(Int32AlgebraicComputer.Instance, x, y, w);

        public static Vector2<double> CreateDouble(double x, double y) => new Vector2<double>(DoubleAlgebraicComputer.Instance, x, y);
        public static Vector2<double> CreateDouble(double x, double y, double w) => new Vector2<double>(DoubleAlgebraicComputer.Instance, x, y, w);

        public static Vector2<T> Cross<T>(Vector2<T> v1, Vector2<T> v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return Matrix.Cross(v1.Computer, v1.X, v1.Y, v1.W, v2.X, v2.Y, v2.W).AsVector2();
        }

        public static Vector2<T> CreateInfinity<T>(IAlgebraicComputer<T> computer) => new Vector2<T>(computer, computer.Zero, computer.Zero, computer.Zero); // TODO: should this be 0, 0, 1?

        public static Vector2<T> CreateZeroVector<T>(IAlgebraicComputer<T> computer) => new Vector2<T>(computer, computer.Zero, computer.Zero);

        public static Matrix<T> CreateScale<T>(IAlgebraicComputer<T> computer, T scale)
        {
            return CreateScale(computer, scale, scale);
        }

        public static Matrix<T> CreateScale<T>(IAlgebraicComputer<T> computer, T sx, T sy)
        {
            return new Matrix<T>(computer, 3, 3,
                sx, computer.Zero, computer.Zero,
                computer.Zero, sy, computer.Zero,
                computer.Zero, computer.Zero, computer.One);
        }

        public static Matrix<T> CreateTranslate<T>(IAlgebraicComputer<T> computer, T dx, T dy)
        {
            return new Matrix<T>(computer, 3, 3,
                computer.One, computer.Zero, dx,
                computer.Zero, computer.One, dy,
                computer.Zero, computer.Zero, computer.One);
        }
    }
}
