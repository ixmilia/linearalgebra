using System;

namespace IxMilia.LinearAlgebra.Geometry
{
    public static class Conic2
    {
        public static Conic2<int> CreateInt32(int a, int b, int c, int d, int e) => new Conic2<int>(Int32AlgebraicComputer.Instance, a, b, c, d, e);
        public static Conic2<int> CreateInt32(int a, int b, int c, int d, int e, int f) => new Conic2<int>(Int32AlgebraicComputer.Instance, a, b, c, d, e, f);

        public static Conic2<double> CreateDouble(double a, double b, double c, double d, double e) => new Conic2<double>(DoubleAlgebraicComputer.Instance, a, b, c, d, e);
        public static Conic2<double> CreateDouble(double a, double b, double c, double d, double e, double f) => new Conic2<double>(DoubleAlgebraicComputer.Instance, a, b, c, d, e, f);

        public static Conic2<T> FromPoints<T>(IAlgebraicComputer<T> computer, Vector2<T> v1, Vector2<T> v2, Vector2<T> v3, Vector2<T> v4, Vector2<T> v5)
        {
            var constraints = new Matrix<T>(computer, 5, 5,
                computer.Multiply(v1.X, v1.X), computer.Multiply(v1.X, v1.Y), computer.Multiply(v1.Y, v1.Y), v1.X, v1.Y,
                computer.Multiply(v2.X, v2.X), computer.Multiply(v2.X, v2.Y), computer.Multiply(v2.Y, v2.Y), v2.X, v2.Y,
                computer.Multiply(v3.X, v3.X), computer.Multiply(v3.X, v3.Y), computer.Multiply(v3.Y, v3.Y), v3.X, v3.Y,
                computer.Multiply(v4.X, v4.X), computer.Multiply(v4.X, v4.Y), computer.Multiply(v4.Y, v4.Y), v4.X, v4.Y,
                computer.Multiply(v5.X, v5.X), computer.Multiply(v5.X, v5.Y), computer.Multiply(v5.Y, v5.Y), v5.X, v5.Y);
            var ones = new Matrix<T>(computer, 5, 1,
                computer.One,
                computer.One,
                computer.One,
                computer.One,
                computer.One);
            var result = new LinearSystem<T>(constraints, ones).Solve();
            if (result == null)
            {
                return null;
            }

            return new Conic2<T>(computer, result[0, 0], result[1, 0], result[2, 0], result[3, 0], result[4, 0], computer.One);
        }

        /// <summary>
        /// Create a <see cref="Conic2"/> representing a parabola using the standard equation of `Ax^2 + By + C = 0`
        /// </summary>
        /// <param name="a">The coefficient to the `x^2` term.</param>
        /// <param name="b">The coefficient to the `y` term.</param>
        /// <param name="c">The constant.</param>
        /// <returns>A <see cref="Conic2"/> representing a parabola.</returns>
        public static Conic2<T> Parabola<T>(IAlgebraicComputer<T> computer, T a, T b, T c)
        {
            return new Conic2<T>(computer, a, computer.Zero, computer.Zero, computer.Zero, b, c);
        }

        /// <summary>
        /// Create a <see cref="Conic2"/> representing a circle with the given center and radius.
        /// </summary>
        /// <param name="center">The center of the circle.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <returns>The <see cref="Conic2"/> representing a circle.</returns>
        public static Conic2<T> Circle<T>(Vector2<T> center, T radius)
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
        public static Conic2<T> Ellipse<T>(Vector2<T> center, T majorAxisLength, T minorAxisLength)
        {
            // (x-h)^2   (y-k)^2
            // ------- + ------- = 1
            //   a^2       b^2

            // b^2*(x-h)^2 + a^2(y-k)^2 - a^2*b^2 = 0

            // b^2*x^2 - 2*b^2*h*x + b^2*h^2 + a^2*y^2 - 2*a^2*k*y + a^2*k^2 - a^2*b^2 = 0
            //    A          D         F         C          E          F          F

            var computer = center.Computer;
            if (!computer.IsOne(center.W))
            {
                throw new ArgumentException("Center must be normalized");
            }

            var a = majorAxisLength;
            var b = minorAxisLength;
            var h = center.X;
            var k = center.Y;

            var A = computer.Square(b);
            var B = computer.Zero;
            var C = computer.Square(a);
            var D = computer.Multiply(computer.Negate(computer.Two()), b, b, h);
            var E = computer.Multiply(computer.Negate(computer.Two()), a, a, k);
            var F = computer.Subtract(computer.Add(computer.Multiply(b, b, h, h), computer.Multiply(a, a, k, k)), computer.Multiply(a, a, b, b));
            return new Conic2<T>(computer, A, B, C, D, E, F);
        }
    }
}
