using System;
using System.Collections;

namespace Sorting {
    public class SelectionSorter : CompareSorter, IEquatable<SelectionSorter> {
        private static SelectionSorter SINGLETON = null;

        private SelectionSorter() {
        }

        public static SelectionSorter Instance {
            get {
                if (SelectionSorter.SINGLETON is null) {
                    SelectionSorter.SINGLETON = new SelectionSorter();
                }

                return SelectionSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high; ++index) {
                int minIndex = this.MinIndex(list, index, high, comparer);

                if (minIndex != index) {
                    SortUtils.Swap(list, index, minIndex);
                }
            }
        }

        private int MinIndex(IList list, int low, int high, IComparer comparer) {
            object minElement = list[low];
            int minIndex = low;

            for (int index = low + 1; index < high; ++index) {
                object current = list[index];

                if (comparer.Compare(current, minElement) < 0) {
                    minElement = current;
                    minIndex = index;
                }
            }

            return minIndex;
        }

        public override bool Equals(object obj) {
            return this.Equals(obj as SelectionSorter);
        }

        public bool Equals(SelectionSorter sorter) {
            return !(sorter is null); 
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
