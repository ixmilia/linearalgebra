namespace IxMilia.LinearAlgebra.Geometry
{
    public static class Vector1
    {
        public static Vector1<int> CreateInt32(int x) => new Vector1<int>(Int32AlgebraicComputer.Instance, x);
        public static Vector1<int> CreateInt32(int x, int w) => new Vector1<int>(Int32AlgebraicComputer.Instance, x, w);
        public static Vector1<double> CreateDouble(double x) => new Vector1<double>(DoubleAlgebraicComputer.Instance, x);
        public static Vector1<double> CreateDouble(double x, double w) => new Vector1<double>(DoubleAlgebraicComputer.Instance, x, w);

        public static Vector1<T> CreateInfinity<T>(IAlgebraicComputer<T> computer) => new Vector1<T>(computer, computer.Zero, computer.Zero); // TODO: should this be 0, 1?

        public static Vector1<T> CreateZeroVector<T>(IAlgebraicComputer<T> computer) => new Vector1<T>(computer, computer.Zero);

        public static Matrix<T> CreateScale<T>(IAlgebraicComputer<T> computer, T sx)
        {
            return new Matrix<T>(computer, 2, 2,
                sx, computer.Zero,
                computer.Zero, computer.One);
        }

        public static Matrix<T> CreateTranslate<T>(IAlgebraicComputer<T> computer, T dx)
        {
            return new Matrix<T>(computer, 2, 2,
                computer.One, dx,
                computer.Zero, computer.One);
        }
    }
}
