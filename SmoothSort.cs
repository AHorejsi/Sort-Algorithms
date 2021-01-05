using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class SmoothSorter<N> : ICompareSorter<N>, IEquatable<SmoothSorter<N>> {
        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as SmoothSorter<N>);
        }

        public bool Equals(SmoothSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
