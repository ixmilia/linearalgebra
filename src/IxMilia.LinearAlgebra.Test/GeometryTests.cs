using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class GeometryTests : TestBase
    {
        [Fact]
        public void Vector1SimpleScaleTest()
        {
            var vector = Vector1.CreateInt32(1);
            var translate = Vector1.CreateScale(Int32AlgebraicComputer.Instance, 5);
            var result = translate * vector;
            var expected = Vector1.CreateInt32(5);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector1SimpleTranslateTest()
        {
            var vector = Vector1.CreateDouble(1);
            var translate = Vector1.CreateTranslate(DoubleAlgebraicComputer.Instance, 5);
            var result = translate * vector;
            var expected = Vector1.CreateDouble(6);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(32.0, 0.0)]
        [InlineData(212.0, 100.0)]
        public void Vector1CombineTransformationsTest(double input, double output)
        {
            // converting F to C
            var transform = Vector1.CreateScale(DoubleAlgebraicComputer.Instance, 5.0 / 9.0) * Vector1.CreateTranslate(DoubleAlgebraicComputer.Instance, -32.0);
            var result = transform * Vector1.CreateDouble(input);
            Assert.Equal(1.0, result[1, 0]);
            Assert.Equal(output, result[0, 0]);
        }

        [Fact]
        public void Vector2SimpleScaleTest()
        {
            var vector = Vector2.CreateInt32(1, 2);
            var translate = Vector2.CreateScale(Int32AlgebraicComputer.Instance, 5, 5);
            var result = translate * vector;
            var expected = Vector2.CreateInt32(5, 10);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector2SimpleTranslateTest()
        {
            var vector = Vector2.CreateDouble(1, 2);
            var translate = Vector2.CreateTranslate(DoubleAlgebraicComputer.Instance, 5, 5);
            var result = translate * vector;
            var expected = Vector2.CreateDouble(6, 7);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector3SimpleScaleTest()
        {
            var vector = Vector3.CreateInt32(1, 2, 3);
            var translate = Vector3.CreateScale(Int32AlgebraicComputer.Instance, 5, 5, 5);
            var result = translate * vector;
            var expected = Vector3.CreateInt32(5, 10, 15);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector3SimpleTranslateTest()
        {
            var vector = Vector3.CreateDouble(1, 2, 3);
            var translate = Vector3.CreateTranslate(DoubleAlgebraicComputer.Instance, 5, 5, 5);
            var result = translate * vector;
            var expected = Vector3.CreateDouble(6, 7, 8);
            Assert.Equal(expected, result);
        }
    }
}
