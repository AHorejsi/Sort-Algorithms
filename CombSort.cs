using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class CombSorter<N> : ICompareSorter<N>, IEquatable<CombSorter<N>> {
        public CombSorter() {
        }

        unsafe public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            int gap = high - low;
            var swapped = true;

            while (gap > 1 || swapped) {
                this.NextGap(&gap);
                swapped = false;
                int end = high - gap;


                for (int index = low; index < end; ++index) {
                    int nextIndex = index + gap;

                    if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                        SortUtils.Swap(list, index, nextIndex);
                        swapped = true;
                    }
                }
            }
        }

        unsafe private void NextGap(int* gap) {
            *gap = (*gap * 10) / 13;

            if (*gap < 1) {
                *gap = 1;
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as CombSorter<N>);
        }

        public bool Equals(CombSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
