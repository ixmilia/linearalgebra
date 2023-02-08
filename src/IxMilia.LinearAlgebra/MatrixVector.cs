namespace IxMilia.LinearAlgebra
{
    public abstract class MatrixVector<T> : Matrix<T>
    {
        public Matrix<T> Parent { get; }
        
        public T this[int i]
        {
            get { return GetValue(i); }
            set { SetValue(i, value); }
        }

        public override T this[int row, int column]
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

        protected MatrixVector(Matrix<T> parent)
            : base(parent.Computer)
        {
            Parent = parent;
        }

        protected abstract T GetValue(int i);
        protected abstract void SetValue(int i, T value);
    }
}
