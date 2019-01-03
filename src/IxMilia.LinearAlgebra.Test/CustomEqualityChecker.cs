using System;

namespace IxMilia.LinearAlgebra.Test
{
    internal class CustomEqualityChecker : IDisposable
    {
        private Func<double, double, bool> _lastEqualityChecker;

        public CustomEqualityChecker(Func<double, double, bool> equalityChecker)
        {
            _lastEqualityChecker = Matrix.ItemEqualityChecker;
            Matrix.ItemEqualityChecker = equalityChecker;
        }

        public void Dispose()
        {
            Matrix.ItemEqualityChecker = _lastEqualityChecker;
        }
    }
}
