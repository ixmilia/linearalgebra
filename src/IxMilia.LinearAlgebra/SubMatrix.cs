using System;

namespace IxMilia.LinearAlgebra
{
    public class SubMatrix : Matrix
    {
        public Matrix Parent { get; }

        public int RowOffset { get; }
        public override int Rows { get; }

        public int ColumnOffset { get; }
        public override int Columns { get; }

        public override double this[int row, int column]
        {
            get
            {
                CheckIndexAccess(row, column);
                var r = RowOffset + row;
                var c = ColumnOffset + column;
                CheckParentIndexAccess(r, c);
                return Parent[r, c];
            }

            set
            {
                CheckIndexAccess(row, column);
                var r = RowOffset + row;
                var c = ColumnOffset + column;
                CheckParentIndexAccess(r, c);
                Parent[r, c] = value;
            }
        }

        public SubMatrix(Matrix parent, int rowOffset, int rows, int columnOffset, int columns)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (rowOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rowOffset));
            }

            if (rowOffset + rows >= parent.Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (columnOffset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnOffset));
            }

            if (columnOffset + columns >= parent.Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            Parent = parent;
            RowOffset = rowOffset;
            Rows = rows;
            ColumnOffset = columnOffset;
            Columns = columns;
        }

        private void CheckParentIndexAccess(int row, int column)
        {
            if (row < 0 || row >= Parent.Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column < 0 || column >= Parent.Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }
        }
    }
}
