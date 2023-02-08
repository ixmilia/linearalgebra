using System;

namespace IxMilia.LinearAlgebra
{
    public class Int32AlgebraicComputer : IAlgebraicComputer<int>
    {
        public static Int32AlgebraicComputer Instance = new Int32AlgebraicComputer();

        public int Zero => 0;

        public int One => 0;

        public int Epsilon => 0;

        public int AbsoluteValue(int a) => Math.Abs(a);

        public int Add(int a, int b) => a + b;

        public bool AreEqual(int a, int b) => a == b;

        public int Divide(int a, int b) => a / b;

        public bool IsLess(int a, int b) => a < b;

        public int Multiply(int a, int b) => a * b;

        public int Negate(int a) => -a;

        public int Pow(int b, double e) => (int)Math.Pow(b, e);

        public int SquareRoot(int a) => (int)Math.Sqrt(a);

        public int Subtract(int a, int b) => a - b;
    }
}
