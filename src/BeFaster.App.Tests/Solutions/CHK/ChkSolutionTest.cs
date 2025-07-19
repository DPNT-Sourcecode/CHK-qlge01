using BeFaster.App.Solutions.CHK;

namespace BeFaster.App.Tests.Solutions.CHK {
    [TestFixture]
    public class ChkSolutionTest {
        [TestCase("E", ExpectedResult = 40), TestCase("EE", ExpectedResult = 80), TestCase("EB", ExpectedResult = 70), TestCase("EEB", ExpectedResult = 80), TestCase("EEBB", ExpectedResult = 110), TestCase("AAAAA", ExpectedResult = 200), TestCase("FF", ExpectedResult = 20), TestCase("FFF", ExpectedResult = 20)]
        public int ComputeCheckout(string skus) {
            return new CheckoutSolution().Checkout(skus);
        }
    }
}
