using System;
using System.Collections;

namespace Sorting {
    public class StoogeSorter : CompareSorter, IEquatable<StoogeSorter> {
        private static StoogeSorter? SINGLETON = null;

        private StoogeSorter() {
        }

        public static StoogeSorter Singleton {
            get {
                if (StoogeSorter.SINGLETON is null) {
                    StoogeSorter.SINGLETON = new StoogeSorter();
                }

                return StoogeSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList list, int low, int high, IComparer comparer) {
            if (comparer.Compare(list[high], list[low]) < 0) {
                SortUtils.Swap(list, low, high);
            }

            int size = high - low + 1;

            if (size > 2) {
                int third = size / 3;

                this.DoSort(list, low, high - third, comparer);
                this.DoSort(list, low + third, high, comparer);
                this.DoSort(list, low, high - third, comparer);
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as StoogeSorter);
        }

        public bool Equals(StoogeSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = this.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
