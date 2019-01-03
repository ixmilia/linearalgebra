namespace IxMilia.LinearAlgebra.Geometry
{
    public static class GeometryExtensions
    {
        public static Line2 AsLine2(this Matrix matrix)
        {
            if (matrix.Rows == 3 && matrix.Columns == 1)
            {
                return new Line2(matrix[0, 0], matrix[1, 0], matrix[2, 0]);
            }

            return null;
        }

        public static Vector2 AsVector2(this Matrix matrix)
        {
            if ((matrix.Rows == 2 || matrix.Rows == 3) && matrix.Columns == 1)
            {
                return new Vector2(matrix[0, 0], matrix[1, 0], matrix.Rows == 3 ? matrix[2, 0] : 1.0);
            }

            return null;
        }

        public static Vector3 AsVector3(this Matrix matrix)
        {
            if ((matrix.Rows == 3 || matrix.Rows == 4) && matrix.Columns == 1)
            {
                return new Vector3(matrix[0, 0], matrix[1, 0], matrix[2, 0], matrix.Rows == 4 ? matrix[3, 0] : 1.0);
            }

            return null;
        }
    }
}
