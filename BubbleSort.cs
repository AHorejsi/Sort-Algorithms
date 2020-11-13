using System;
using System.Collections;

namespace Sorting {
    public class BubbleSorter : ICompareSorter, IEquatable<BubbleSorter> {
        private static BubbleSorter? instance = null;

        private BubbleSorter() {
        }

        public static BubbleSorter Singleton {
            get {
                if (BubbleSorter.instance is null) {
                    BubbleSorter.instance = new BubbleSorter();
                }

                return BubbleSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            for (int i = low + 1; i < high; ++i) {
                bool swapped = false;
                int j = low;
                int k = j + 1;

                while (k < high) {
                    if (comparer.Compare(list[j], list[k]) > 0) {
                        SortUtils.Swap(list, j, k);
                        swapped = true;
                    }

                    ++j;
                    ++k;
                }

                if (!swapped) {
                    return;
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as BubbleSorter);
        }

        public bool Equals(BubbleSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
