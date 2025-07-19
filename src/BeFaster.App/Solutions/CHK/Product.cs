namespace BeFaster.App.Solutions.CHK {
    internal class Product {
        public char SKU { get; }
        public int Price { get; }

        public List<(int Amount, int Price)> Offers { get; } = new List<(int, int)>();
        public (int Amount, int FreeAmount, char FreeSKU)? FreeOffer { get; set; }

        public Product(char sku, int price) {
            SKU = sku;
            Price = price;
        }
    }
}



