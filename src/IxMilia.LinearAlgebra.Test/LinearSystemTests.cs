using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class LinearSystemTests : TestBase
    {
        [Fact]
        public void SimpleLinearSystemTest()
        {
            // 3x + 2y - z = 1
            // 2x - 2y + 4z = -2
            // -x + 0.5y - z = 0
            // x = 1, y = -2, z = -2
            var ls = new LinearSystem<double>(
                Matrix.CreateDouble(3, 3,
                    3.0, 2.0, -1.0,
                    2.0, -2.0, 4.0,
                    -1.0, 0.5, -1.0),
                Matrix.CreateDouble(3, 1,
                    1.0,
                    -2.0,
                    0.0));
            var solution = ls.Solve();
            var expected = Matrix.CreateDouble(3, 1,
                1.0,
                -2.0,
                -2.0);
            Assert.Equal(expected, solution, DoubleMatrixEqualityComparer.Instance);
        }
    }
}
