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


            Dictionary<char, int> shopping = new Dictionary<char, int>();

            foreach (char item in skus) {
                if (!shopping.ContainsKey(item)) {
                    shopping[item] = 0;
                }

                shopping[item]++;
            }
            return -1;
        }
    }
}


