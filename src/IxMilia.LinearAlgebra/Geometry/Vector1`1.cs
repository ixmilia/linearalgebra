namespace IxMilia.LinearAlgebra.Geometry
{
    public class Vector1<T> : Matrix<T>
    {
        public T X
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public T W
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public T LengthSquared => Computer.Square(X);

        public T Length => Computer.SquareRoot(LengthSquared);

        public bool IsInfinity => Computer.IsZero(W);

        private Vector1(IAlgebraicComputer<T> computer)
            : base(computer, 2, 1)
        {
        }

        public Vector1(IAlgebraicComputer<T> computer, T x)
            : this(computer, x, computer.One)
        {
        }

        public Vector1(IAlgebraicComputer<T> computer, T x, T w)
            : this(computer)
        {
            X = x;
            W = w;
        }

        public void TryNormalize()
        {
            if (Computer.IsZero(W))
            {
                return;
            }

            X = Computer.Divide(X, W);
            W = Computer.One;
        }

        public override string ToString()
        {
            return $"({X})";
        }
    }
}
