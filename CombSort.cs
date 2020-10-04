using System;
using System.Collections;

namespace Sorting {
    public class CombSorter : CompareSorter, IEquatable<CombSorter> {
        private static CombSorter SINGLETON = null;

        private CombSorter() { 
        }

        public static CombSorter Instance {
            get { 
                if (CombSorter.SINGLETON is null) {
                    CombSorter.SINGLETON = new CombSorter();
                }

                return CombSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            int gap = high - low;
            bool swapped = true;

            while (gap > 1 || swapped) {
                gap = this.NextGap(gap);
                swapped = false;
                int end = high - gap;

                for (int index = low; index < end; ++index) {
                    int nextIndex = index + gap;

                    if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                        SortUtils.Swap(list, index, nextIndex);
                        swapped = true;
                    }
                }
            }
        }

        private int NextGap(int gap) {
            gap = (gap * 10) / 13;

            return (gap < 1) ? 1 : gap;
        }

        public override bool Equals(object obj) {
            return this.Equals(obj as CombSorter);
        }

        public bool Equals(CombSorter sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
