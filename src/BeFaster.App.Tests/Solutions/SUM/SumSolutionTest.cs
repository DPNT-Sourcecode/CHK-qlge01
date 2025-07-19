using BeFaster.App.Solutions.SUM;

namespace BeFaster.App.Tests.Solutions.SUM {
    [TestFixture]
    public class SumSolutionTest {
        [TestCase(1, 1, ExpectedResult = 2), TestCase(500, 1, ExpectedResult = -1), TestCase(1, 500, ExpectedResult = -1), TestCase(100, 100, ExpectedResult = 200), TestCase(-101, -101, ExpectedResult = -1)]
        public int ComputeSum(int x, int y) {
            return new SumSolution().Sum(x, y);
        }
    }
}

