using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class BubbleSorter<N> : ICompareSorter<N>, IEquatable<BubbleSorter<N>> {
        public BubbleSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            for (int i = low + 1; i < high; ++i) {
                var swapped = false;
                int j = low;
                int k = j + 1;

                while (k < high) {
                    if (comparer.Compare(list[j], list[k]) > 0) {
                        SortUtils.Swap(list, j, k);
                        swapped = true;
                    }

                    ++j;
                    ++k;
                }

                if (!swapped) {
                    return;
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as BubbleSorter<N>);
        }

        public bool Equals(BubbleSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
