using System.Collections;

namespace Sorting {
    public class StoogeSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList list, int low, int high, IComparer comparer) {
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
