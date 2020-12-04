using System.Collections.Generic;

namespace Sorting {
    public sealed class CombSorter<N> : ICompareSorter<N> {
        public CombSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            int gap = high - low;
            bool swapped = true;

            while (gap > 1 || swapped) {
                gap = this.NextGap(gap);
                swapped = false;
                int end = high - gap;


                for (int index = low; index < end; ++index) {
                    int nextIndex = index + gap;

                    if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                        SortUtils.Swap(list, index, nextIndex);
                        swapped = true;
                    }
                }
            }
        }

        private int NextGap(int gap) {
            gap = (gap * 10) / 13;

            return (gap < 1) ? 1 : gap;
        }
    }
}
