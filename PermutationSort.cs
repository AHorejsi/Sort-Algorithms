using System;
using System.Collections;

namespace Sorting {
    public sealed class PermutationSorter : ICompareSorter, IEquatable<PermutationSorter> {
        private static PermutationSorter? instance = null;

        private PermutationSorter() {
        }

        public static PermutationSorter Singleton {
            get { 
                if (PermutationSorter.instance is null) {
                    PermutationSorter.instance = new PermutationSorter();
                }

                return PermutationSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            SortUtils.CheckRange(low, high);

            while (!SortUtils.IsSorted(list, low, high, comparer)) {
                this.NextPermutation(list, low, high, comparer);
            }
        }

        private void NextPermutation(IList list, int low, int high, IComparer comparer) {
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

        private void Reverse(IList list, int low, int high) {
            --high;

            while (low < high) {
                SortUtils.Swap(list, low, high);

                ++low;
                --high;
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as PermutationSorter);
        }

        public bool Equals(PermutationSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
