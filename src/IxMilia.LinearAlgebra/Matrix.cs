using System;
using System.Text;

namespace IxMilia.LinearAlgebra
{
    public class Matrix
    {
        private const double Epsilon = 1.0E-13;

        private readonly double[,] _values;

        public virtual int Rows { get; }
        public virtual int Columns { get; }

        public virtual double this[int row, int column]
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

        public double Scalar
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
                        if (this[r, c] != 0.0)
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
                    var rowVector = new MatrixRowVector(this, 2);
                    return rowVector[0] == 0.0 && rowVector[1] == 0.0 && rowVector[2] == 1.0;
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
                    var sub = new SubMatrix(this, 0, 2, 0, 2);
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
                return IsEuclidian && new SubMatrix(this, 0, 2, 0, 2).Determinant == 1.0;
            }
        }

        public Matrix Transpose => new TransposeMatrix(this);

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
                            if (this[r, c] != 1.0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (this[r, c] != 0.0)
                            {
                                return false;
                            }
                        }
                    }
                }

                return true;
            }
        }

        public double Determinant
        {
            get
            {
                if (!IsSquare || Rows == 1)
                {
                    throw new InvalidOperationException("Determinant can only be computed for square matrices with a rank of 2 or greater.");
                }

                if (Rows == 2)
                {
                    return this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
                }

                var result = 0.0;
                var factor = 1.0;
                for (int i = 0; i < Columns; i++)
                {
                    var value = this[0, i];
                    if (value != 0.0)
                    {
                        // don't compute the minor and determinant unnecessarily
                        result += value * factor * new MinorMatrix(this, 0, i).Determinant;
                    }

                    factor *= -1.0;
                }

                return result;
            }
        }

        public Matrix OfMinors
        {
            get
            {
                var result = new Matrix(Rows, Columns);
                for (int r = 0; r < Rows; r++)
                {
                    for (int c = 0; c < Columns; c++)
                    {
                        result[r, c] = new MinorMatrix(this, r, c).Determinant;
                    }
                }

                return result;
            }
        }

        public Matrix Cofactor
        {
            get
            {
                if (IsSquare && Rows == 2)
                {
                    return new Matrix(2, 2,
                        this[1, 1], -this[1, 0],
                        -this[0, 1], this[0, 0]);
                }
                else
                {
                    var minors = OfMinors;
                    var scale = 1.0;
                    for (int r = 0; r < Rows; r++)
                    {
                        for (int c = 0; c < Columns; c++)
                        {
                            minors[r, c] = minors[r, c] * scale;
                            scale *= -1.0;
                        }
                    }

                    return minors;
                }
            }
        }

        public Matrix Adjugate
        {
            get
            {
                return Cofactor.Transpose;
            }
        }

        public Matrix Inverse
        {
            get
            {
                if (!IsSquare)
                {
                    throw new InvalidOperationException("Only square matrices can be inverted");
                }

                var det = Determinant;
                if (det == 0.0)
                {
                    return null;
                }

                var inverse = (1.0 / det) * Adjugate;
                return inverse;
            }
        }

        protected Matrix()
        {
            // Rows = 0, Columns = 0, _values = null
        }

        protected Matrix(double[,] values)
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

        public Matrix(int rows, int columns)
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
            _values = new double[rows, columns];
        }

        public Matrix(int rows, int columns, double[,] values)
            : this(rows, columns)
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

        public Matrix(int rows, int columns, params double[] values)
            : this(rows, columns)
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

        public double Dot(Matrix other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            MatrixVector left, right;
            int itemCount;
            if (IsRowVector && other.IsRowVector && Columns == other.Columns)
            {
                left = new MatrixRowVector(this, 0);
                right = new MatrixRowVector(other, 0);
                itemCount = Columns;
            }
            else if (IsColumnVector && other.IsColumnVector && Rows == other.Rows)
            {
                left = new MatrixColumnVector(this, 0);
                right = new MatrixColumnVector(other, 0);
                itemCount = Rows;
            }
            else
            {
                throw new InvalidOperationException("Matrices must be either both row vectors or both column vectors");
            }

            var result = 0.0;
            for (int i = 0; i < itemCount; i++)
            {
                result += left[i] * right[i];
            }

            return result;
        }

        protected static bool AreClose(double a, double b)
        {
            return Math.Abs(a - b) <= Epsilon;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (int r = 0; r < Rows; r++)
            {
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
            var other = obj as Matrix;
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

        protected static Matrix Cross(double ax, double ay, double az, double bx, double by, double bz)
        {
            var rx = ay * bz - az * by;
            var ry = az * bx - ax * bz;
            var rz = ax * by - ay * bx;
            return new Matrix(3, 1, rx, ry, rz);
        }

        public static Matrix operator *(Matrix left, Matrix right)
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

            var result = new double[left.Rows, right.Columns];
            for (int r = 0; r < left.Rows; r++)
            {
                for (int c = 0; c < right.Columns; c++)
                {
                    // row i of left * column j of right, sum
                    var sum = 0.0;
                    for (int k = 0; k < left.Columns; k++)
                    {
                        sum += left[r, k] * right[k, c];
                    }

                    result[r, c] = sum;
                }
            }

            return new Matrix(result);
        }

        public static Matrix operator *(Matrix matrix, double scalar)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix));
            }

            var values = new double[matrix.Rows, matrix.Columns];
            for (int r = 0; r < matrix.Rows; r++)
            {
                for (int c = 0; c < matrix.Columns; c++)
                {
                    values[r, c] = scalar * matrix[r, c];
                }
            }

            return new Matrix(values);
        }

        public static Matrix operator *(double scalar, Matrix matrix)
        {
            return matrix * scalar;
        }

        public static bool operator ==(Matrix a, Matrix b)
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
                    if (!ItemEqualityChecker(a[r, c], b[r, c]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix a, Matrix b)
        {
            return !(a == b);
        }

        internal static Func<double, double, bool> ItemEqualityChecker = (a, b) => a == b;
    }
}
