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
            get { return Parent[row, column]; }
            set { Parent[row, column] = value; }
        }

        protected MatrixVector(Matrix parent)
        {
            Parent = parent;
        }

        protected abstract double GetValue(int i);
        protected abstract void SetValue(int i, double value);
    }
}
