using System.Collections;

namespace Sorting {
    public class CocktailSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            bool swapped;
            --high;

            do {
                swapped = this.BubbleUp(list, low, high, comparer);

                if (!swapped) {
                    return;
                }

                --high;

                swapped = this.BubbleDown(list, low, high, comparer);

                ++low;
            } while (swapped);
        }

        private bool BubbleUp(IList list, int low, int high, IComparer comparer) {
            bool swapped = false;

            for (int i = low; i < high; ++i) {
                int j = i + 1;

                if (comparer.Compare(list[j], list[i]) < 0) {
                    SortUtils.Swap(list, i, j);
                    swapped = true;
                }
            }

            return swapped;
        }

        private bool BubbleDown(IList list, int low, int high, IComparer comparer) {
            bool swapped = false;

            for (int i = high - 1; i >= low; --i) {
                int j = i + 1;

                if (comparer.Compare(list[j], list[i]) < 0) {
                    SortUtils.Swap(list, i, j);
                    swapped = true;
                }
            }

            return swapped;
        }
    }
}
