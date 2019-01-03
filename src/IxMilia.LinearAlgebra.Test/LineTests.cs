using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class LineTests : TestBase
    {
        [Fact]
        public void TransposeTest()
        {
            Matrix point = new Line2(1, -1, 1);
            Assert.Equal(3, point.Rows);
            Assert.Equal(1, point.Columns);
            var transpose = point.Transpose;
            Assert.Equal(new Matrix(1, 3, 1, -1, 1), transpose, MatrixComparer.Instance);
        }

        [Fact]
        public void ContainsPointTest()
        {
            var line = new Line2(1, -1, 0); // y = x
            Assert.True(line.ContainsPoint(new Vector2(0, 0)));
            Assert.True(line.ContainsPoint(new Vector2(1, 1)));
            Assert.True(line.ContainsPoint(new Vector2(-2, -2)));
        }

        [Fact]
        public void FromPointsTest1()
        {
            var line = Line2.FromPoints(new Vector2(1, 1), new Vector2(2, 2)); // y = x
            Assert.Equal(new Line2(-1, 1, 0), line);
        }

        [Fact]
        public void FromPointsTest2()
        {
            // y = 2x + 1 => -2x + 1y = 1 -> -2x + 1y - 1 = 0
            var line = Line2.FromPoints(new Vector2(0, 1), new Vector2(1, 3));
            Assert.Equal(new Line2(-2, 1, -1), line);
        }

        [Fact]
        public void IntersectionTest1()
        {
            var l1 = Line2.XEquals(3);
            var l2 = Line2.YEquals(4);
            var intersection = Line2.Intersection(l1, l2);
            Assert.Equal(new Vector2(3, 4), intersection);
        }

        [Fact]
        public void IntersectionTest2()
        {
            var l1 = Line2.FromSlopeAndYIntercept(slope: 1, yIntercept: 1);
            var l2 = Line2.FromSlopeAndYIntercept(slope: -1, yIntercept: 1);
            var intersection = Line2.Intersection(l1, l2);
            Assert.Equal(new Vector2(0, 1), intersection);
        }

        [Fact]
        public void IntersectionOfParallelLines()
        {
            var l1 = Line2.XEquals(1);
            var l2 = Line2.XEquals(2);
            Assert.True(l1.IsParallelTo(l2));
        }

        [Fact]
        public void InfinityTest()
        {
            var line = Line2.Infinity;
            var point = Vector2.Infinity;
            Assert.True((line * point.Transpose).IsZero);
        }
    }
}
