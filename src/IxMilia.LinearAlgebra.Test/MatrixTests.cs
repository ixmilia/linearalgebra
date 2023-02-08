using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class MatrixTests : TestBase
    {
        [Fact]
        public void ToStringTest()
        {
            var matrix = Matrix.CreateInt32(2, 3,
                1, 2, 3,
                4, 5, 6);
            var expected = NormalizeNewlines(@"
[[1, 2, 3]
 [4, 5, 6]]").Trim();
            Assert.Equal(expected, matrix.ToString());
        }

        [Fact]
        public void ConstructorApiTest()
        {
            var matrix = Matrix.CreateInt32(
                new[] { new[] { 1, 2, 3 },
                        new[] { 4, 5, 6 } });
            var expected = Matrix.CreateInt32(2, 3,
                1, 2, 3,
                4, 5, 6);
            Assert.Equal(expected, matrix);
        }

        [Fact]
        public void MultiplicationTest()
        {
            var left = Matrix.CreateInt32(2, 3,
                1, 2, 3,
                4, 5, 6);

            var right = Matrix.CreateInt32(3, 2,
                7, 8,
                9, 10,
                11, 12);

            var result = left * right;
            Assert.Equal(Matrix.CreateInt32(2, 2,
                58, 64,
                139, 154),
                result);
        }

        [Fact]
        public void MinorMatrixTest()
        {
            var matrix = Matrix.CreateInt32(3, 3,
                1, 2, 3,
                4, 5, 6,
                7, 8, 9);
            var minor1 = new MinorMatrix<int>(matrix, 0, 0);
            Assert.Equal(Matrix.CreateInt32(2, 2,
                5, 6,
                8, 9),
                minor1,
                Int32MatrixComparer.Instance);
        }

        [Fact]
        public void DeterminantTest()
        {
            var matrix = Matrix.CreateDouble(3, 3,
                -2, 2, -3,
                -1, 1, 3,
                2, 0, 1);
            Assert.Equal(18, matrix.Determinant);
        }

        [Fact]
        public void InvertTest()
        {
            var matrix = Matrix.CreateDouble(3, 3,
                3, 0, 2,
                2, 0, -2,
                0, 1, 1);
            var inv = matrix.Inverse;
            Assert.Equal(Matrix.CreateDouble(3, 3,
                0.2, 0.2, 0,
                -0.2, 0.3, 1,
                0.2, -0.3, 0),
                inv,
                DoubleMatrixEqualityComparer.Instance);
        }

        [Fact]
        public void NormTest()
        {
            Assert.Equal(5, Vector2.CreateInt32(3, 4).Norm(2));
        }

        [Fact]
        public void MaxNormTest()
        {
            Assert.Equal(5, Vector3.CreateInt32(1, 5, 2).MaxNorm());
        }

        [Fact]
        public void AsRowsTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var rows = matrix.AsRows();
            Assert.Equal(3, rows.Length);
            Assert.Equal(Matrix.CreateInt32(1, 2, 1, 2), rows[0]);
            Assert.Equal(Matrix.CreateInt32(1, 2, 3, 4), rows[1]);
            Assert.Equal(Matrix.CreateInt32(1, 2, 5, 6), rows[2]);
        }

        [Fact]
        public void AsColumnsTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var columns = matrix.AsColumns();
            Assert.Equal(2, columns.Length);
            Assert.Equal(Matrix.CreateInt32(3, 1,
                    1,
                    3,
                    5), columns[0]);
            Assert.Equal(Matrix.CreateInt32(3, 1,
                    2,
                    4,
                    6), columns[1]);
        }

        [Fact]
        public void FromRowsTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var rows = matrix.AsRows();
            var reconstructed = Matrix.FromRows(matrix.Computer, rows);
            Assert.Equal(matrix, reconstructed);
        }

        [Fact]
        public void FromColumnsTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var columns = matrix.AsColumns();
            var reconstructed = Matrix.FromColumns(matrix.Computer, columns);
            Assert.Equal(matrix, reconstructed);
        }

        [Fact]
        public void AdditionTest()
        {
            var m1 = Matrix.CreateInt32(1, 2, 1, 2);
            var m2 = Matrix.CreateInt32(1, 2, 3, 4);
            Assert.Equal(Matrix.CreateInt32(1, 2, 1 + 3, 2 + 4), m1 + m2);
        }

        [Fact]
        public void SubtractionTest()
        {
            var m1 = Matrix.CreateInt32(1, 2, 1, 2);
            var m2 = Matrix.CreateInt32(1, 2, 3, 4);
            Assert.Equal(Matrix.CreateInt32(1, 2, 1 - 3, 2 - 4), m1 - m2);
        }

        [Fact]
        public void MapValueTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var inc = matrix.MapValue(v => v + 1);
            var expected = Matrix.CreateInt32(3, 2,
                2, 3,
                4, 5,
                6, 7);
            Assert.Equal(expected, inc);
        }

        [Fact]
        public void MapRowTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var add = Matrix.CreateInt32(1, 2,
                1, 2);
            var result = matrix.MapRow(row => row + add);
            var expected = Matrix.CreateInt32(3, 2,
                2, 4,
                4, 6,
                6, 8);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void MapColumnTest()
        {
            var matrix = Matrix.CreateInt32(3, 2,
                1, 2,
                3, 4,
                5, 6);
            var add = Matrix.CreateInt32(3, 1,
                1,
                2,
                3);
            var result = matrix.MapColumn(column => column + add);
            var expected = Matrix.CreateInt32(3, 2,
                2, 3,
                5, 6,
                8, 9);
            Assert.Equal(expected, result);
        }
    }
}
