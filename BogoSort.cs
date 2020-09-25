using System.Collections;

namespace Sorting {
    public delegate void Shuffler(IList list, int low, int high);

    public class BogoSorter : ComparisonSorter {
        public Shuffler Shuffler {
            get;
            private set;
        }

        public BogoSorter(Shuffler shuffler) {
            this.Shuffler = shuffler;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            if (high - low > 1) {
                while (!SortUtils.IsSorted(list, low, high, comparer)) {
                    this.Shuffler(list, low, high);
                }
            }
        }
    }

    public static class Shufflers {
        public static void RandomShuffle(IList list, int low, int high) {
            for (int index = low + 2; index < high; ++index) {
                int randomIndex = SortUtils.RandomInt(low, index);

                if (randomIndex != index) {
                    SortUtils.Swap(list, index, randomIndex);
                }
            }
        }

        public static void SwapTwo(IList list, int low, int high) {
            int randomIndex1 = SortUtils.RandomInt(low, high);
            int randomIndex2 = SortUtils.RandomInt(low, high);

            if (randomIndex1 != randomIndex2) {
                SortUtils.Swap(list, randomIndex1, randomIndex2);
            }
        }
    }
}
