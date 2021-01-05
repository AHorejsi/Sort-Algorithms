using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class CocktailSorter<N> : ICompareSorter<N>, IEquatable<CocktailSorter<N>> {
        public CocktailSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            bool swapped;
            --high;

            do {
                swapped = this.BubbleUp(list, low, high, comparer);

                if (!swapped) {
                    return;
                }

                --high;

                swapped = this.BubbleDown(list, low, high, comparer);

                ++low;
            } while (swapped);
        }

        private bool BubbleUp(IList<N> list, int low, int high, IComparer<N> comparer) {
            var swapped = false;

            for (int i = low; i < high; ++i) {
                int j = i + 1;

                if (comparer.Compare(list[j], list[i]) < 0) {
                    SortUtils.Swap(list, i, j);
                    swapped = true;
                }
            }

            return swapped;
        }

        private bool BubbleDown(IList<N> list, int low, int high, IComparer<N> comparer) {
            var swapped = false;

            for (int i = high - 1; i >= low; --i) {
                int j = i + 1;

                if (comparer.Compare(list[j], list[i]) < 0) {
                    SortUtils.Swap(list, i, j);
                    swapped = true;
                }
            }

            return swapped;
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as CocktailSorter<N>);
        }

        public bool Equals(CocktailSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}