using System.Collections.Generic;

namespace Sorting {
    public sealed class ShellSorter<N> : ICompareSorter<N> {
        public ShellSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            int size = high - low;

            for (int gap = size / 2; gap > 0; gap /= 2) {
                for (int i = gap; i < high; ++i) {
                    N temp = list[i];
                    int j;

                    for (j = i; j >= gap && comparer.Compare(temp, list[j - gap]) < 0; j -= gap) {
                        list[j] = list[j - gap];
                    }

                    list[j] = temp;
                }
            }
        }
    }
}
