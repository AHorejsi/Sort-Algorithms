using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public static class ExtensionMethods {
        public static void Sort<E, F>(this IList<E> list, ICompareSorter<F> sorter) where E : F, IComparable<F> {
            sorter.Sort(list);
        }

        public static void Sort<E, F>(this IList<E> list, ICompareSorter<F> sorter, Comparison<F> comparison) where E : F {
            sorter.Sort(list, comparison);
        }

        public static void Sort<E, F>(this IList<E> list, ICompareSorter<F> sorter, IComparer<F> comparer) where E : F {
            sorter.Sort(list, comparer);
        }

        public static void Sort<E, F>(this IList<E> list, ICompareSorter<F> sorter, int low, int high) where E : F, IComparable<F> {
            sorter.Sort(list, low, high);
        }

        public static void Sort<E, F>(this IList<E> list, ICompareSorter<F> sorter, Comparison<F> comparison, int low, int high) where E : F {
            sorter.Sort(list, low, high, comparison);
        }

        public static void Sort<E, F>(this IList<E> list, ICompareSorter<F> sorter, IComparer<F> comparer, int low, int high) where E : F {
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

        public static void Sort<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter) where E : F, IComparable<F> {
            sorter.Sort(list);
        }

        public static void Sort<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, Comparison<F> comparison) where E : F {
            sorter.Sort(list, comparison);
        }

        public static void Sort<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, IComparer<F> comparer) where E : F {
            sorter.Sort(list, comparer);
        }

        public static void Sort<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, LinkedListNode<E> first, LinkedListNode<E> last) where E : F, IComparable<F> {
            sorter.Sort(first, last);
        }

        public static void Sort<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, Comparison<F> comparison, LinkedListNode<E> first, LinkedListNode<E> last) where E : F {
            sorter.Sort(first, last, comparison);
        }

        public static void Sort<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, IComparer<F> comparer, LinkedListNode<E> first, LinkedListNode<E> last) where E : F {
            sorter.Sort(first, last, comparer);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedIntegerSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedIntegerSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            sorter.Sort(first, last);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedFloatSorter<E> sorter) {
            sorter.Sort(list);
        }

        public static void Sort<E>(this LinkedList<E> list, ILinkedFloatSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            sorter.Sort(first, last);
        }

        public static async Task SortAsync<E, F>(this IList<E> list, ICompareSorter<F> sorter) where E : F, IComparable<F> {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync<E, F>(this IList<E> list, ICompareSorter<F> sorter, Comparison<F> comparison) where E : F {
            await sorter.SortAsync(list, comparison);
        }

        public static async Task SortAsync<E, F>(this IList<E> list, ICompareSorter<F> sorter, IComparer<F> comparer) where E : F {
            await sorter.SortAsync(list, comparer);
        }

        public static async Task SortAsync<E, F>(this IList<E> list, ICompareSorter<F> sorter, int low, int high) where E : F, IComparable<F> {
            await sorter.SortAsync(list, low, high);
        }

        public static async Task SortAsync<E, F>(this IList<E> list, ICompareSorter<F> sorter, Comparison<F> comparison, int low, int high) where E : F {
            await sorter.SortAsync(list, low, high, comparison);
        }

        public static async Task SortAsync<E, F>(this IList<E> list, ICompareSorter<F> sorter, IComparer<F> comparer, int low, int high) where E : F {
            await sorter.SortAsync(list, low, high, comparer);
        }

        public static async Task SortAsync<E>(this IList<E> list, IIntegerSorter<E> sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync<E>(this IList<E> list, IIntegerSorter<E> sorter, int low, int high) {
            await sorter.SortAsync(list, low, high);
        }

        public static async Task SortAsync<E>(this IList<E> list, IFloatSorter<E> sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync<E>(this IList<E> list, IFloatSorter<E> sorter, int low, int high) {
            await sorter.SortAsync(list, low, high);
        }

        public static async Task SortAsync<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter) where E : F, IComparable<F> {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, Comparison<F> comparison) where E : F {
            await sorter.SortAsync(list, comparison);
        }

        public static async Task SortAsync<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, IComparer<F> comparer) where E : F {
            await sorter.SortAsync(list, comparer);
        }

        public static async Task SortAsync<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, LinkedListNode<E> first, LinkedListNode<E> last) where E : F, IComparable<F> {
            await sorter.SortAsync(first, last);
        }

        public static async Task SortAsync<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, Comparison<F> comparison, LinkedListNode<E> first, LinkedListNode<E> last) where E : F {
            await sorter.SortAsync(first, last, comparison);
        }

        public static async Task SortAsync<E, F>(this LinkedList<E> list, ILinkedCompareSorter<F> sorter, IComparer<F> comparer, LinkedListNode<E> first, LinkedListNode<E> last) where E : F {
            await sorter.SortAsync(first, last, comparer);
        }

        public static async Task SortAsync<E>(this LinkedList<E> list, ILinkedIntegerSorter<E> sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync<E>(this LinkedList<E> list, ILinkedIntegerSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            await sorter.SortAsync(first, last);
        }

        public static async Task SortAsync<E>(this LinkedList<E> list, ILinkedFloatSorter<E> sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync<E>(this LinkedList<E> list, ILinkedFloatSorter<E> sorter, LinkedListNode<E> first, LinkedListNode<E> last) {
            await sorter.SortAsync(first, last);
        }
    }
}
