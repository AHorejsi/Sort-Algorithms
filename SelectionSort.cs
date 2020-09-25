using System.Collections;

namespace Sorting {
    public class SelectionSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high; ++index) {
                int minIndex = SortUtils.MinIndex(list, index, high, comparer);

                if (minIndex != index) {
                    SortUtils.Swap(list, index, minIndex);
                }
            }
        }
    }
}
