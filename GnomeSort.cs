using System.Collections;

namespace Sorting {
    public class GnomeSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            int index = low;

            while (index < high) {
                if (0 == index) {
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
