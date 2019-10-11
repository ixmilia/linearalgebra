namespace IxMilia.LinearAlgebra
{
    public abstract class MatrixVector : Matrix
    {
        public Matrix Parent { get; }

        public double this[int i]
        {
            get { return GetValue(i); }
            set { SetValue(i, value); }
        }

        public override double this[int row, int column]
        {
            get
            {
                CheckIndexAccess(row, column);
                return Rows == 1 ? GetValue(column) : GetValue(row);
            }
            set
            {
                CheckIndexAccess(row, column);
                SetValue(Rows == 1 ? column : row, value);
            }
        }

        protected MatrixVector(Matrix parent)
        {
            Parent = parent;
        }

        protected abstract double GetValue(int i);
        protected abstract void SetValue(int i, double value);
    }
}
