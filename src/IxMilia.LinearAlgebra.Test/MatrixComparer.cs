using System.Collections.Generic;

namespace IxMilia.LinearAlgebra.Test
{
    internal class MatrixComparer : IEqualityComparer<Matrix>
    {
        public static readonly MatrixComparer Instance = new MatrixComparer();

        public bool Equals(Matrix x, Matrix y)
        {
            return x == y;
        }

        public int GetHashCode(Matrix obj)
        {
            return obj.GetHashCode();
        }
    }
}
