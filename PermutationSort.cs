using System.Collections;

namespace Sorting {
    public class PermutationSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            while (!SortUtils.IsSorted(list, low, high, comparer)) {
                this.NextPermutation(list, low, high, comparer);
            }
        }

        private void NextPermutation(IList list, int low, int high, IComparer comparer) {
            if (high - low > 1) {
                int i = high - 1;

                while (true) {
                    int j = i;
                    int k;

                    --i;

                    if (comparer.Compare(list[i], list[j]) < 0) {
                        k = high;

                        do {
                            --k;
                        } while (comparer.Compare(list[i], list[k]) >= 0);

                        SortUtils.Swap(list, i, k);
                        SortUtils.Reverse(list, j, high);

                        return;
                    }

                    if (i == low) {
                        SortUtils.Reverse(list, low, high);

                        return;
                    }
                }
            }
        }
    }
}
