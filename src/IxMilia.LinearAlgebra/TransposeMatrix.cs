namespace IxMilia.LinearAlgebra
{
    public class TransposeMatrix<T> : Matrix<T>
    {
        public Matrix<T> Parent { get; }

        public override int Rows => Parent.Columns;
        public override int Columns => Parent.Rows;

        public override T this[int row, int column]
        {
            get { return Parent[column, row]; }
            set { Parent[column, row] = value; }
        }

        public TransposeMatrix(Matrix<T> parent)
            : base(parent.Computer)
        {
            Parent = parent;
        }
    }
}
