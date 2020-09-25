using System.Collections;

namespace Sorting {
    public class OddEvenSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            bool isSortedOnEvenIndices;
            bool isSortedOnOddIndices;

            do {
                isSortedOnEvenIndices = this.BubbleUpEvenIndices(list, low, high, comparer);
                isSortedOnOddIndices = this.BubbleUpOddIndices(list, low, high, comparer);
            } while (!isSortedOnEvenIndices || !isSortedOnOddIndices);
        }

        private bool BubbleUpEvenIndices(IList list, int low, int high, IComparer comparer) {
            bool isSortedOnEvenIndices = true;

            for (int index = low; index < high; index = index + 2) {
                int nextIndex = index + 1;

                if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                    SortUtils.Swap(list, index, nextIndex);
                    isSortedOnEvenIndices = false;
                }
            }

            return isSortedOnEvenIndices;
        }

        private bool BubbleUpOddIndices(IList list, int low, int high, IComparer comparer) {
            bool isSortedOnOddIndices = true;

            for (int index = low + 1; index < high - 1; index = index + 2) {
                int nextIndex = index + 1;

                if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                    SortUtils.Swap(list, index, nextIndex);
                    isSortedOnOddIndices = false;
                }
            }

            return isSortedOnOddIndices;
        }
    }
}
