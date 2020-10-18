using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public class BubbleSorter<T> : CompareSorter<T>, IEquatable<BubbleSorter<T>> {
        private static BubbleSorter<T>? SINGLETON = null;

        private BubbleSorter() {
        }

        public static BubbleSorter<T> Singleton {
            get {
                if (BubbleSorter<T>.SINGLETON is null) {
                    BubbleSorter<T>.SINGLETON = new BubbleSorter<T>();
                }

                return BubbleSorter<T>.SINGLETON;
            }
        }

        public override void Sort<R>(IList<R> list, int low, int high, IComparer<R> comparer) {
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
            return this.Equals(obj as BubbleSorter<T>);
        }

        public bool Equals(BubbleSorter<T>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
