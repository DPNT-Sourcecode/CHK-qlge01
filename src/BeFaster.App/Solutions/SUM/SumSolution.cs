namespace BeFaster.App.Solutions.SUM {
    public class SumSolution {
        public int Sum(int x, int y) {
            if (0 <= x && 0 <= y && x <= 100 && y <= 100) {
                return x + y;
            }

            return -1;
        }
    }

