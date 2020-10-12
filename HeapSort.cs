using System;
using System.Collections;

// Consider different types of heaps
namespace Sorting {
    public delegate void Heapifier(IList list, int index, int size, IComparer comparer);

    public class HeapSorter : CompareSorter, IEquatable<HeapSorter> {
        private readonly Heapifier heapifier;

        internal HeapSorter(Heapifier heapifier) {
            this.heapifier = heapifier;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            for (int index = (list.Count / 2) - 1; index >= 0; --index) {
                this.heapifier(list, index, list.Count, comparer);
            }

            for (int index = list.Count - 1; index > 0; --index) {
                SortUtils.Swap(list, 0, index);
                this.heapifier(list, 0, index, comparer);
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as HeapSorter);
        }

        public bool Equals(HeapSorter? sorter) {
            if (sorter is null) {
                return false;
            }
            else {
                return this.heapifier == sorter.heapifier;
            }
        }
    }

    internal static class Heapifiers {
        public static void Binary(IList list, int index, int size, IComparer comparer) {
            while (true) {
                int indexOfLargest = Heapifiers.GetIndexOfLargest(list, index, size, comparer);

                if (index != indexOfLargest) {
                    SortUtils.Swap(list, index, indexOfLargest);
                    index = indexOfLargest;
                }
                else {
                    break;
                }
            }
        }

        public static void Weak(IList list, int index, int size, IComparer comparer) {
            throw new NotImplementedException();
        }

        public static void Fibonacci(IList list, int index, int size, IComparer comparer) {
            throw new NotImplementedException();
        }

        public static void Leonardo(IList list, int index, int size, IComparer comparer) {
            throw new NotImplementedException();
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

    public enum HeapType { BINARY, WEAK, FIBONACCI, LEONARDO }

    public static class HeapSortFactory {
        public static HeapSorter Make(HeapType type) {
            Heapifier heapifier = type switch {
                HeapType.BINARY => Heapifiers.Binary,
                HeapType.WEAK => Heapifiers.Weak,
                HeapType.FIBONACCI => Heapifiers.Fibonacci,
                HeapType.LEONARDO => Heapifiers.Leonardo,
                _ => throw new InvalidOperationException()
            };

            return new HeapSorter(heapifier);
        }
    }
}
