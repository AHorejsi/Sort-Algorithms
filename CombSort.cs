using System.Collections;

namespace Sorting {
    public class CombSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
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
