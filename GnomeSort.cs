using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class GnomeSorter<N> : ICompareSorter<N>, IEquatable<GnomeSorter<N>> {
        public GnomeSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            int index = low;

            while (index < high) {
                if (low == index) {
                    ++index;
                }

                if (comparer.Compare(list[index], list[index - 1]) >= 0) {
                    ++index;
                }
                else {
                    SortUtils.Swap(list, index, index - 1);
                    --index;
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as GnomeSorter<N>);
        }

        public bool Equals(GnomeSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
