using System;
using System.Collections.Generic;

namespace IxMilia.LinearAlgebra
{
    public static class Matrix
    {
        public static Matrix<int> CreateInt32(int rows, int columns, params int[] values) => new Matrix<int>(Int32AlgebraicComputer.Instance, rows, columns, values);
        public static Matrix<int> CreateInt32(IEnumerable<IEnumerable<int>> values) => new Matrix<int>(Int32AlgebraicComputer.Instance, values);

        public static Matrix<double> CreateDouble(int rows, int columns, params double[] values) => new Matrix<double>(DoubleAlgebraicComputer.Instance, rows, columns, values);
        public static Matrix<double> CreateDouble(IEnumerable<IEnumerable<double>> values) => new Matrix<double>(DoubleAlgebraicComputer.Instance, values);

        public static Matrix<T> FromRows<T>(IAlgebraicComputer<T> computer, Matrix<T>[] rows)
        {
            if (rows.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            var columnCount = rows[0].Columns;
            var values = new T[rows.Length, columnCount];
            for (int r = 0; r < rows.Length; r++)
            {
                if (!rows[r].IsRowVector)
                {
                    throw new ArgumentException(nameof(rows), "Must be a row vector.");
                }

                if (rows[r].Columns != columnCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(Matrix<T>.Columns), "Column counts are inconsistent.");
                }

                for (int c = 0; c < columnCount; c++)
                {
                    values[r, c] = rows[r][0, c];
                }
            }

            return new Matrix<T>(computer, values);
        }

        public static Matrix<T> FromColumns<T>(IAlgebraicComputer<T> computer, Matrix<T>[] columns)
        {
            if (columns.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            var rowCount = columns[0].Rows;
            var values = new T[rowCount, columns.Length];
            for (int c = 0; c < columns.Length; c++)
            {
                if (!columns[c].IsColumnVector)
                {
                    throw new ArgumentException(nameof(columns), "Must be a column vector.");
                }

                if (columns[c].Rows != rowCount)
                {
                    throw new ArgumentOutOfRangeException(nameof(Matrix<T>.Rows), "Row counts are inconsistent.");
                }

                for (int r = 0; r < rowCount; r++)
                {
                    values[r, c] = columns[c][r, 0];
                }
            }

            return new Matrix<T>(computer, values);
        }

        public static Matrix<T> Cross<T>(IAlgebraicComputer<T> computer, T ax, T ay, T az, T bx, T by, T bz)
        {
            var rx = computer.Subtract(computer.Multiply(ay, bz), computer.Multiply(az, by));
            var ry = computer.Subtract(computer.Multiply(az, bx), computer.Multiply(ax, bz));
            var rz = computer.Subtract(computer.Multiply(ax, by), computer.Multiply(ay, bx));
            return new Matrix<T>(computer, 3, 1, rx, ry, rz);
        }
    }
}
