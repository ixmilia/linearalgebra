using Xunit;

namespace IxMilia.LinearAlgebra.Test
{
    public class AlgebraicComputerTests
    {
        [Fact]
        public void DeterminantEquationIsCorrect()
        {
            var matrix = new Matrix<string>(StringAlgebraicComputer.Instance, 2, 2,
                "a", "b",
                "c", "d");
            var det = matrix.Determinant;
            Assert.Equal("((a * d) - (b * c))", det);
        }

        [Fact]
        public void InverseEquationIsCorrect()
        {
            var matrix = new Matrix<string>(StringAlgebraicComputer.Instance, 2, 2,
                "a", "b",
                "c", "d");
            var inv = matrix.Inverse;
            var expected = new Matrix<string>(StringAlgebraicComputer.Instance, 2, 2,
                "((1 / ((a * d) - (b * c))) * d)", "((1 / ((a * d) - (b * c))) * -(b))",
                "((1 / ((a * d) - (b * c))) * -(c))", "((1 / ((a * d) - (b * c))) * a)");
            Assert.Equal(expected, inv);
        }
    }

    internal class StringAlgebraicComputer : IAlgebraicComputer<string>
    {
        public static StringAlgebraicComputer Instance = new StringAlgebraicComputer();

        public string Zero => "0";

        public string One => "1";

        public string Epsilon => "0";

        public string AbsoluteValue(string a) => $"|{a}|";

        public string Add(string a, string b) => $"({a} + {b})";

        public bool AreEqual(string a, string b) => a == b;

        public string Divide(string a, string b) => $"({a} / {b})";

        public bool IsLess(string a, string b) => string.Compare(a, b) < 0;

        public string Multiply(string a, string b) => $"({a} * {b})";

        public string Negate(string a) => $"-({a})";

        public string Pow(string b, string e) => $"({b})^({e})";

        public string SquareRoot(string a) => $"sqrt({a})";

        public string Subtract(string a, string b) => $"({a} - {b})";
    }
}
