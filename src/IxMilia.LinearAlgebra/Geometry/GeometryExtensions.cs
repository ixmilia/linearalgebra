namespace IxMilia.LinearAlgebra.Geometry
{
    public static class GeometryExtensions
    {
        public static Line2<T> AsLine2<T>(this Matrix<T> matrix)
        {
            if (matrix.Rows == 3 && matrix.Columns == 1)
            {
                return new Line2<T>(matrix.Computer, matrix[0, 0], matrix[1, 0], matrix[2, 0]);
            }

            return null;
        }

        public static Vector2<T> AsVector2<T>(this Matrix<T> matrix)
        {
            if ((matrix.Rows == 2 || matrix.Rows == 3) && matrix.Columns == 1)
            {
                return new Vector2<T>(matrix.Computer, matrix[0, 0], matrix[1, 0], matrix.Rows == 3 ? matrix[2, 0] : matrix.Computer.One);
            }

            return null;
        }

        public static Vector3<T> AsVector3<T>(this Matrix<T> matrix)
        {
            if ((matrix.Rows == 3 || matrix.Rows == 4) && matrix.Columns == 1)
            {
                return new Vector3<T>(matrix.Computer, matrix[0, 0], matrix[1, 0], matrix[2, 0], matrix.Rows == 4 ? matrix[3, 0] : matrix.Computer.One);
            }

            return null;
        }
    }
}
