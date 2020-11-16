using System;
using System.Collections;

namespace Sorting {
    public sealed class CombSorter : ICompareSorter, IEquatable<CombSorter> {
        private static CombSorter? instance = null;

        private CombSorter() { 
        }

        public static CombSorter Singleton {
            get { 
                if (CombSorter.instance is null) {
                    CombSorter.instance = new CombSorter();
                }

                return CombSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            SortUtils.CheckRange(low, high);

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

        public override bool Equals(object? obj) {
            return this.Equals(obj as CombSorter);
        }

        public bool Equals(CombSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
