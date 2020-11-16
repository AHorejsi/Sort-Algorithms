using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public static class ExtensionMethods {
        public static void Sort(this IList list, ICompareSorter sorter) {
            sorter.Sort(list);
        }

        public static void Sort(this IList list, ICompareSorter sorter, Comparison<object?> comparison) {
            sorter.Sort(list, comparison);
        }

        public static void Sort(this IList list, ICompareSorter sorter, IComparer comparer) {
            sorter.Sort(list, comparer);
        }

        public static void Sort(this IList list, ICompareSorter sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList list, ICompareSorter sorter, Comparison<object?> comparison, int low, int high) {
            sorter.Sort(list, low, high, comparison);
        }

        public static void Sort(this IList list, ICompareSorter sorter, IComparer comparer, int low, int high) {
            sorter.Sort(list, low, high, comparer);
        }

        public static void Sort(this IList list, IIntegerSorter sorter) {
            sorter.Sort(list);
        }

        public static void Sort(this IList list, IIntegerSorter sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList list, IFloatSorter sorter) {
            sorter.Sort(list);
        }

        public static void Sort(this IList list, IFloatSorter sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, Comparison<object?> comparison) {
            await sorter.SortAsync(list, comparison);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, IComparer comparer) {
            await sorter.SortAsync(list, comparer);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high) {
            await sorter.SortAsync(list, low, high);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, Comparison<object?> comparison, int low, int high) {
            await sorter.SortAsync(list, low, high, comparison);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, IComparer comparer, int low, int high) {
            await sorter.SortAsync(list, low, high, comparer);
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter, int low, int high) {
            await sorter.SortAsync(list, low, high);
        }

        public static async Task SortAsync(this IList list, IFloatSorter sorter) {
            await sorter.SortAsync(list);
        }

        public static async Task SortAsync(this IList list, IFloatSorter sorter, int low, int high) {
            await sorter.SortAsync(list, low, high);
        }
    }
}
