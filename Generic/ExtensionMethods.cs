using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public static class ExtensionMethods {
        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter) where E : IComparable<E> {
            sorter.Sort(list);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, Comparison<E> comparison) {
            sorter.Sort(list, comparison);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, IComparer<E> comparer) {
            sorter.Sort(list, comparer);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, int low, int high) where E : IComparable<E> {
            sorter.Sort(list, low, high);
        }
    }
}
