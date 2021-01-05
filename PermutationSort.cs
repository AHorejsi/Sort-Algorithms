using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class PermutationSorter<N> : ICompareSorter<N>, IEquatable<PermutationSorter<N>> {
        public PermutationSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            while (!SortUtils.IsSorted(list, low, high, comparer)) {
                this.NextPermutation(list, low, high, comparer);
            }
        }

        private void NextPermutation(IList<N> list, int low, int high, IComparer<N> comparer) {
            int i = high - 1;

            while (true) {
                int j = i;
                int k;

                --i;

                if (comparer.Compare(list[i], list[j]) < 0) {
                    k = high;

                    do {
                        --k;
                    } while (comparer.Compare(list[i], list[k]) >= 0);

                    SortUtils.Swap(list, i, k);
                    this.Reverse(list, j, high);

                    return;
                }

                if (i == low) {
                    this.Reverse(list, low, high);

                    return;
                }
            }
        }

        private void Reverse(IList<N> list, int low, int high) {
            --high;

            while (low < high) {
                SortUtils.Swap(list, low, high);

                ++low;
                --high;
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as PermutationSorter<N>);
        }

        public bool Equals(PermutationSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
