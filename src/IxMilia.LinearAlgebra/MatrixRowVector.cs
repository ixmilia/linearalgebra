namespace IxMilia.LinearAlgebra
{
    public class MatrixRowVector : MatrixVector
    {
        public int Row { get; }

        public override int Rows => 1;
        public override int Columns { get; }

        public MatrixRowVector(Matrix parent, int row)
            : base(parent)
        {
            Row = row;
            Columns = parent.Columns;
        }

        protected override double GetValue(int i) => Parent[Row, i];
        protected override void SetValue(int i, double value) => Parent[Row, i] = value;
    }
}
