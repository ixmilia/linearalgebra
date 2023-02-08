using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public static class Plane
    {
        public static Plane<int> CreateInt32(int a, int b, int c, int d) => new Plane<int>(Int32AlgebraicComputer.Instance, a, b, c, d);
        public static Plane<double> CreateDouble(double a, double b, double c, double d) => new Plane<double>(DoubleAlgebraicComputer.Instance, a, b, c, d);

        public static Plane<T> FromNormalAndDistance<T>(Vector3<T> normal, T distFromOrigin)
        {
            if (normal == null)
            {
                throw new ArgumentNullException(nameof(normal));
            }

            if (normal.IsInfinity)
            {
                throw new ArgumentOutOfRangeException(nameof(normal), "Unable to create a plane from the infinity vector");
            }

            var computer = normal.Computer;
            return new Plane<T>(computer, computer.Divide(normal.X, normal.W), computer.Divide(normal.Y, normal.W), computer.Divide(normal.Z, normal.W), distFromOrigin);
        }

        public static Plane<T> FromPoints<T>(Vector3<T> p1, Vector3<T> p2, Vector3<T> p3)
        {
            var edge1 = p2 - p1;
            var edge2 = p3 - p1;
            var normal = Vector3.Cross(edge1, edge2);

            // a*(x-x0) + b*(y-y0) + c*(z-z0) = 0

            // ax - ax0 + by - by0 + cz - cz0 = 0

            // ax + by + cz = ax0 + by0 + cz0

            // d = -(ax0 + by0 + cz0)

            var computer = p1.Computer;
            var x = computer.Multiply(normal.X, p1.X);
            var y = computer.Multiply(normal.Y, p1.Y);
            var z = computer.Multiply(normal.Z, p1.Z);
            var d = computer.Negate(computer.Add(x, y, z));
            return FromNormalAndDistance(normal, d);
        }
    }
}
