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
                while (!SortingUtils.IsSorted(list, low, high, comparer)) {
                    this.Shuffler(list, low, high);
                }
            }
        }
    }

    public static class Permuters {
        public static void RandomShuffle(IList list, int low, int high) {
            for (int index = low + 2; index < high; ++index) {
                int randomIndex = SortingUtils.RandomInt(low, index);

                if (randomIndex != index) {
                    SortingUtils.Swap(list, index, randomIndex);
                }
            }
        }

        public static void SwapTwo(IList list, int low, int high) {
            int randomIndex1 = SortingUtils.RandomInt(low, high);
            int randomIndex2 = SortingUtils.RandomInt(low, high);

            if (randomIndex1 != randomIndex2) {
                SortingUtils.Swap(list, randomIndex1, randomIndex2);
            }
        }
    }
}
