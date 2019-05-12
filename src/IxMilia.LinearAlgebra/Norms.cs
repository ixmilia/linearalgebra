using System;

namespace IxMilia.LinearAlgebra
{
    public static class NormExtensions
    {
        public static bool IsVector(this Matrix m)
        {
            return m.Columns == 1;
        }

        public static double UnrootedNorm(this Matrix m, int p)
        {
            if (!m.IsVector())
            {
                throw new InvalidOperationException("Matrix is not a vector.");
            }

            if (p <= 0)
            {
                throw new ArgumentException(nameof(p), $"`{nameof(p)}` must be greater than or equal to 1.");
            }

            var result = 0.0;
            for (int i = 0; i < p; i++)
            {
                result += Math.Pow(Math.Abs(m[i, 0]), p);
            }

            return result;
        }

        public static double Norm(this Matrix m, int p)
        {
            var unrooted = m.UnrootedNorm(p);
            return Math.Pow(unrooted, 1.0 / p);
        }

        public static double MaxNorm(this Matrix m)
        {
            if (!m.IsVector())
            {
                throw new InvalidOperationException("Matrix is not a vector.");
            }

            var result = m[0, 0];
            for (int i = 1; i < m.Rows; i++)
            {
                if (m[i, 0] > result)
                {
                    result = m[i, 0];
                }
            }

            return result;
        }

        public static double FrobeniusNorm(this Matrix m)
        {
            var result = 0.0;
            for (int r = 0; r < m.Rows; r++)
            {
                for (int c = 0; c < m.Columns; c++)
                {
                    var v = m[r, c];
                    result += v * v;
                }
            }

            return Math.Sqrt(result);
        }
    }
}
