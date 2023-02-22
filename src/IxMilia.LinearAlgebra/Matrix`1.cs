using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IxMilia.LinearAlgebra
{
    public class Matrix<T>
    {
        public IAlgebraicComputer<T> Computer { get; }

        private readonly T[,] _values;

        public virtual int Rows { get; }
        public virtual int Columns { get; }

        public virtual T this[int row, int column]
        {
            get
            {
                CheckIndexAccess(row, column);
                return _values[row, column];
            }
            set
            {
                CheckIndexAccess(row, column);
                _values[row, column] = value;
            }
        }

        public bool IsSquare => Rows == Columns;
        public bool IsRowVector => Rows == 1;
        public bool IsColumnVector => Columns == 1;

        public T Scalar
        {
            get
            {
                if (Rows != 1 || Columns != 1)
                {
                    throw new InvalidOperationException("Matrix is not a scalar");
                }

                return this[0, 0];
            }
        }

        public bool IsZero
        {
            get
            {
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        if (!Computer.IsZero(this[r, c]))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public bool IsAffine
        {
            get
            {
                if (Rows == 3 && Columns == 3)
                {
                    var rowVector = new MatrixRowVector<T>(this, 2);
                    return Computer.IsZero(rowVector[0]) && Computer.IsZero(rowVector[1]) && Computer.IsOne(rowVector[2]);
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsEuclidian
        {
            get
            {
                if (IsAffine)
                {
                    var sub = new SubMatrix<T>(this, 0, 2, 0, 2);
                    return sub.IsOrthoganal;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsOrientedEuclidian
        {
            get
            {
                return IsEuclidian && Computer.IsOne(new SubMatrix<T>(this, 0, 2, 0, 2).Determinant);
            }
        }

        public Matrix<T> Transpose => new TransposeMatrix<T>(this);

        public bool IsOrthoganal
        {
            get
            {
                if (!IsSquare)
                {
                    return false;
                }

                return (this * Transpose).IsIdentity;
            }
        }

        public bool IsIdentity
        {
            get
            {
                if (!IsSquare)
                {
                    return false;
                }

                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        if (r == c)
                        {
                            if (!Computer.IsOne(this[r, c]))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (!Computer.IsZero(this[r, c]))
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
        }

        public T Determinant
        {
            get
            {
                if (!IsSquare || Rows == 1)
                {
                    throw new InvalidOperationException("Determinant can only be computed for square matrices with a rank of 2 or greater.");
                }

                if (Rows == 2)
                {
                    return Computer.Subtract(Computer.Multiply(this[0, 0], this[1, 1]), Computer.Multiply(this[0, 1], this[1, 0]));
                }

                var result = Computer.Zero;
                var factor = Computer.One;
                for (int i = 0; i < Columns; i++)
                {
                    var value = this[0, i];
                    if (!Computer.IsZero(value))
                    {
                        // don't compute the minor and determinant unnecessarily
                        result = Computer.Add(result, Computer.Multiply(value, factor, new MinorMatrix<T>(this, 0, i).Determinant));
                    }

                    factor = Computer.Negate(factor);
                }

                return result;
            }
        }

        public Matrix<T> OfMinors
        {
            get
            {
                var result = new Matrix<T>(Computer, Rows, Columns);
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        result[r, c] = new MinorMatrix<T>(this, r, c).Determinant;
                    }
                }

                return result;
            }
        }

        public Matrix<T> Cofactor
        {
            get
            {
                if (IsSquare && Rows == 2)
                {
                    return new Matrix<T>(Computer, 2, 2,
                        this[1, 1], Computer.Negate(this[1, 0]),
                        Computer.Negate(this[0, 1]), this[0, 0]);
                }
                else
                {
                    var minors = OfMinors;
                    var scale = Computer.One;
                    for (int r = 0; r < Rows; r++)
                    {
                        for (int c = 0; c < Columns; c++)
                        {
                            minors[r, c] = Computer.Multiply(minors[r, c], scale);
                            scale = Computer.Negate(scale);
                        }
                    }

                    return minors;
                }
            }
        }

        public Matrix<T> Adjugate
        {
            get
            {
                return Cofactor.Transpose;
            }
        }

        public Matrix<T> Inverse
        {
            get
            {
                if (!IsSquare)
                {
                    throw new InvalidOperationException("Only square matrices can be inverted");
                }

                var det = Determinant;
                if (Computer.IsZero(det))
                {
                    return null;
                }

                var inverse = Computer.Divide(Computer.One, det) * Adjugate;
                return inverse;
            }
        }

        protected Matrix(IAlgebraicComputer<T> computer)
        {
            // Rows = 0, Columns = 0, _values = null
            Computer = computer;
        }

        internal Matrix(IAlgebraicComputer<T> computer, T[,] values)
            : this(computer)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            var rows = values.GetLength(0);
            var columns = values.GetLength(1);

            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            Rows = rows;
            Columns = columns;
            _values = values;
        }

        public Matrix(IAlgebraicComputer<T> computer, int rows, int columns)
            : this(computer)
        {
            if (rows <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rows));
            }

            if (columns <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columns));
            }

            Rows = rows;
            Columns = columns;
            _values = new T[rows, columns];
        }

        public Matrix(IAlgebraicComputer<T> computer, int rows, int columns, T[,] values)
            : this(computer, rows, columns)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.GetLength(0) != rows || values.GetLength(1) != columns)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Improper number of values given.");
            }

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    this[r, c] = values[r, c];
                }
            }
        }

        public Matrix(IAlgebraicComputer<T> computer, int rows, int columns, params T[] values)
            : this(computer, rows, columns)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length != rows * columns)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Improper number of values given.");
            }

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    _values[r, c] = values[r * columns + c];
                }
            }
        }

        public Matrix(IAlgebraicComputer<T> computer, IEnumerable<IEnumerable<T>> values)
            : this(computer)
        {
            var rows = new List<List<T>>();
            foreach (var row in values)
            {
                var rowList = row.ToList();
                if (rows.Count > 0)
                {
                    if (rowList.Count != rows[0].Count)
                    {
                        throw new ArgumentOutOfRangeException(nameof(values), "All rows must have the same number of values");
                    }
                }

                rows.Add(rowList);
            }

            if (rows.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Must contain at least 1 row.");
            }

            Rows = rows.Count;
            Columns = rows[0].Count;

            if (Columns == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(values), "Must contain at least 1 column.");
            }

            _values = new T[Rows, Columns];

            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    _values[r, c] = rows[r][c];
                }
            }
        }

        protected void CheckIndexAccess(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row));
            }

            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column));
            }
        }

        public T Dot(Matrix<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            MatrixVector<T> left, right;
            int itemCount;
            if (IsRowVector && other.IsRowVector && Columns == other.Columns)
            {
                left = new MatrixRowVector<T>(this, 0);
                right = new MatrixRowVector<T>(other, 0);
                itemCount = Columns;
            }
            else if (IsColumnVector && other.IsColumnVector && Rows == other.Rows)
            {
                left = new MatrixColumnVector<T>(this, 0);
                right = new MatrixColumnVector<T>(other, 0);
                itemCount = Rows;
            }
            else
            {
                throw new InvalidOperationException("Matrices must be either both row vectors or both column vectors");
            }

            var result = Computer.Zero;
            for (int i = 0; i < itemCount; i++)
            {
                result = Computer.Add(result, Computer.Multiply(left[i], right[i]));
            }
            
            return result;
        }

        public T Fold(T initial, Func<T, T, T> combinator)
        {
            var result = initial;
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    result = combinator(result, this[r, c]);
                }
            }

            return result;
        }

        public T Sum() => Fold(Computer.Zero, Computer.Add);

        public Matrix<T> MapValue(Func<T, T> action)
        {
            return MapValue((_r, _c, value) => action(value));
        }

        public Matrix<T> MapValue(Func<int, int, T, T> action)
        {
            var values = new T[Rows, Columns];
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    values[r, c] = action(r, c, this[r, c]);
                }
            }

            return new Matrix<T>(Computer, values);
        }

        public Matrix<T> MapRow(Func<MatrixRowVector<T>, Matrix<T>> action)
        {
            return MapRow((row, _i) => action(row));
        }

        public Matrix<T> MapRow(Func<MatrixRowVector<T>, int, Matrix<T>> action)
        {
            var newRows = AsRows().Select((row, i) => action(row, i)).ToArray();
            return Matrix.FromRows(Computer, newRows);
        }

        public Matrix<T> MapColumn(Func<MatrixColumnVector<T>, Matrix<T>> action)
        {
            return MapColumn((column, _i) => action(column));
        }

        public Matrix<T> MapColumn(Func<MatrixColumnVector<T>, int, Matrix<T>> action)
        {
            var newColumns = AsColumns().Select((column, i) => action(column, i)).ToArray();
            return Matrix.FromColumns(Computer, newColumns);
        }

        public MatrixRowVector<T>[] AsRows()
        {
            return Enumerable.Range(0, Rows).Select(r => new MatrixRowVector<T>(this, r)).ToArray();
        }

        public MatrixColumnVector<T>[] AsColumns()
        {
            return Enumerable.Range(0, Columns).Select(c => new MatrixColumnVector<T>(this, c)).ToArray();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (int r = 0; r < Rows; r++)
            {
                if (r != 0)
                {
                    sb.Append(" ");
                }

                sb.Append("[");
                for (int c = 0; c < Columns; c++)
                {
                    sb.Append(this[r, c]);
                    if (c != Columns - 1)
                    {
                        sb.Append(", ");
                    }
                }

                sb.Append("]");
                if (r != Rows - 1)
                {
                    sb.Append("\n");
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Matrix<T>;
            if (other != null)
            {
                return this == other;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var result = Rows.GetHashCode() ^ Columns.GetHashCode();
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    result ^= this[r, c].GetHashCode();
                }
            }

            return result;
        }

        public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new InvalidOperationException("Matrices must be of the same size.");
            }

            var computer = left.Computer;
            var values = new T[left.Rows, left.Columns];
            for (int r = 0; r < left.Rows; r++)
            {
                for (int c = 0; c < left.Columns; c++)
                {
                    values[r, c] = computer.Add(left[r, c], right[r, c]);
                }
            }

            return new Matrix<T>(computer, values);
        }

        public static Matrix<T> operator -(Matrix<T> left, Matrix<T> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new InvalidOperationException("Matrices must be of the same size.");
            }

            var computer = left.Computer;
            var values = new T[left.Rows, left.Columns];
            for (int r = 0; r < left.Rows; r++)
            {
                for (int c = 0; c < left.Columns; c++)
                {
                    values[r, c] = computer.Subtract(left[r, c], right[r, c]);
                }
            }

            return new Matrix<T>(computer, values);
        }

        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left == null)
            {
                throw new ArgumentNullException(nameof(left));
            }

            if (right == null)
            {
                throw new ArgumentNullException(nameof(right));
            }

            if (left.Columns != right.Rows)
            {
                throw new InvalidOperationException("Inner dimensions don't match.");
            }

            var computer = left.Computer;
            var result = new T[left.Rows, right.Columns];
            for (int r = 0; r < left.Rows; r++)
            {
                for (int c = 0; c < right.Columns; c++)
                {
                    // row i of left * column j of right, sum
                    var sum = computer.Zero;
                    for (int k = 0; k < left.Columns; k++)
                    {
                        sum = computer.Add(sum, computer.Multiply(left[r, k], right[k, c]));
                    }

                    result[r, c] = sum;
                }
            }

            return new Matrix<T>(computer, result);
        }

        public static Matrix<T> operator *(Matrix<T> matrix, T scalar)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var computer = matrix.Computer;
            var values = new T[matrix.Rows, matrix.Columns];
            for (int r = 0; r < matrix.Rows; r++)
            {
                for (int c = 0; c < matrix.Columns; c++)
                {
                    values[r, c] = computer.Multiply(scalar, matrix[r, c]);
                }
            }

            return new Matrix<T>(computer, values);
        }

        public static Matrix<T> operator *(T scalar, Matrix<T> matrix)
        {
            return matrix * scalar;
        }

        public static bool operator ==(Matrix<T> a, Matrix<T> b) => AreEqual(a, b, a.Computer.AreEqual);

        internal static bool AreEqual(Matrix<T> a, Matrix<T> b, Func<T, T, bool> areItemsEqual)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            if (a.Rows != b.Rows || a.Columns != b.Columns)
            {
                return false;
            }

            for (int r = 0; r < a.Rows; r++)
            {
                for (int c = 0; c < a.Columns; c++)
                {
                    if (!areItemsEqual(a[r, c], b[r, c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix<T> a, Matrix<T> b)
        {
            return !(a == b);
        }
    }
}
