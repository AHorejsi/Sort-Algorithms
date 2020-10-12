using System;
using System.Collections;

namespace Sorting {
    public class GnomeSorter : CompareSorter, IEquatable<GnomeSorter> {
        private static GnomeSorter? SINGLETON = null;

        private GnomeSorter() {
        }

        public static GnomeSorter Singleton {
            get {
                if (GnomeSorter.SINGLETON is null) {
                    GnomeSorter.SINGLETON = new GnomeSorter();
                }

                return GnomeSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            int index = low;

            while (index < high) {
                if (0 == index) {
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
            return base.Equals(obj as GnomeSorter);
        }

        public bool Equals(GnomeSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = this.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
