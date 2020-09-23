using System;
using System.Collections;

namespace Sorting {
    public delegate void Heapifier(IList list, int index, int size, IComparer comparer);

    public class HeapSorter : ComparisonSorter {
        public Heapifier Heapifier {
            get;
            private set;
        }

        public HeapSorter(Heapifier heapifier) {
            this.Heapifier = heapifier;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            for (int index = (list.Count / 2) - 1; index >= 0; --index) {
                this.Heapifier(list, index, list.Count, comparer);
            }

            for (int index = list.Count - 1; index > 0; --index) {
                SortingUtils.Swap(list, 0, index);
                this.Heapifier(list, 0, index, comparer);
            }
        }
    }

    public static class Heapifiers {
        public static void Iterative(IList list, int index, int size, IComparer comparer) {
            while (true) {
                int indexOfLargest = Heapifiers.GetIndexOfLargest(list, index, size, comparer);

                if (index != indexOfLargest) {
                    SortingUtils.Swap(list, index, indexOfLargest);
                    index = indexOfLargest;
                }
                else {
                    break;
                }
            }
        }

        public static void Recursive(IList list, int index, int size, IComparer comparer) {
            int indexOfLargest = Heapifiers.GetIndexOfLargest(list, index, size, comparer);

            if (index != indexOfLargest) {
                SortingUtils.Swap(list, index, indexOfLargest);
                Heapifiers.Recursive(list, indexOfLargest, size, comparer);
            }
        }

        private static int GetIndexOfLargest(IList list, int index, int size, IComparer comparer) {
            int indexOfLargest = index;
            int indexOfLeft = 2 * index + 1;
            int indexOfRight = 2 * index + 2;

            if (indexOfLeft < size && comparer.Compare(list[indexOfLeft], list[indexOfLargest]) > 0) {
                indexOfLargest = indexOfLeft;
            }

            if (indexOfRight < size && comparer.Compare(list[indexOfRight], list[indexOfLargest]) > 0) {
                indexOfLargest = indexOfRight;
            }

            return indexOfLargest;
        }
    }
}
