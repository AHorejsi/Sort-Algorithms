using System;
using System.Collections;

namespace Sorting {
    public class GnomeSorter : ICompareSorter, IEquatable<GnomeSorter> {
        private static GnomeSorter? instance = null;

        private GnomeSorter() {
        }

        public static GnomeSorter Singleton {
            get {
                if (GnomeSorter.instance is null) {
                    GnomeSorter.instance = new GnomeSorter();
                }

                return GnomeSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            int index = low;

            while (index < high) {
                if (low == index) {
                    ++index;
                }

                if (comparer.Compare(list[index], list[index - 1]) >= 0) {
                    ++index;
                }
                else {
                    SortUtils.Swap(list, index, index - 1);
                    --index;
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as GnomeSorter);
        }

        public bool Equals(GnomeSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
