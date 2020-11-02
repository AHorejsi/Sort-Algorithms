using System;
using System.Collections;

namespace Sorting {
    public class ShellSorter : CompareSorter, IEquatable<ShellSorter> {
        private static ShellSorter? SINGLETON = null;

        private ShellSorter() {
        }

        public static ShellSorter Singleton {
            get {
                if (ShellSorter.SINGLETON is null) {
                    ShellSorter.SINGLETON = new ShellSorter();
                }

                return ShellSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            int size = high - low;

            for (int gap = size / 2; gap > 0; gap /= 2) {
                for (int i = gap; i < high; ++i) {
                    object? temp = list[i];
                    int j;

                    for (j = i; j >= gap && comparer.Compare(temp, list[j - gap]) < 0; j -= gap) {
                        list[j] = list[j - gap];
                    }

                    list[j] = temp;
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as ShellSorter);
        }

        public bool Equals(ShellSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
