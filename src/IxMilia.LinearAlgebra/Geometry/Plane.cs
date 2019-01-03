using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Plane : Matrix
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

        public double D
        {
            get { return this[3, 0]; }
            set { this[3, 0] = value; }
        }

        public Vector3 Normal
        {
            get
            {
                var normal = new Vector3(A, B, C, D);
                normal.TryNormalize();
                return normal;
            }
        }

        public double Norm => Normal.Length;

        public double DistsanceFromOrigin
        {
            get
            {
                var norm = Norm;
                if (norm == 0.0)
                {
                    throw new InvalidOperationException("The infinity plane has no distance from the origin");
                }

                return D / norm;
            }
        }

        public Plane()
            : base(4, 1)
        {
        }

        public Plane(double a, double b, double c, double d)
            : this()
        {
            if (a == 0.0 && b == 0.0 && c == 0.0)
            {
                throw new InvalidOperationException($"At least one coefficient ({nameof(a)}, {nameof(b)}, or {nameof(c)}) must be non-zero");
            }

            A = a;
            B = b;
            C = c;
            D = d;
        }

        public bool ContainsPoint(Vector3 point)
        {
            return (Transpose * point).Scalar == 0.0;
        }

        public static Plane FromNormalAndDistance(Vector3 normal, double distFromOrigin)
        {
            if (normal == null)
            {
                throw new ArgumentNullException(nameof(normal));
            }

            if (normal.IsInfinity)
            {
                throw new ArgumentOutOfRangeException(nameof(normal), "Unable to create a plane from the infinity vector");
            }

            return new Plane(normal.X / normal.W, normal.Y / normal.W, normal.Z / normal.W, distFromOrigin);
        }

        public static Plane FromPoints(Vector3 p1, Vector3 p2, Vector3 p3)
        {
            var edge1 = p2 - p1;
            var edge2 = p3 - p1;
            var normal = Vector3.Cross(edge1, edge2);

            // a*(x-x0) + b*(y-y0) + c*(z-z0) = 0

            // ax - ax0 + by - by0 + cz - cz0 = 0

            // ax + by + cz = ax0 + by0 + cz0

            // d = -(ax0 + by0 + cz0)

            var d = -(normal.X * p1.X + normal.Y * p1.Y + normal.Z * p1.Z);
            return FromNormalAndDistance(normal, d);
        }
    }
}
