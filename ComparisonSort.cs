using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting {
    public abstract class ComparisonSorter {
        protected ComparisonSorter() {
        }

        public virtual void Sort(IList list) {
            this.Sort(list, Comparer.Default);
        }

        public virtual void Sort(IList list, IComparer comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        public virtual void Sort(IList list, int low, int high) {
            this.Sort(list, low, high, Comparer.Default);
        }

        public abstract void Sort(IList list, int low, int high, IComparer comparer);

        public static void Sort<T>(ComparisonSorter sorter, IList<T> list) where T : IComparable<T> {
            sorter.Sort((IList)list);
        }

        public static void Sort<T>(ComparisonSorter sorter, IList<T> list, IComparer<T> comparer) {
            sorter.Sort((IList)list, (IComparer)comparer);
        }

        public static void Sort<T>(ComparisonSorter sorter, IList<T> list, int low, int high) where T : IComparable<T> {
            sorter.Sort((IList)list, low, high);
        }

        public static void Sort<T>(ComparisonSorter sorter, IList<T> list, int low, int high, IComparer<T> comparer) {
            sorter.Sort((IList)list, low, high, (IComparer)comparer);
        }

        public static bool SortIfSubclass<T>(ComparisonSorter sorter, IList<T> list) where T : IComparable<T> {
            return ComparisonSorter.SortIfSubclass<T>(sorter, list, Comparer<T>.Default);
        }

        public static bool SortIfSubclass<T>(ComparisonSorter sorter, IList<T> list, IComparer<T> comparer) {
            return ComparisonSorter.SortIfSubclass<T>(sorter, list, 0, list.Count, comparer);
        }

        public static bool SortIfSubclass<T>(ComparisonSorter sorter, IList<T> list, int low, int high) where T : IComparable<T> {
            return ComparisonSorter.SortIfSubclass<T>(sorter, list, low, high, Comparer<T>.Default);
        }

        public static bool SortIfSubclass<T>(ComparisonSorter sorter, IList<T> list, int low, int high, IComparer<T> comparer) {
            if (list is IList) {
                ComparisonSorter.Sort<T>(sorter, list, low, high, comparer);

                return true;
            }
            else {
                return false;
            }
        }
    }
}
