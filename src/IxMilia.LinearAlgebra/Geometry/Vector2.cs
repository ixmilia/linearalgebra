using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Vector2 : Matrix
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

        public double W
        {
            get { return this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public double LengthSquared => X * X + Y * Y;

        public double Length => Math.Sqrt(LengthSquared);

        public bool IsInfinity => W == 0.0;

        private Vector2()
            : base(3, 1)
        {
        }

        public Vector2(double x, double y)
            : this(x, y, 1.0)
        {
        }

        public Vector2(double x, double y, double w)
            : this()
        {
            X = x;
            Y = y;
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
            W = 1.0;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Vector2 Cross(Vector2 v1, Vector2 v2)
        {
            if (v1 == null)
            {
                throw new ArgumentNullException(nameof(v1));
            }

            if (v2 == null)
            {
                throw new ArgumentNullException(nameof(v2));
            }

            return Cross(v1.X, v1.Y, v1.W, v2.X, v2.Y, v2.W).AsVector2();
        }

        public static Vector2 Infinity => new Vector2(0, 0, 0); // TODO: should this be 0, 0, 1?

        public static Vector2 ZeroVector => new Vector2(0.0, 0.0);

        public static Matrix CreateScale(double scale)
        {
            return CreateScale(scale, scale);
        }

        public static Matrix CreateScale(double sx, double sy)
        {
            return new Matrix(3, 3,
                sx, 0.0, 0.0,
                0.0, sy, 0.0,
                0.0, 0.0, 1.0);
        }

        public static Matrix CreateTranslate(double dx, double dy)
        {
            return new Matrix(3, 3,
                1.0, 0.0, dx,
                0.0, 1.0, dy,
                0.0, 0.0, 1.0);
        }
    }
}
