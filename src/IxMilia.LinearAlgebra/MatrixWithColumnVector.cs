using System;

namespace IxMilia.LinearAlgebra
{
    public class MatrixWithColumnVector : Matrix
    {
        public Matrix Parent { get; }
        public Matrix ColumnVector { get; }
        public int ColumnVectorIndex { get; }

        public override int Rows => Parent.Rows;
        public override int Columns => Parent.Columns;

        public override double this[int row, int column]
        {
            get
            {
                CheckIndexAccess(row, column);
                return column == ColumnVectorIndex
                    ? ColumnVector[row, 0]
                    : Parent[row, column];
            }
            set
            {
                CheckIndexAccess(row, column);
                if (column == ColumnVectorIndex)
                {
                    ColumnVector[row, 0] = value;
                }
                else
                {
                    Parent[row, column] = value;
                }
            }
        }

        public MatrixWithColumnVector(Matrix parentMatrix, Matrix columnVector, int columnVectorIndex)
        {
            if (parentMatrix == null)
            {
                throw new ArgumentNullException(nameof(parentMatrix));
            }

            if (columnVector == null)
            {
                throw new ArgumentNullException(nameof(columnVector));
            }

            if (columnVector.Rows != parentMatrix.Rows)
            {
                throw new InvalidOperationException($"Row count of {nameof(parentMatrix)} and {nameof(columnVector)} must match.");
            }

            if (!columnVector.IsColumnVector)
            {
                throw new InvalidOperationException("Column vector is not actually a column vector.");
            }

            if (columnVectorIndex < 0 || columnVectorIndex >= parentMatrix.Columns)
            {
                throw new IndexOutOfRangeException($"Column vector index must be between 0 and {parentMatrix.Columns} inclusive.");
            }

            Parent = parentMatrix;
            ColumnVector = columnVector;
            ColumnVectorIndex = columnVectorIndex;
        }
    }
}
