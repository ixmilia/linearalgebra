using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class MatrixTests : TestBase
    {
        [Fact]
        public void MultiplicationTest()
        {
            var left = new Matrix(2, 3,
                1, 2, 3,
                4, 5, 6);

            var right = new Matrix(3, 2,
                7, 8,
                9, 10,
                11, 12);

            var result = left * right;
            Assert.Equal(new Matrix(2, 2,
                58, 64,
                139, 154),
                result);
        }

        [Fact]
        public void MinorMatrixTest()
        {
            var matrix = new Matrix(3, 3,
                1, 2, 3,
                4, 5, 6,
                7, 8, 9);
            var minor1 = new MinorMatrix(matrix, 0, 0);
            Assert.Equal(new Matrix(2, 2,
                5, 6,
                8, 9),
                minor1,
                MatrixComparer.Instance);
        }

        [Fact]
        public void DeterminantTest()
        {
            var matrix = new Matrix(3, 3,
                -2, 2, -3,
                -1, 1, 3,
                2, 0, 1);
            Assert.Equal(18, matrix.Determinant);
        }

        [Fact]
        public void InvertTest()
        {
            using (new CloseToEqualityChecker())
            {
                var matrix = new Matrix(3, 3,
                    3, 0, 2,
                    2, 0, -2,
                    0, 1, 1);
                var inv = matrix.Inverse;
                Assert.Equal(new Matrix(3, 3,
                    0.2, 0.2, 0,
                    -0.2, 0.3, 1,
                    0.2, -0.3, 0),
                    inv);
            }
        }
    }
}
