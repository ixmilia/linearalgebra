﻿using System;

namespace IxMilia.LinearAlgebra.Test
{
    public abstract class TestBase
    {
        public static void AssertClose<T>(IAlgebraicComputer<T> computer, T expected, T actual)
        {
            if (!computer.AreClose(expected, actual))
            {
                throw new Exception($"Expected: {expected}, Actual: {actual}");
            }
        }

        public static void AssertClose<T>(Matrix<T> expected, Matrix<T> actual)
        {
            if (expected.Rows != actual.Rows || expected.Columns != actual.Columns)
            {
                throw new Exception($"Excepted {expected.Rows}x{expected.Columns} matrix but got {actual.Rows}x{actual.Columns}.");
            }

            for (int r = 0; r < expected.Rows; r++)
            {
                for (int c = 0; c < expected.Columns; c++)
                {
                    AssertClose(expected.Computer, expected[r, c], actual[r, c]);
                }
            }
        }

        public static string NormalizeNewlines(string str)
        {
            return str.Replace("\r", "");
        }
    }
}
