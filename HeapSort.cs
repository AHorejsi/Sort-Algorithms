using System;
using System.Collections.Generic;

namespace Sorting {
    public abstract class HeapSorter<N> : ICompareSorter<N> {
        public abstract void Sort(IList<N> list, int low, int high, IComparer<N> comparer);
    }

    public sealed class BinaryHeapSorter<N> : HeapSorter<N> {
        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            for (int index = (list.Count / 2) - 1; index >= 0; --index) {
                this.Heapify(list, index, list.Count, comparer);
            }

            for (int index = list.Count - 1; index > 0; --index) {
                SortUtils.Swap(list, 0, index);
                this.Heapify(list, 0, index, comparer);
            }
        }

        private void Heapify(IList<N> list, int index, int size, IComparer<N> comparer) {
            while (true) {
                int indexOfLargest = this.GetIndexOfLargest(list, index, size, comparer);

                if (index != indexOfLargest) {
                    SortUtils.Swap(list, index, indexOfLargest);
                    index = indexOfLargest;
                }
                else {
                    break;
                }
            }
        }

        private int GetIndexOfLargest(IList<N> list, int index, int size, IComparer<N> comparer) {
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

    public sealed class WeakHeapSorter<N> : HeapSorter<N> {
        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            throw new NotImplementedException();
        }
    }

    public sealed class FibonacciHeapSorter<N> : HeapSorter<N> {
        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            throw new NotImplementedException();
        }
    }

    public sealed class LeonardoHeapSorter<N> : HeapSorter<N> {
        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            throw new NotImplementedException();
        }
    }

    public enum HeapType { BINARY, WEAK, FIBONACCI, LEONARDO }

    public static class HeapSortFactory<N> {
        public static HeapSorter<N> Make(HeapType type) {
            return type switch {
                HeapType.BINARY => new BinaryHeapSorter<N>(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
