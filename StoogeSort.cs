using System.Collections.Generic;

namespace Sorting {
    public sealed class StoogeSorter<N> : ICompareSorter<N> {
        public StoogeSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList<N> list, int low, int high, IComparer<N> comparer) {
            if (comparer.Compare(list[high], list[low]) < 0) {
                SortUtils.Swap(list, low, high);
            }

            int size = high - low + 1;

            if (size > 2) {
                int third = size / 3;

                this.DoSort(list, low, high - third, comparer);
                this.DoSort(list, low + third, high, comparer);
                this.DoSort(list, low, high - third, comparer);
            }
        }
    }
}
