namespace BeFaster.App.Solutions.CHK {
    internal class GroupOffer {
        public HashSet<char> SKUs { get; }
        public int RequiredAmount { get; }
        public int Price { get; }

        public GroupOffer(IEnumerable<char> skus, int requiredAmount, int price) {
            SKUs = new HashSet<char>(skus);
            RequiredAmount = requiredAmount;
            Price = price;
        }
    }
}
