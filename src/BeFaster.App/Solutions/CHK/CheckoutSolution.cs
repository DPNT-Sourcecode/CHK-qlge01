namespace BeFaster.App.Solutions.CHK {
    public class CheckoutSolution {
        public int Checkout(string? skus) {
            Dictionary<char, int> prices = new Dictionary<char, int> {
                {'A', 50},
                {'B', 30},
                {'C', 20},
                {'D', 15}
            };

            Dictionary<char, (int amount, int price)> offers = new Dictionary<char, (int amount, int price)> {
                {'A', (3, 130)},
                {'B', (2, 45)}
            };
            return -1;
        }
    }
}

