using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public static class ExtensionMethods {
        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, Comparison<E> comparison) {
            sorter.Sort(list, comparison);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, IComparer<E> comparer) {
            sorter.Sort(list, comparer);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, Comparison<E> comparison, int low, int high) {
            sorter.Sort(list, low, high, comparison);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, IComparer<E> comparer, int low, int high) {
            sorter.Sort(list, low, high, comparer);
        }

        public static void Sort<E>(this IList<E> list, IIntegerSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this IList<E> list, IIntegerSorter<E> sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this IList<E> list, IFloatSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this IList<E> list, IFloatSorter<E> sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedCompareSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedCompareSorter<E> sorter, Comparison<E> comparison) {
            sorter.Sort(list, comparison);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedCompareSorter<E> sorter, IComparer<E> comparer) {
            sorter.Sort(list, comparer);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedCompareSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            SortUtils.CheckList(list, first, last);
            sorter.Sort(first, last);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedCompareSorter<E> sorter, Comparison<E> comparison, LinkedListNode<E> first, LinkedListNode<E> last) {
            SortUtils.CheckList(list, first, last);
            sorter.Sort(first, last, comparison);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedCompareSorter<E> sorter, IComparer<E> comparer, LinkedListNode<E> first, LinkedListNode<E> last) {
            SortUtils.CheckList(list, first, last);
            sorter.Sort(first, last, comparer);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedIntegerSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedIntegerSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            SortUtils.CheckList(list, first, last);
            sorter.Sort(first, last);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedFloatSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedFloatSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            SortUtils.CheckList(list, first, last);
            sorter.Sort(first, last);
        }
    }
}
