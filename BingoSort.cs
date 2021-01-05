using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class BingoSorter<N> : ICompareSorter<N>, IEquatable<BingoSorter<N>> {
        public BingoSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            int max = high - low - 1;
            N nextValue = list[max];

            for (int index = max; index >= 0; --index) {
                if (comparer.Compare(nextValue, list[index]) < 0) {
                    nextValue = list[index];
                }
            }

            while (max > 0 && 0 == comparer.Compare(list[max], nextValue)) {
                --max;
            }

            while (max > 0) {
                N value = nextValue;
                nextValue = list[max];

                for (int index = max - 1; index >= 0; --index) {
                    if (0 == comparer.Compare(value, list[index])) {
                        SortUtils.Swap(list, index, max);
                        --max;
                    }
                    else if (comparer.Compare(nextValue, list[index]) < 0) {
                        nextValue = list[index];
                    }
                }

                while (max > 0 && 0 == comparer.Compare(list[max], nextValue)) {
                    --max;
                }
            }
        }

        public override bool Equals(object? obj) => this.Equals(obj as BingoSorter<N>);

        public bool Equals(BingoSorter<N>? sorter) => !(sorter is null);

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
