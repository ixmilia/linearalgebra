using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace IxMilia.LinearAlgebra.Test
{
    internal class DoubleMatrixEqualityComparer : IEqualityComparer<Matrix<double>>
    {
        public static DoubleMatrixEqualityComparer Instance = new DoubleMatrixEqualityComparer();

        public bool Equals([AllowNull] Matrix<double> x, [AllowNull] Matrix<double> y) => Matrix<double>.AreEqual(x, y, DoubleAlgebraicComputer.Instance.AreClose);

        public int GetHashCode([DisallowNull] Matrix<double> obj)
        {
            throw new NotImplementedException();
        }
    }
}
