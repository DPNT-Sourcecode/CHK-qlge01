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

            int total = 0;

            foreach (char item in skus) {
                if (!shopping.ContainsKey(item)) {
                    shopping[item] = 0;
                }

                shopping[item]++;
            }

            foreach (var typePair in shopping) {
                char item = typePair.Key;
                int amount = typePair.Value;

                if (offers.ContainsKey(item)) {
                    var (quanity, price) = offers[item];
                    int offerQuantity = amount / quanity;
                    int remainder = amount % quanity;

                    total += offerQuantity * price + remainder * prices[item];
                }
            }

            return -1;
        }
    }
}



