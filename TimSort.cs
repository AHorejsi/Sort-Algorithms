using System;
using System.Collections.Generic;

namespace Sorting {
    public class TimSorter<N> : ICompareSorter<N>, IEquatable<TimSorter<N>> {
        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as TimSorter<N>);
        }

        public bool Equals(TimSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
