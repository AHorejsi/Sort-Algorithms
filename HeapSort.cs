using System;
using System.Collections.Generic;

namespace Sorting {
    public enum HeapType { BINARY, WEAK }

    public sealed class HeapSorter<N> : ICompareSorter<N>, IEquatable<HeapSorter<N>> {
        private HeapSortAlgorithm<N> algorithm;
        private HeapType heapType;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public HeapSorter(HeapType heapType) {
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            this.HeapType = heapType;
        }

        public HeapType HeapType {
            get => this.heapType;
            set {
                this.heapType = value;
                this.algorithm = value switch {
                    HeapType.BINARY => new BinaryHeapSortAlgorithm<N>(),
                    HeapType.WEAK => new WeakHeapSortAlgorithm<N>(),
                    _ => throw new InvalidOperationException()
                };
            }
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            this.algorithm.DoSort(list, low, high, comparer);
        }

        public override bool Equals(object? obj) => this.Equals(obj as HeapSorter<N>);

        public bool Equals(HeapSorter<N>? sorter) => sorter is null || this.heapType == sorter.heapType;

        public override int GetHashCode() => HashCode.Combine(this.heapType);
    }

    internal abstract class HeapSortAlgorithm<N> {
        public abstract void DoSort(IList<N> list, int low, int high, IComparer<N> comparer);
    }

    internal sealed class BinaryHeapSortAlgorithm<N> : HeapSortAlgorithm<N> {
        public override void DoSort(IList<N> list, int low, int high, IComparer<N> comparer) {
            for (int index = (low + high) / 2; index >= low; --index) {
                this.Heapify(list, low, index, high, comparer);
            }

            for (int index = high - 1; index > low; --index) {
                SortUtils.Swap(list, low, index);
                this.Heapify(list, low, low, index, comparer);
            }
        }

        private void Heapify(IList<N> list, int low, int index, int size, IComparer<N> comparer) {
            int indexOfLargest;

            while (true) {
                indexOfLargest = this.GetIndexOfLargest(list, low, index, size, comparer);

                if (index != indexOfLargest) {
                    SortUtils.Swap(list, index, indexOfLargest);
                    index = indexOfLargest;
                }
                else {
                    break;
                }
            }
        }

        private int GetIndexOfLargest(IList<N> list, int low, int index, int size, IComparer<N> comparer) {
            int indexOfLargest = index;
            int indexOfLeft = 2 * index + 1 - low;
            int indexOfRight = 2 * index + 2 - low;

            if (indexOfLeft < size && comparer.Compare(list[indexOfLeft], list[indexOfLargest]) > 0) {
                indexOfLargest = indexOfLeft;
            }

            if (indexOfRight < size && comparer.Compare(list[indexOfRight], list[indexOfLargest]) > 0) {
                indexOfLargest = indexOfRight;
            }

            return indexOfLargest;
        }
    }

    internal sealed class WeakHeapSortAlgorithm<N> : HeapSortAlgorithm<N> {
        public override void DoSort(IList<N> list, int low, int high, IComparer<N> comparer) {
            var flags = new byte[(high + 7) / 8];

            for (int i = high; i > low; --i) {
                int j = i;

                while ((j & 1) == this.GetFlag(flags, j >> 1)) {
                    j >>= 1;
                }

                int gParent = j >> 1;
                this.Merge(flags, gParent, i, list, comparer);
            }

            for (int i = high - 1; i >= low + 2; --i) {
                SortUtils.Swap(list, low, i);

                int j = low + 1;
                int k = 2 * j + this.GetFlag(flags, j);

                while ((k = 2 * j + this.GetFlag(flags, j)) < i) {
                    j = k;
                }

                while (j > low) {
                    this.Merge(flags, low, j, list, comparer);
                    j >>= 1;
                }
            }

            SortUtils.Swap(list, low, low + 1);
        }

        private int GetFlag(byte[] flags, int x) => (flags[x >> 3] >> (x & 7)) & 1;

        private void Merge(byte[] flags, int i, int j, IList<N> list, IComparer<N> comparer) {
            if (comparer.Compare(list[i], list[j]) < 0) {
                this.ToggleFlag(flags, j);
                SortUtils.Swap(list, i, j);
            }
        }

        private void ToggleFlag(byte[] flags, int x) {
            flags[x >> 3] ^= (byte)(1 << (x & 7));
        }
    }
}
