using System.Collections.Generic;

namespace IxMilia.LinearAlgebra.Test
{
    internal class MatrixComparer<T> : IEqualityComparer<Matrix<T>>
    {
        public bool Equals(Matrix<T> x, Matrix<T> y)
        {
            return x == y;
        }

        public int GetHashCode(Matrix<T> obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class DoubleMatrixComparer : MatrixComparer<double>
    {
        public static DoubleMatrixComparer Instance = new DoubleMatrixComparer();
    }

    internal class Int32MatrixComparer : MatrixComparer<int>
    {
        public static Int32MatrixComparer Instance = new Int32MatrixComparer();
    }
}
