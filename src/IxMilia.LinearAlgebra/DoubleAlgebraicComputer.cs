using System;

namespace IxMilia.LinearAlgebra
{
    public class DoubleAlgebraicComputer : IAlgebraicComputer<double>
    {
        public static DoubleAlgebraicComputer Instance = new DoubleAlgebraicComputer();

        public double Epsilon => 1.0e-13;

        public double Zero => 0.0;

        public double One => 1.0;

        public double AbsoluteValue(double a) => Math.Abs(a);

        public double Add(double a, double b) => a + b;

        public bool AreEqual(double a, double b) => a == b;

        public double Divide(double a, double b) => a / b;

        public bool IsLess(double a, double b) => a < b;

        public double Multiply(double a, double b) => a * b;

        public double Negate(double a) => -a;

        public double Pow(double b, double e) => Math.Pow(b, e);

        public double SquareRoot(double a) => Math.Sqrt(a);

        public double Subtract(double a, double b) => a - b;
    }
}
