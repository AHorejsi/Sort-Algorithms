using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class StoogeSorter<N> : ICompareSorter<N>, IEquatable<StoogeSorter<N>> {
        public StoogeSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList<N> list, int low, int high, IComparer<N> comparer) {
            if (comparer.Compare(list[high], list[low]) < 0) {
                SortUtils.Swap(list, low, high);
            }

            int size = high - low + 1;

            if (size > 2) {
                int third = size / 3;

                this.DoSort(list, low, high - third, comparer);
                this.DoSort(list, low + third, high, comparer);
                this.DoSort(list, low, high - third, comparer);
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as StoogeSorter<N>);
        }

        public bool Equals(StoogeSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
