using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class GeometryTests : TestBase
    {
        [Fact]
        public void Vector1SimpleScaleTest()
        {
            var vector = new Vector1(1.0);
            var translate = Vector1.CreateScale(5.0);
            var result = translate * vector;
            var expected = new Vector1(5.0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector1SimpleTranslateTest()
        {
            var vector = new Vector1(1.0);
            var translate = Vector1.CreateTranslate(5.0);
            var result = translate * vector;
            var expected = new Vector1(6.0);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(32.0, 0.0)]
        [InlineData(212.0, 100.0)]
        public void Vector1CombineTransformationsTest(double input, double output)
        {
            // converting F to C
            var transform = Vector1.CreateScale(5.0 / 9.0) * Vector1.CreateTranslate(-32.0);
            var result = transform * new Vector1(input);
            Assert.Equal(1.0, result[1, 0]);
            Assert.Equal(output, result[0, 0]);
        }

        [Fact]
        public void Vector2SimpleScaleTest()
        {
            var vector = new Vector2(1.0, 2.0);
            var translate = Vector2.CreateScale(5.0, 5.0);
            var result = translate * vector;
            var expected = new Vector2(5.0, 10.0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector2SimpleTranslateTest()
        {
            var vector = new Vector2(1.0, 2.0);
            var translate = Vector2.CreateTranslate(5.0, 5.0);
            var result = translate * vector;
            var expected = new Vector2(6.0, 7.0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector3SimpleScaleTest()
        {
            var vector = new Vector3(1.0, 2.0, 3.0);
            var translate = Vector3.CreateScale(5.0, 5.0, 5.0);
            var result = translate * vector;
            var expected = new Vector3(5.0, 10.0, 15.0);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Vector3SimpleTranslateTest()
        {
            var vector = new Vector3(1.0, 2.0, 3.0);
            var translate = Vector3.CreateTranslate(5.0, 5.0, 5.0);
            var result = translate * vector;
            var expected = new Vector3(6.0, 7.0, 8.0);
            Assert.Equal(expected, result);
        }
    }
}
