namespace IxMilia.LinearAlgebra
{
    public class MatrixRowVector<T> : MatrixVector<T>
    {
        public int Row { get; }

        public override int Rows => 1;
        public override int Columns { get; }

        public MatrixRowVector(Matrix<T> parent, int row)
            : base(parent)
        {
            Row = row;
            Columns = parent.Columns;
        }

        protected override T GetValue(int i) => Parent[Row, i];
        protected override void SetValue(int i, T value) => Parent[Row, i] = value;
    }
}
