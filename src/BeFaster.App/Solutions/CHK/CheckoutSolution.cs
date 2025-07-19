namespace BeFaster.App.Solutions.CHK {
    public class CheckoutSolution {
        static Product FreeOfferHelper(Product product, int amount, int freeAmount, char freeSKU) {
            product.FreeOffer = (amount, freeAmount, freeSKU);
            return product;
        }
        Dictionary<char, Product> products = new Dictionary<char, Product> {
            ['A'] = new Product('A', 50) { Offers = { (5, 200), (3, 130) } },
            ['B'] = new Product('B', 30) { Offers = { (2, 45) } },
            ['C'] = new Product('C', 20),
            ['D'] = new Product('D', 15),
            ['E'] = FreeOfferHelper(new Product('E', 40), 2, 1, 'B'),
            ['F'] = FreeOfferHelper(new Product('F', 10), 2, 1, 'F'),
            ['G'] = new Product('G', 20),
            ['H'] = new Product('H', 10) { Offers = { (10, 80), (5, 45) } },
            ['I'] = new Product('I', 35),
            ['J'] = new Product('J', 60),
            ['K'] = new Product('K', 80) { Offers = { (2, 150) } },
            ['L'] = new Product('L', 90),
            ['M'] = new Product('M', 15),
            ['N'] = FreeOfferHelper(new Product('N', 40), 3, 1, 'M'),
            ['O'] = new Product('O', 10),
            ['P'] = new Product('P', 50) { Offers = { (5, 200) } },
            ['Q'] = new Product('Q', 30) { Offers = { (3, 80) } },
            ['R'] = FreeOfferHelper(new Product('R', 50), 3, 1, 'Q'),
            ['S'] = new Product('S', 30),
            ['T'] = new Product('T', 20),
            ['U'] = FreeOfferHelper(new Product('U', 40), 3, 1, 'U'),
            ['V'] = new Product('V', 50) { Offers = { (3, 130), (2, 90) } },
            ['W'] = new Product('W', 20),
            ['X'] = new Product('X', 90),
            ['Y'] = new Product('Y', 10),
            ['Z'] = new Product('Z', 50),
        };
        public int Checkout(string? skus) {
            Dictionary<char, int> prices = new Dictionary<char, int> {
                {'A', 50},
                {'B', 30},
                {'C', 20},
                {'D', 15},
                {'E', 40},
                {'F', 10}
            };

            Dictionary<char, List<(int amount, int price)>> offers = new Dictionary<char, List<(int amount, int price)>> {
                {'A', new List<(int, int)>{ (5, 200), (3, 130) } },
                {'B', new List<(int, int)>{ (2, 45)}}
            };

            Dictionary<char, (int amount, int freeAmount, char freeItem)> freeOffers = new Dictionary<char, (int amount, int freeAmount, char freeItem)> {
                {'E', (2, 1, 'B')},
                {'F', (2, 1, 'F')}
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

            foreach (var (sku, product) in products) {
                if (!product.FreeOffer.HasValue) {
                    continue;
                }
                var (amount, freeAmount, freeItem) = product.FreeOffer.Value;

                if (shopping.TryGetValue(sku, out int promoCount)) {
                    if (sku == freeItem) {
                        int size = amount + freeAmount;
                        int totalFree = (promoCount / size) * freeAmount;

                        shopping[sku] = Math.Max(0, promoCount - totalFree);
                    } else {
                        if (shopping.TryGetValue(freeItem, out int freeCount)) {
                            int totalFree = (promoCount / amount) * freeAmount;
                            shopping[freeItem] = Math.Max(0, freeCount - totalFree);
                        }
                    }
                }
            }

            foreach (var typePair in shopping) {
                char item = typePair.Key;
                int amount = typePair.Value;

                if (offers.TryGetValue(item, out var itemOffers)) {
                    itemOffers.Sort((a, b) => b.amount.CompareTo(a.amount));

                    foreach (var offer in itemOffers) {
                        int offerQuantity = amount / offer.amount;
                        total += offerQuantity * offer.price;

                        amount %= offer.amount;
                    }
                }

                total += amount * prices[item];
            }

            return total;
        }
    }
}

