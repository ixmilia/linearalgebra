using System;

namespace IxMilia.LinearAlgebra
{
    public static class NormExtensions
    {
        public static bool IsVector<T>(this Matrix<T> m)
        {
            return m.Columns == 1;
        }

        public static T UnrootedNorm<T>(this Matrix<T> m, int p)
        {
            if (!m.IsVector())
            {
                throw new InvalidOperationException("Matrix is not a vector.");
            }

            if (p <= 0)
            {
                throw new ArgumentException(nameof(p), $"`{nameof(p)}` must be greater than or equal to 1.");
            }

            var result = m.Computer.Zero;
            for (int i = 0; i < p; i++)
            {
                result = m.Computer.Add(result, m.Computer.Pow(m.Computer.AbsoluteValue(m[i, 0]), p));
            }

            return result;
        }

        public static T Norm<T>(this Matrix<T> m, int p)
        {
            var unrooted = m.UnrootedNorm(p);
            return m.Computer.Pow(unrooted, 1.0 / p);
        }

        public static T MaxNorm<T>(this Matrix<T> m)
        {
            if (!m.IsVector())
            {
                throw new InvalidOperationException("Matrix is not a vector.");
            }

            var result = m[0, 0];
            for (int i = 1; i < m.Rows; i++)
            {
                if (m.Computer.IsGreater(m[i, 0], result))
                {
                    result = m[i, 0];
                }
            }

            return result;
        }

        public static T FrobeniusNorm<T>(this Matrix<T> m)
        {
            var result = m.Computer.Zero;
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Columns; c++)
                {
                    var v = m[r, c];
                    result = m.Computer.Add(result, m.Computer.Square(v));
                }
            }

            return m.Computer.SquareRoot(result);
        }
    }
}
