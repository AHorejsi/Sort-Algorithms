using System.Collections;

namespace Sorting {
    public class SelectionSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high; ++index) {
                int minIndex = SortingUtils.MinIndex(list, index, high, comparer);

                if (minIndex != index) {
                    SortingUtils.Swap(list, index, minIndex);
                }
            }
        }
    }
}
