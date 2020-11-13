using System;
using System.Collections;

namespace Sorting {
    public class CocktailSorter : ICompareSorter, IEquatable<CocktailSorter> {
        private static CocktailSorter? instance = null;

        private CocktailSorter() {
        }

        public static CocktailSorter Singleton {
            get {
                if (CocktailSorter.instance is null) {
                    CocktailSorter.instance = new CocktailSorter();
                }

                return CocktailSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
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

        private bool BubbleUp(IList list, int low, int high, IComparer comparer) {
            bool swapped = false;

            for (int i = low; i < high; ++i) {
                int j = i + 1;

                if (comparer.Compare(list[j], list[i]) < 0) {
                    SortUtils.Swap(list, i, j);
                    swapped = true;
                }
            }

            return swapped;
        }

        private bool BubbleDown(IList list, int low, int high, IComparer comparer) {
            bool swapped = false;

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
            return this.Equals(obj as CocktailSorter);
        }

        public bool Equals(CocktailSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
