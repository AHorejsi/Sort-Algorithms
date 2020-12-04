using System.Collections.Generic;

namespace Sorting {
    public sealed class GnomeSorter<N> : ICompareSorter<N> {
        public GnomeSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            int index = low;

            while (index < high) {
                if (low == index) {
                    ++index;
                }

                if (comparer.Compare(list[index], list[index - 1]) >= 0) {
                    ++index;
                }
                else {
                    SortUtils.Swap(list, index, index - 1);
                    --index;
                }
            }
        }
    }
}
