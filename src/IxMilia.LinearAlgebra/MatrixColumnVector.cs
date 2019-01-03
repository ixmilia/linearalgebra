namespace IxMilia.LinearAlgebra
{
    public class MatrixColumnVector : MatrixVector
    {
        public int Column { get; }

        public override int Rows { get; }
        public override int Columns => 1;

        public MatrixColumnVector(Matrix parent, int column)
            : base(parent)
        {
            Column = column;
            Rows = parent.Rows;
        }

        protected override double GetValue(int i) => Parent[i, Column];
        protected override void SetValue(int i, double value) => Parent[i, Column] = value;
    }
}
