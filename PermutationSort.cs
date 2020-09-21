using System.Collections;

namespace Sorting {
    public class PermutationSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            while (!SortingUtils.IsSorted(list, low, high, comparer)) {
                this.NextPermutation(list, low, high, comparer);
            }
        }

        private void NextPermutation(IList list, int low, int high, IComparer comparer) {
            if (high - low > 1) {
                int index1 = high - 1;

                while (true) {
                    int index2 = index1;
                    int index3;

                    --index1;

                    if (comparer.Compare(list[index1], list[index2]) < 0) {
                        index3 = high;

                        do {
                            --index3;
                        } while (comparer.Compare(list[index1], list[index3]) >= 0);

                        SortingUtils.Swap(list, index1, index3);
                        SortingUtils.Reverse(list, index2, high);

                        return;
                    }

                    if (index1 == low) {
                        SortingUtils.Reverse(list, low, high);

                        return;
                    }
                }
            }
        }
    }
}
