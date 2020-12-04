using System.Collections.Generic;

namespace Sorting {
    public sealed class BubbleSorter<N> : ICompareSorter<N> {
        public BubbleSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            for (int i = low + 1; i < high; ++i) {
                bool swapped = false;
                int j = low;
                int k = j + 1;

                while (k < high) {
                    if (comparer.Compare(list[j], list[k]) > 0) {
                        SortUtils.Swap(list, j, k);
                        swapped = true;
                    }

                    ++j;
                    ++k;
                }

                if (!swapped) {
                    return;
                }
            }
        }
    }
}
