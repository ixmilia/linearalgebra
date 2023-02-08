namespace IxMilia.LinearAlgebra.Geometry
{
    public class Conic2<T> : Matrix<T>
    {
        public T A
        {
            get { return this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public T B
        {
            get { return Computer.Multiply(this[0, 1], Computer.Two()); }
            set { this[0, 1] = Computer.Divide(value, Computer.Two()); this[1, 0] = Computer.Divide(value, Computer.Two()); }
        }

        public T C
        {
            get { return this[1, 1]; }
            set { this[1, 1] = value; }
        }

        public T D
        {
            get { return Computer.Multiply(this[0, 2], Computer.Two()); }
            set { this[0, 2] = Computer.Divide(value, Computer.Two()); ; this[2, 0] = Computer.Divide(value, Computer.Two()); }
        }

        public T E
        {
            get { return Computer.Multiply(this[1, 2], Computer.Two()); }
            set { this[1, 2] = Computer.Divide(value, Computer.Two()); this[2, 1] = Computer.Divide(value, Computer.Two()); }
        }

        public T F
        {
            get { return this[2, 2]; }
            set { this[2, 2] = value; }
        }

        public bool IsDegenerate => Computer.IsZero(Determinant);

        private T A33_Minor => new MinorMatrix<T>(this, 3, 3).Determinant;

        public bool IsHyperbola => Computer.IsNegative(A33_Minor);

        public bool IsRectangularHyperbola => IsHyperbola && Computer.IsZero(Computer.Add(A, C));

        public bool IsParabola => Computer.IsZero(A33_Minor);

        public bool IsEllipse => Computer.IsPositive(A33_Minor);

        public bool IsCircle => Computer.AreClose(A, C) && Computer.AreClose(B, Computer.Zero);

        public Vector2<T> Center => (new MinorMatrix<T>(this, 2, 2).Inverse * new Matrix<T>(Computer, 2, 1, Computer.Divide(Computer.Negate(D), Computer.Two()), Computer.Divide(Computer.Negate(E), Computer.Two()))).AsVector2();

        public Conic2(IAlgebraicComputer<T> computer, T a, T b, T c, T d, T e)
            : this(computer, a, b, c, d, e, computer.One)
        {
        }

        /// <summary>
        /// Ax^2 + Bxy + Cy^2 + Dx + Ey + F = 0
        /// </summary>
        public Conic2(IAlgebraicComputer<T> computer, T a, T b, T c, T d, T e, T f)
            : base(computer, 3, 3)
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

        public Matrix<T> As6Vector()
        {
            return new Matrix<T>(Computer, 6, 1,
                A,
                B,
                C,
                D,
                E,
                F);
        }

        public Line2<T> GetTangentLineAtPoint(Vector2<T> point)
        {
            return (this * point).AsLine2();
        }
    }
}
