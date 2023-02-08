namespace IxMilia.LinearAlgebra.Geometry
{
    public class Vector2<T> : Matrix<T>
    {
        public T X
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public T Y
        {
            get { return this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public T W
        {
            get { return this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public T LengthSquared => Computer.Add(Computer.Square(X), Computer.Square(Y));

        public T Length => Computer.SquareRoot(LengthSquared);

        public bool IsInfinity => Computer.IsZero(W);

        private Vector2(IAlgebraicComputer<T> computer)
            : base(computer, 3, 1)
        {
        }

        public Vector2(IAlgebraicComputer<T> computer, T x, T y)
            : this(computer, x, y, computer.One)
        {
        }

        public Vector2(IAlgebraicComputer<T> computer, T x, T y, T w)
            : this(computer)
        {
            X = x;
            Y = y;
            W = w;
        }

        public void TryNormalize()
        {
            if (Computer.IsZero(W))
            {
                return;
            }

            X = Computer.Divide(X, W);
            Y = Computer.Divide(Y, W);
            W = Computer.One;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}
