namespace BeFaster.App.Solutions.CHK {
    public class CheckoutSolution {
        public int Checkout(string? skus) {
            Dictionary<char, int> prices = new Dictionary<char, int> {
                {'A', 50},
                {'B', 30},
                {'C', 20},
                {'D', 15},
                {'E', 40}
            };

            Dictionary<char, List<(int amount, int price)>> offers = new Dictionary<char, List<(int amount, int price)>> {
                {'A', new List<(int, int)>{ (5, 200), (3, 130) } },
                {'B', new List<(int, int)>{ (2, 45)}}
            };

            Dictionary<char, (int amount, int freeAmount)> freeOffers = new Dictionary<char, (int amount, int freeAmount)> {
                {'E', (2, 1)}
            };


            Dictionary<char, int> shopping = new Dictionary<char, int>();

            int total = 0;

            foreach (char item in skus) {

                if (!prices.ContainsKey(item)) {
                    return -1;
                }

                if (!shopping.ContainsKey(item)) {
                    shopping[item] = 0;
                }

                shopping[item]++;
            }

            foreach (var typePair in shopping) {
                char item = typePair.Key;
                int amount = typePair.Value;

                if (freeOffers.TryGetValue(item, out var freeOffer)) {
                    int offerSize = freeOffer.amount + freeOffer.freeAmount;
                    int offerQuantity = amount / offerSize;
                    int remainder = amount % offerSize;

                    int paidAmount = offerQuantity * freeOffer.amount + Math.Min(remainder, freeOffer.freeAmount);
                    amount = paidAmount;
                }

                if (offers.TryGetValue(item, out var itemOffers)) {
                    itemOffers.Sort((a, b) => b.amount.CompareTo(a.amount));
                    foreach (var offer in itemOffers) {
                        int quantity = offer.amount;
                        int price = offer.price;

                        int offerQuantity = amount / quantity;
                        total += offerQuantity * price;

                        amount %= quantity;

                    }
                }

                if (offers.ContainsKey(item)) {
                    total += amount * prices[item];
                }
            }

            return total;
        }
    }
}






