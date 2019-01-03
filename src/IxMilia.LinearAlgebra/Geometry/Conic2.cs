using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public class Conic2 : Matrix
    {
        public double A
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public double B
        {
            get { return this[0, 1] * 2.0; }
            set { this[0, 1] = value / 2.0; this[1, 0] = value / 2.0; }
        }

        public double C
        {
            get { return this[1, 1]; }
            set { this[1, 1] = value; }
        }

        public double D
        {
            get { return this[0, 2] * 2.0; }
            set { this[0, 2] = value / 2.0; ; this[2, 0] = value / 2.0; }
        }

        public double E
        {
            get { return this[1, 2] * 2.0; }
            set { this[1, 2] = value / 2.0; this[2, 1] = value / 2.0; }
        }

        public double F
        {
            get { return this[2, 2]; }
            set { this[2, 2] = value; }
        }

        public bool IsDegenerate => Determinant == 0.0;

        private double A33_Minor => new MinorMatrix(this, 3, 3).Determinant;

        public bool IsHyperbola => A33_Minor < 0.0;

        public bool IsRectangularHyperbola => IsHyperbola && A + C == 0.0;

        public bool IsParabola => A33_Minor == 0.0;

        public bool IsEllipse => A33_Minor > 0.0;

        public bool IsCircle => AreClose(A, C) && AreClose(B, 0.0);

        public Vector2 Center => (new MinorMatrix(this, 2, 2).Inverse * new Matrix(2, 1, -D / 2.0, -E / 2.0)).AsVector2();

        public Conic2(double a, double b, double c, double d, double e)
            : this(a, b, c, d, e, 1.0)
        {
        }

        /// <summary>
        /// Ax^2 + Bxy + Cy^2 + Dx + Ey + F = 0
        /// </summary>
        public Conic2(double a, double b, double c, double d, double e, double f)
            : base(3, 3)
        {
            // [ A,   B/2, D/2 ]
            // [ B/2, C,   E/2 ]
            // [ D/2, E/2, F   ]
            A = a;
            B = b;
            C = c;
            D = d;
            E = e;
            F = f;
        }

        public Matrix As6Vector()
        {
            return new Matrix(6, 1,
                A,
                B,
                C,
                D,
                E,
                F);
        }

        public Line2 GetTangetLineAtPoint(Vector2 point)
        {
            return (this * point).AsLine2();
        }

        public static Conic2 FromPoints(Vector2 v1, Vector2 v2, Vector2 v3, Vector2 v4, Vector2 v5)
        {
            var constraints = new Matrix(5, 5,
                v1.X * v1.X, v1.X * v1.Y, v1.Y * v1.Y, v1.X, v1.Y,
                v2.X * v2.X, v2.X * v2.Y, v2.Y * v2.Y, v2.X, v2.Y,
                v3.X * v3.X, v3.X * v3.Y, v3.Y * v3.Y, v3.X, v3.Y,
                v4.X * v4.X, v4.X * v4.Y, v4.Y * v4.Y, v4.X, v4.Y,
                v5.X * v5.X, v5.X * v5.Y, v5.Y * v5.Y, v5.X, v5.Y);
            var ones = new Matrix(5, 1,
                1,
                1,
                1,
                1,
                1);
            var result = new LinearSystem(constraints, ones).Solve();
            if (result == null)
            {
                return null;
            }

            return new Conic2(result[0, 0], result[1, 0], result[2, 0], result[3, 0], result[4, 0], 1.0);
        }

        /// <summary>
        /// Create a <see cref="Conic2"/> representing a parabola using the standard equation of `Ax^2 + By + C = 0`
        /// </summary>
        /// <param name="a">The coefficient to the `x^2` term.</param>
        /// <param name="b">The coefficient to the `y` term.</param>
        /// <param name="c">The constant.</param>
        /// <returns>A <see cref="Conic2"/> representing a parabola.</returns>
        public static Conic2 Parabola(double a, double b, double c)
        {
            return new Conic2(a, 0.0, 0.0, 0.0, b, c);
        }

        /// <summary>
        /// Create a <see cref="Conic2"/> representing a circle with the given center and radius.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>The <see cref="Conic2"/> representing a circle.</returns>
        public static Conic2 Circle(Vector2 center, double radius)
        {
            return Ellipse(center, radius, radius);
        }

        /// <summary>
        /// Create a <see cref="Conic2"/> representing an ellipse with the given center and major and minor axes.
        /// </summary>
        /// <param name="center">The center of the ellipse.</param>
        /// <param name="majorAxisLength">The length of the major axis of the ellipse.</param>
        /// <param name="minorAxisLength">The length of the minor axis of the ellipse.</param>
        /// <returns>A <see cref="Conic2"/> representing an ellipse.</returns>
        public static Conic2 Ellipse(Vector2 center, double majorAxisLength, double minorAxisLength)
        {
            // (x-h)^2   (y-k)^2
            // ------- + ------- = 1
            //   a^2       b^2

            // b^2*(x-h)^2 + a^2(y-k)^2 - a^2*b^2 = 0

            // b^2*x^2 - 2*b^2*h*x + b^2*h^2 + a^2*y^2 - 2*a^2*k*y + a^2*k^2 - a^2*b^2 = 0
            //    A          D         F         C          E          F          F

            if (center.W != 1.0)
            {
                throw new ArgumentException("Center must be normalized");
            }

            var a = majorAxisLength;
            var b = minorAxisLength;
            var h = center.X;
            var k = center.Y;

            var A = b * b;
            var B = 0.0;
            var C = a * a;
            var D = -2.0 * b * b * h;
            var E = -2.0 * a * a * k;
            var F = (b * b * h * h) + (a * a * k * k) - (a * a * b * b);
            return new Conic2(A, B, C, D, E, F);
        }
    }
}
