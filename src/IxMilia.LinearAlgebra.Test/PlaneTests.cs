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
            var plane1 = Plane.FromNormalAndDistance(new Vector3(0, 0, 1), 0.0);
            var plane2 = Plane.FromPoints(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0));
            Assert.Equal(plane1, plane2);
        }

        [Fact]
        public void Vector3FromPlanesTest()
        {
            // looking for (1, 2, 3)
            var x = Plane.FromNormalAndDistance(new Vector3(1.0, 0.0, 0.0), 1.0);
            var y = Plane.FromNormalAndDistance(new Vector3(0.0, 1.0, 0.0), 2.0);
            var z = Plane.FromNormalAndDistance(new Vector3(0.0, 0.0, 1.0), 3.0);
            var point = Vector3.FromPlanes(x, y, z);
            Assert.Equal(new Vector3(1.0, 2.0, 3.0), point);
        }

        [Fact]
        public void PlaneContainsTest()
        {
            var plane = Plane.FromNormalAndDistance(new Vector3(0, 0, 1), 0.0);

            Assert.True(plane.ContainsPoint(new Vector3(2, 3, 0)));
            Assert.False(plane.ContainsPoint(new Vector3(0, 0, 1)));
        }
    }
}
