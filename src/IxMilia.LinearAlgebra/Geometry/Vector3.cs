using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Vector3 : Matrix
    {
        public double X
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public double Y
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public double Z
        {
            get { return this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public double W
        {
            get { return this[3, 0]; }
            set { this[3, 0] = value; }
        }

        public double LengthSquared => X * X + Y * Y + Z * Z;

        public double Length => Math.Sqrt(LengthSquared);

        public bool IsInfinity => W == 0.0;

        private Vector3()
            : base(4, 1)
        {
        }

        public Vector3(double x, double y, double z)
            : this(x, y, z, 1.0)
        {
        }

        public Vector3(double x, double y, double z, double w)
            : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public void TryNormalize()
        {
            if (W == 0.0)
            {
                return;
            }

            X /= W;
            Y /= W;
            Z /= W;
            W = 1.0;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }

        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return v1 + (-v2);
        }

        public static Vector3 operator -(Vector3 v1)
        {
            return (v1 * -1.0).AsVector3();
        }

        public static Vector3 Cross(Vector3 v1, Vector3 v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return Cross(v1.X, v1.Y, v1.Z, v2.X, v2.Y, v2.Z).AsVector3();
        }

        public static Vector3 FromPlanes(Plane p1, Plane p2, Plane p3)
        {
            var ls = new LinearSystem(
                new Matrix(3, 3,
                    p1.A, p1.B, p1.C,
                    p2.A, p2.B, p2.C,
                    p3.A, p3.B, p3.C),
                new Matrix(3, 1,
                    p1.D,
                    p2.D,
                    p3.D));
            var result = ls.Solve();
            return result?.AsVector3();
        }

        public static Vector3 Infinity => new Vector3(0.0, 0.0, 0.0, 0.0);

        public static Vector3 ZeroVector => new Vector3(0.0, 0.0, 0.0);

        public static Matrix CreateScale(double scale)
        {
            return CreateScale(scale, scale, scale);
        }

        public static Matrix CreateScale(double sx, double sy, double sz)
        {
            return new Matrix(4, 4,
                sx, 0.0, 0.0, 0.0,
                0.0, sy, 0.0, 0.0,
                0.0, 0.0, sz, 0.0,
                0.0, 0.0, 0.0, 1.0);
        }

        public static Matrix CreateTranslate(double dx, double dy, double dz)
        {
            return new Matrix(4, 4,
                1.0, 0.0, 0.0, dx,
                0.0, 1.0, 0.0, dy,
                0.0, 0.0, 1.0, dz,
                0.0, 0.0, 0.0, 1.0);
        }
    }
}
