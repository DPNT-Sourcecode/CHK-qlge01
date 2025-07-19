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

        List<GroupOffer> groupOffers = new List<GroupOffer> {
            new GroupOffer(new [] {'S', 'T', 'X', 'Y', 'Z'}, 3, 45)
        };
        public int Checkout(string? skus) {
            Dictionary<char, int> shopping = new Dictionary<char, int>();

            int total = 0;

            // Process basket
            foreach (char item in skus) {

                if (!products.ContainsKey(item)) {
                    return -1;
                }

                if (!shopping.ContainsKey(item)) {
                    shopping[item] = 0;
                }

                shopping[item]++;
            }

            // Calculate offers and price

            // Group Offers
            foreach (GroupOffer groupOffer in groupOffers) {
                var items = shopping.Where(kv => groupOffer.SKUs.Contains(kv.Key) && kv.Value > 0).SelectMany(kv => Enumerable.Repeat(kv.Key, kv.Value)).ToList();

                items.Sort((a, b) => products[b].Price.CompareTo(products[a].Price));

                int count = items.Count() / groupOffer.RequiredAmount;
                total += count * groupOffer.Price;

                for (int i = 0; i < count * groupOffer.RequiredAmount; i++) {
                    char sku = items[i];
                    shopping[sku]--;
                }
            }

            foreach (var (sku, product) in products) {
                // Free Offers
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

            // Normal Offers and pricing
            foreach (var typePair in shopping) {
                char item = typePair.Key;
                int amount = typePair.Value;

                Product product = products[item];

                var sortedOffers = product.Offers.OrderByDescending(offer => offer.Price);

                foreach (var (offerAmount, offerPrice) in sortedOffers) {
                    int offerQuantity = amount / offerAmount;
                    total += offerQuantity * offerPrice;

                    amount %= offerAmount;
                }

                total += amount * product.Price;
            }

            return total;
        }
    }
}
