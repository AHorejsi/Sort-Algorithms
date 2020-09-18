using System.Collections;

namespace Sorting {
    public class BubbleSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            if (low < high) {
                for (int index1 = low + 1; index1 < high; ++index1) {
                    bool swapped = false;
                    int index2 = low;
                    int index3 = index2 + 1;

                    while (index3 < high) {
                        if (comparer.Compare(list[index2], list[index3]) > 0) {
                            SortingUtils.Swap(list, index2, index3);
                            swapped = true;
                        }

                        ++index2;
                        ++index3;
                    }

                    if (!swapped) {
                        return;
                    }
                }
            }
        }
    }
}
