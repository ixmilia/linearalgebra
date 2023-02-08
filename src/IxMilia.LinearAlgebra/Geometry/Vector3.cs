using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public static class Vector3
    {
        public static Vector3<int> CreateInt32(int x, int y, int z) => new Vector3<int>(Int32AlgebraicComputer.Instance, x, y, z);
        public static Vector3<int> CreateInt32(int x, int y, int z, int w) => new Vector3<int>(Int32AlgebraicComputer.Instance, x, y, z, w);
        public static Vector3<double> CreateDouble(int x, int y, int z) => new Vector3<double>(DoubleAlgebraicComputer.Instance, x, y, z);
        public static Vector3<double> CreateDouble(int x, int y, int z, int w) => new Vector3<double>(DoubleAlgebraicComputer.Instance, x, y, z, w);

        public static Vector3<T> FromPlanes<T>(Plane<T> p1, Plane<T> p2, Plane<T> p3)
        {
            var computer = p1.Computer;
            var ls = new LinearSystem<T>(
                new Matrix<T>(computer, 3, 3,
                    p1.A, p1.B, p1.C,
                    p2.A, p2.B, p2.C,
                    p3.A, p3.B, p3.C),
                new Matrix<T>(computer, 3, 1,
                    p1.D,
                    p2.D,
                    p3.D));
            var result = ls.Solve();
            return result?.AsVector3();
        }

        public static Vector3<T> CreateInfinity<T>(IAlgebraicComputer<T> computer) => new Vector3<T>(computer, computer.Zero, computer.Zero, computer.Zero, computer.Zero);

        public static Vector3<T> CreateZeroVector<T>(IAlgebraicComputer<T> computer) => new Vector3<T>(computer, computer.Zero, computer.Zero, computer.Zero);

        public static Matrix<T> CreateScale<T>(IAlgebraicComputer<T> computer, T scale)
        {
            return CreateScale(computer, scale, scale, scale);
        }

        public static Matrix<T> CreateScale<T>(IAlgebraicComputer<T> computer, T sx, T sy, T sz)
        {
            return new Matrix<T>(computer, 4, 4,
                sx, computer.Zero, computer.Zero, computer.Zero,
                computer.Zero, sy, computer.Zero, computer.Zero,
                computer.Zero, computer.Zero, sz, computer.Zero,
                computer.Zero, computer.Zero, computer.Zero, computer.One);
        }

        public static Matrix<T> CreateTranslate<T>(IAlgebraicComputer<T> computer, T dx, T dy, T dz)
        {
            return new Matrix<T>(computer, 4, 4,
                computer.One, computer.Zero, computer.Zero, dx,
                computer.Zero, computer.One, computer.Zero, dy,
                computer.Zero, computer.Zero, computer.One, dz,
                computer.Zero, computer.Zero, computer.Zero, computer.One);
        }

        public static Vector3<T> Cross<T>(Vector3<T> v1, Vector3<T> v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return Matrix.Cross(v1.Computer, v1.X, v1.Y, v1.Z, v2.X, v2.Y, v2.Z).AsVector3();
        }
    }
}
