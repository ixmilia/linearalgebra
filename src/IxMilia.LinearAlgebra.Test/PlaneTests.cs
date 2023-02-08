using IxMilia.LinearAlgebra.Geometry;
using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class PlaneTests : TestBase
    {
        [Fact]
        public void FromThreePointsTest()
        {
            // z = 0
            var plane1 = Plane.FromNormalAndDistance(Vector3.CreateDouble(0, 0, 1), 0);
            var plane2 = Plane.FromPoints(Vector3.CreateDouble(0, 0, 0), Vector3.CreateDouble(1, 0, 0), Vector3.CreateDouble(0, 1, 0));
            Assert.Equal(plane1, plane2);
        }

        [Fact]
        public void Vector3FromPlanesTest()
        {
            // looking for (1, 2, 3)
            var x = Plane.FromNormalAndDistance(Vector3.CreateDouble(1, 0, 0), 1);
            var y = Plane.FromNormalAndDistance(Vector3.CreateDouble(0, 1, 0), 2);
            var z = Plane.FromNormalAndDistance(Vector3.CreateDouble(0, 0, 1), 3);
            var point = Vector3.FromPlanes(x, y, z);
            Assert.Equal(Vector3.CreateDouble(1, 2, 3), point);
        }

        [Fact]
        public void PlaneContainsTest()
        {
            var plane = Plane.FromNormalAndDistance(Vector3.CreateDouble(0, 0, 1), 0);

            Assert.True(plane.ContainsPoint(Vector3.CreateDouble(2, 3, 0)));
            Assert.False(plane.ContainsPoint(Vector3.CreateDouble(0, 0, 1)));
        }
    }
}
