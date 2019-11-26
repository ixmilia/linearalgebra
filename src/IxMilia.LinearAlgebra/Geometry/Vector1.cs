using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Vector1 : Matrix
    {
        public double X
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public double W
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public double LengthSquared => X * X;

        public double Length => Math.Sqrt(LengthSquared);

        public bool IsInfinity => W == 0.0;

        private Vector1()
            : base(2, 1)
        {
        }

        public Vector1(double x)
            : this(x, 1.0)
        {
        }

        public Vector1(double x, double w)
            : this()
        {
            X = x;
            W = w;
        }

        public void TryNormalize()
        {
            if (W == 0.0)
            {
                return;
            }

            X /= W;
            W = 1.0;
        }

        public override string ToString()
        {
            return $"({X})";
        }

        public static Vector1 Infinity => new Vector1(0, 0); // TODO: should this be 0, 1?

        public static Vector1 ZeroVector => new Vector1(0.0);

        public static Matrix CreateScale(double sx)
        {
            return new Matrix(2, 2,
                sx, 0.0,
                0.0, 1.0);
        }

        public static Matrix CreateTranslate(double dx)
        {
            return new Matrix(2, 2,
                1.0, dx,
                0.0, 1.0);
        }
    }
}
