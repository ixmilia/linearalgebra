using System;
using System.Collections.Generic;
using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class ConicTests : TestBase
    {
        [Fact]
        public void CircleConicTest()
        {
            var delta = Math.PI / 6.0; // 30 degrees
            var points = new List<Vector2>();
            var angle = 0.0;
            for (int i = 0; i < 5; i++, angle += delta)
            {
                points.Add(new Vector2(Math.Cos(angle), Math.Sin(angle)));
            }

            var circle = Conic2.FromPoints(points[0], points[1], points[2], points[3], points[4]);
            Assert.True(circle.IsCircle);
            AssertClose(new Vector2(0, 0), circle.Center);
        }

        [Fact]
        public void UnitCircleTest()
        {
            var unit = Conic2.Circle(new Vector2(0, 0), 1);
            var expected = new Matrix(3, 3,
                1, 0, 0,
                0, 1, 0,
                0, 0, -1);
            Assert.Equal(expected, unit, MatrixComparer.Instance);
        }

        [Fact]
        public void ConicTangentLineTest()
        {
            var unitCircle = Conic2.Circle(new Vector2(0, 0), 1);
            var pointOnCircle = new Vector2(1, 0);
            var tangentLine = unitCircle.GetTangetLineAtPoint(pointOnCircle);
            Assert.True(tangentLine.ContainsPoint(new Vector2(1, 0)));
            Assert.True(tangentLine.ContainsPoint(new Vector2(1, 1)));
        }
    }
}
