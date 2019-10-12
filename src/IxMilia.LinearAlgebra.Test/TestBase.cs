using System;

namespace IxMilia.LinearAlgebra.Test
{
    public abstract class TestBase
    {
        public static bool AreClose(double expected, double actual)
        {
            var diff = Math.Abs(actual - expected);
            return diff <= 0.0000000000001;
        }

        public static void AssertClose(double expected, double actual)
        {
            if (!AreClose(expected, actual))
            {
                throw new Exception($"Expected: {expected}, Actual: {actual}");
            }
        }

        public static void AssertClose(Matrix expected, Matrix actual)
        {
            if (expected.Rows != actual.Rows || expected.Columns != actual.Columns)
            {
                throw new Exception($"Excepted {expected.Rows}x{expected.Columns} matrix but got {actual.Rows}x{actual.Columns}.");
            }

            for (int r = 0; r < expected.Rows; r++)
            {
                for (int c = 0; c < expected.Columns; c++)
                {
                    AssertClose(expected[r, c], actual[r, c]);
                }
            }
        }

        public static string NormalizeNewlines(string str)
        {
            return str.Replace("\r", "");
        }
    }
}
