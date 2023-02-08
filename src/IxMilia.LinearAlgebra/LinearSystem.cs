using System;

namespace IxMilia.LinearAlgebra
{
    public class LinearSystem<T>
    {
        public Matrix<T> Coefficients { get; }
        public Matrix<T> Constants { get; }

        public LinearSystem(Matrix<T> coefficients, Matrix<T> constants)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException(nameof(coefficients));
            }

            if (constants == null)
            {
                throw new ArgumentNullException(nameof(constants));
            }

            if (!coefficients.IsSquare || coefficients.Rows < 2)
            {
                throw new InvalidOperationException("Coefficient matrix must be square with a rank >= 2.");
            }

            if (!constants.IsColumnVector || constants.Rows != coefficients.Rows)
            {
                throw new InvalidOperationException("Constant matrix must be a column vector with the same number of rows as the coefficient matrix.");
            }

            Coefficients = coefficients;
            Constants = constants;
        }

        public Matrix<T> Solve()
        {
            var inv = Coefficients.Inverse;
            if (inv == null)
            {
                return null;
            }

            var result = inv * Constants;
            return result;
        }
    }
}
