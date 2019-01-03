namespace IxMilia.LinearAlgebra
{
    public class MinorMatrix : Matrix
    {
        public Matrix Parent { get; }
        public int MinorRow { get; }
        public int MinorColumn { get; }

        public override int Rows => Parent.Rows - 1;
        public override int Columns => Parent.Columns - 1;

        public override double this[int row, int column]
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

        public MinorMatrix(Matrix parent, int minorRow, int minorColumn)
            : base()
        {
            Parent = parent;
            MinorRow = minorRow;
            MinorColumn = minorColumn;
        }
    }
}
