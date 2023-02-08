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
            var points = new List<Vector2<double>>();
            var angle = 0.0;
            for (int i = 0; i < 5; i++, angle += delta)
            {
                points.Add(Vector2.CreateDouble(Math.Cos(angle), Math.Sin(angle)));
            }

            var circle = Conic2.FromPoints(DoubleAlgebraicComputer.Instance, points[0], points[1], points[2], points[3], points[4]);
            Assert.True(circle.IsCircle);
            AssertClose(Vector2.CreateDouble(0.0, 0.0), circle.Center);
        }

        [Fact]
        public void UnitCircleTest()
        {
            var unit = Conic2.Circle(Vector2.CreateDouble(0, 0), 1);
            var expected = Matrix.CreateDouble(3, 3,
                1, 0, 0,
                0, 1, 0,
                0, 0, -1);
            Assert.Equal(expected, unit, DoubleMatrixComparer.Instance);
        }

        [Fact]
        public void ConicTangentLineTest()
        {
            var unitCircle = Conic2.Circle(Vector2.CreateDouble(0, 0), 1);
            var pointOnCircle = Vector2.CreateDouble(1, 0);
            var tangentLine = unitCircle.GetTangentLineAtPoint(pointOnCircle);
            Assert.True(tangentLine.ContainsPoint(Vector2.CreateDouble(1, 0)));
            Assert.True(tangentLine.ContainsPoint(Vector2.CreateDouble(1, 1)));
        }
    }
}
