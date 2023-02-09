using System;

namespace IxMilia.LinearAlgebra
{
    public interface IAlgebraicComputer<T>
    {
        T Add(T a, T b);
        T Subtract(T a, T b);
        T Multiply(T a, T b);
        T Divide(T a, T b);
        T Negate(T a);
        T AbsoluteValue(T a);
        T Pow(T b, T e);

        bool AreEqual(T a, T b);
        bool IsLess(T a, T b);
        T SquareRoot(T a);
        T Zero { get; }
        T One { get; }
        T Epsilon { get; }
    }

    public static class IAlgebraicComputerExtensions
    {
        public static T Add<T>(this IAlgebraicComputer<T> computer, T a, T b, T next, params T[] rest)
        {
            var result = computer.Add(a, b);
            result = computer.Add(result, next);
            foreach (var value in rest)
            {
                result = computer.Add(result, value);
            }

            return result;
        }

        public static T Multiply<T>(this IAlgebraicComputer<T> computer, T a, T b, T next, params T[] rest)
        {
            var result = computer.Multiply(a, b);
            result = computer.Multiply(result, next);
            foreach (var value in rest)
            {
                result = computer.Multiply(result, value);
            }

            return result;
        }

        public static T CreateInt<T>(this IAlgebraicComputer<T> computer, int value)
        {
            // this is really gross
            if (value == 0)
            {
                return computer.Zero;
            }

            if (value == 1)
            {
                return computer.One;
            }

            var result = computer.Zero;
            for (int i = 0; i < Math.Abs(value); i++)
            {
                result = computer.Add(result, computer.One);
            }

            if (value < 0)
            {
                result = computer.Negate(result);
            }

            return result;
        }

        public static bool AreClose<T>(this IAlgebraicComputer<T> computer, T a, T b) => computer.IsLess(computer.AbsoluteValue(computer.Subtract(a, b)), computer.Epsilon);
        public static bool IsZero<T>(this IAlgebraicComputer<T> computer, T value) => computer.AreEqual(value, computer.Zero);
        public static bool IsOne<T>(this IAlgebraicComputer<T> computer, T value) => computer.AreEqual(value, computer.One);
        public static bool IsGreater<T>(this IAlgebraicComputer<T> computer, T a, T b) => computer.IsLess(b, a) && !computer.AreEqual(a, b);
        public static bool IsNegative<T>(this IAlgebraicComputer<T> computer, T value) => computer.IsLess(value, computer.Zero);
        public static bool IsPositive<T>(this IAlgebraicComputer<T> computer, T value) => computer.IsLess(computer.Zero, value);
        public static T Square<T>(this IAlgebraicComputer<T> computer, T value) => computer.Multiply(value, value);
        public static T Two<T>(this IAlgebraicComputer<T> computer) => computer.Add(computer.One, computer.One);
    }
}
