namespace IxMilia.LinearAlgebra
{
    public class TransposeMatrix : Matrix
    {
        public Matrix Parent { get; }

        public override int Rows => Parent.Columns;
        public override int Columns => Parent.Rows;

        public override double this[int row, int column]
        {
            get { return Parent[column, row]; }
            set { Parent[column, row] = value; }
        }

        public TransposeMatrix(Matrix parent)
        {
            Parent = parent;
        }
    }
}
