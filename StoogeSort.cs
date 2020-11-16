using System;
using System.Collections;

namespace Sorting {
    public sealed class StoogeSorter : ICompareSorter, IEquatable<StoogeSorter> {
        private static StoogeSorter? instance = null;

        private StoogeSorter() {
        }

        public static StoogeSorter Singleton {
            get {
                if (StoogeSorter.instance is null) {
                    StoogeSorter.instance = new StoogeSorter();
                }

                return StoogeSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            SortUtils.CheckRange(low, high);

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
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
