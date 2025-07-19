using BeFaster.App.Solutions.CHK;

namespace BeFaster.App.Tests.Solutions.CHK {
    [TestFixture]
    public class ChkSolutionTest {
        [TestCase("E", ExpectedResult = 40)]
        public int ComputeCheckout(string skus) {
            return new CheckoutSolution().Checkout(skus);
        }
    }
}

