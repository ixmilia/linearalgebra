namespace IxMilia.LinearAlgebra.Test
{
    internal class CloseToEqualityChecker : CustomEqualityChecker
    {
        public CloseToEqualityChecker()
            : base(TestBase.AreClose)
        {
        }
    }
}
