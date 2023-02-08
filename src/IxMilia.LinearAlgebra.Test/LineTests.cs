using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class LineTests : TestBase
    {
        [Fact]
        public void TransposeTest()
        {
            Matrix<int> point = Line2.CreateInt32(1, -1, 1);
            Assert.Equal(3, point.Rows);
            Assert.Equal(1, point.Columns);
            var transpose = point.Transpose;
            Assert.Equal(Matrix.CreateInt32(1, 3, 1, -1, 1), transpose, Int32MatrixComparer.Instance);
        }

        [Fact]
        public void ContainsPointTest()
        {
            var line = Line2.CreateInt32(1, -1, 0); // y = x
            Assert.True(line.ContainsPoint(Vector2.CreateInt32(0, 0)));
            Assert.True(line.ContainsPoint(Vector2.CreateInt32(1, 1)));
            Assert.True(line.ContainsPoint(Vector2.CreateInt32(-2, -2)));
        }

        [Fact]
        public void FromPointsTest1()
        {
            var line = Line2.FromPoints(Vector2.CreateDouble(1, 1), Vector2.CreateDouble(2, 2)); // y = x
            Assert.Equal(Line2.CreateDouble(-1, 1, 0), line);
        }

        [Fact]
        public void FromPointsTest2()
        {
            // y = 2x + 1 => -2x + 1y = 1 -> -2x + 1y - 1 = 0
            var line = Line2.FromPoints(Vector2.CreateDouble(0, 1), Vector2.CreateDouble(1, 3));
            Assert.Equal(Line2.CreateDouble(-2, 1, -1), line);
        }

        [Fact]
        public void IntersectionTest1()
        {
            var l1 = Line2.XEquals(DoubleAlgebraicComputer.Instance, 3);
            var l2 = Line2.YEquals(DoubleAlgebraicComputer.Instance, 4);
            var intersection = Line2.Intersection(l1, l2);
            Assert.Equal(Vector2.CreateDouble(3, 4), intersection);
        }

        [Fact]
        public void IntersectionTest2()
        {
            var l1 = Line2.FromSlopeAndYIntercept(DoubleAlgebraicComputer.Instance, slope: 1, yIntercept: 1);
            var l2 = Line2.FromSlopeAndYIntercept(DoubleAlgebraicComputer.Instance, slope: -1, yIntercept: 1);
            var intersection = Line2.Intersection(l1, l2);
            Assert.Equal(Vector2.CreateDouble(0, 1), intersection);
        }

        [Fact]
        public void IntersectionOfParallelLines()
        {
            var l1 = Line2.XEquals(Int32AlgebraicComputer.Instance, 1);
            var l2 = Line2.XEquals(Int32AlgebraicComputer.Instance, 2);
            Assert.True(l1.IsParallelTo(l2));
        }

        [Fact]
        public void InfinityTest()
        {
            var line = Line2.CreateInfinity(DoubleAlgebraicComputer.Instance);
            var point = Vector2.CreateInfinity(DoubleAlgebraicComputer.Instance);
            Assert.True((line * point.Transpose).IsZero);
        }
    }
}
