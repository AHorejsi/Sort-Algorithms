using System;
using System.Collections;

namespace Sorting {
    public class BubbleSorter : CompareSorter, IEquatable<BubbleSorter> {
        private static BubbleSorter SINGLETON = null;

        private BubbleSorter() {
        }

        public static BubbleSorter Instance {
            get {
                if (BubbleSorter.SINGLETON is null) {
                    BubbleSorter.SINGLETON = new BubbleSorter();
                }

                return BubbleSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
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

        public override bool Equals(object obj) {
            return this.Equals(obj as BubbleSorter);
        }

        public bool Equals(BubbleSorter sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
