using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sorting.Immutable {
    public static class ExtensionMethods {
        public static IImmutableList<E> Sort<E, F>(this IImmutableList<E> list, IImmutableCompareSorter<F> sorter) where E : F, IComparable<F> {
            return sorter.Sort(list);
        }

        public static IImmutableList<E> Sort<E, F>(this IImmutableList<E> list, IImmutableCompareSorter<F> sorter, Comparison<F> comparison) where E : F {
            return sorter.Sort(list, comparison);
        }

        public static IImmutableList<E> Sort<E, F>(this IImmutableList<E> list, IImmutableCompareSorter<F> sorter, IComparer<F> comparer) where E : F {
            return sorter.Sort(list, comparer);
        }

        public static IImmutableList<E> Sort<E, F>(this IImmutableList<E> list, IImmutableCompareSorter<F> sorter, int low, int high) where E : F, IComparable<F> {
            return sorter.Sort(list, low, high);
        }

        public static IImmutableList<E> Sort<E, F>(this IImmutableList<E> list, IImmutableCompareSorter<F> sorter, Comparison<F> comparison, int low, int high) where E : F {
            return sorter.Sort(list, low, high, comparison);
        }

        public static IImmutableList<E> Sort<E, F>(this IImmutableList<E> list, IImmutableCompareSorter<F> sorter, IComparer<F> comparer, int low, int high) where E : F {
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
