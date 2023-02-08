namespace IxMilia.LinearAlgebra
{
    public class MatrixColumnVector<T> : MatrixVector<T>
    {
        public int Column { get; }

        public override int Rows { get; }
        public override int Columns => 1;

        public MatrixColumnVector(Matrix<T> parent, int column)
            : base(parent)
        {
            Column = column;
            Rows = parent.Rows;
        }

        protected override T GetValue(int i) => Parent[i, Column];
        protected override void SetValue(int i, T value) => Parent[i, Column] = value;
    }
}
