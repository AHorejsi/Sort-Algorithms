using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sorting.Immutable {
    public static class ExtensionMethods {
        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableCompareSorter<E> sorter) {
            return sorter.Sort(list);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableCompareSorter<E> sorter, Comparison<E> comparison) {
            return sorter.Sort(list, comparison);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableCompareSorter<E> sorter, IComparer<E> comparer) {
            return sorter.Sort(list, comparer);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableCompareSorter<E> sorter, int low, int high) {
            return sorter.Sort(list, low, high);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableCompareSorter<E> sorter, Comparison<E> comparison, int low, int high) {
            return sorter.Sort(list, low, high, comparison);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableCompareSorter<E> sorter, IComparer<E> comparer, int low, int high) {
            return sorter.Sort(list, low, high, comparer);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableIntegerSorter<E> sorter) {
            return sorter.Sort(list);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableIntegerSorter<E> sorter, int low, int high) {
            return sorter.Sort(list, low, high);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableFloatSorter<E> sorter) {
            return sorter.Sort(list);
        }

        public static IImmutableList<E> Sort<E>(this IImmutableList<E> list, IImmutableFloatSorter<E> sorter, int low, int high) {
            return sorter.Sort(list, low, high);
        }
    }
}
