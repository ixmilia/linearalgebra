namespace IxMilia.LinearAlgebra
{
    public class MinorMatrix<T> : Matrix<T>
    {
        public Matrix<T> Parent { get; }
        public int MinorRow { get; }
        public int MinorColumn { get; }

        public override int Rows => Parent.Rows - 1;
        public override int Columns => Parent.Columns - 1;

        public override T this[int row, int column]
        {
            get
            {
                var r = row < MinorRow ? row : row + 1;
                var c = column < MinorColumn ? column : column + 1;
                return Parent[r, c];
            }

            set
            {
                var r = row < MinorRow ? row : row + 1;
                var c = column < MinorColumn ? column : column + 1;
                Parent[r, c] = value;
            }
        }

        public MinorMatrix(Matrix<T> parent, int minorRow, int minorColumn)
            : base(parent.Computer)
        {
            Parent = parent;
            MinorRow = minorRow;
            MinorColumn = minorColumn;
        }
    }
}
