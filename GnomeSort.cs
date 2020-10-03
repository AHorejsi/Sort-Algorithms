using System;
using System.Collections;

namespace Sorting {
    public class GnomeSorter : CompareSorter {
        private static GnomeSorter SINGLETON = null;

        private GnomeSorter() {
        }

        public static GnomeSorter Instance {
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
    }
}
