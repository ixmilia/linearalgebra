using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class GeometryTests : TestBase
    {
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
