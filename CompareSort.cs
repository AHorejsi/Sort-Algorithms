using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting {
    /// <summary>
    ///     Represents an algorithm that uses comparison as the basis for sorting
    /// </summary>
    public abstract class CompareSorter {
        /// <summary>
        ///     Sorts the given list using the <c>IComparable</c> interface
        /// </summary>
        /// <param name="list">
        ///     The list to be sorted
        /// </param>
        public void Sort(IList list) {
            this.Sort(list, Comparer.Default);
        }

        public void Sort(IList list, IComparer comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        public void Sort(IList list, int low, int high) {
            this.Sort(list, low, high, Comparer.Default);
        }

        public abstract void Sort(IList list, int low, int high, IComparer comparer);

        public abstract override bool Equals(object obj);

        public static bool operator ==(CompareSorter sorter1, CompareSorter sorter2) {
            return sorter1.Equals(sorter2);
        }

        public static bool operator !=(CompareSorter sorter1, CompareSorter sorter2) {
            return !(sorter1 == sorter2);
        }

        //public abstract override int GetHashCode();

        public static void Sort<T>(CompareSorter sorter, IList<T> list) where T : IComparable<T> {
            CompareSorter.Sort<T>(sorter, list, Comparer<T>.Default);
        }

        public static void Sort<T>(CompareSorter sorter, IList<T> list, Comparison<T> comparison) {
            CompareSorter.Sort<T>(sorter, list, Comparer<T>.Create(comparison));
        }

        public static void Sort<T>(CompareSorter sorter, IList<T> list, IComparer<T> comparer) {
            CompareSorter.Sort<T>(sorter, list, 0, list.Count, comparer);
        }

        public static void Sort<T>(CompareSorter sorter, IList<T> list, int low, int high) where T : IComparable<T> {
            CompareSorter.Sort<T>(sorter, list, low, high, Comparer<T>.Default);
        }

        public static void Sort<T>(CompareSorter sorter, IList<T> list, int low, int high, Comparison<T> comparison) {
            CompareSorter.Sort<T>(sorter, list, low, high, Comparer<T>.Create(comparison));
        }

        public static void Sort<T>(CompareSorter sorter, IList<T> list, int low, int high, IComparer<T> comparer) {
            if (!(list is IList)) {
                throw new ArgumentException("Parameter \"list\" is not of the non-generic type IList");
            }
            if (!(comparer is IComparer)) {
                throw new ArgumentException("Parameter \"comparer\" is not of the non-generic type IComparer");
            }

            sorter.Sort((IList)list, low, high, (IComparer)comparer);
        }

        public static bool SortIfSubclass<T>(CompareSorter sorter, IList<T> list) where T : IComparable<T> {
            return CompareSorter.SortIfSubclass<T>(sorter, list, Comparer<T>.Default);
        }

        public static bool SortIfSubclass<T>(CompareSorter sorter, IList<T> list, Comparison<T> comparison) {
            return CompareSorter.SortIfSubclass<T>(sorter, list, Comparer<T>.Create(comparison));
        }

        public static bool SortIfSubclass<T>(CompareSorter sorter, IList<T> list, IComparer<T> comparer) {
            return CompareSorter.SortIfSubclass<T>(sorter, list, 0, list.Count, comparer);
        }

        public static bool SortIfSubclass<T>(CompareSorter sorter, IList<T> list, int low, int high) where T : IComparable<T> {
            return CompareSorter.SortIfSubclass<T>(sorter, list, low, high, Comparer<T>.Default);
        }

        public static bool SortIfSubclass<T>(CompareSorter sorter, IList<T> list, int low, int high, Comparison<T> comparison) {
            return CompareSorter.SortIfSubclass<T>(sorter, list, low, high, Comparer<T>.Create(comparison));
        }

        public static bool SortIfSubclass<T>(CompareSorter sorter, IList<T> list, int low, int high, IComparer<T> comparer) {
            if (list is IList nongenericList && comparer is IComparer nongenericComparer) {
                sorter.Sort(nongenericList, low, high, nongenericComparer);

                return true;
            }
            else {
                return false;
            }
        }
    }
}
