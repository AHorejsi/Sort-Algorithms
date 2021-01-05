using System;
using System.Collections.Generic;

namespace Sorting {
    public static class ExtensionMethods {
        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter) where E : IComparable<E>? {
            list.Sort(sorter, Comparer<E>.Default);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, Comparison<E> comparison) {
            list.Sort(sorter, Comparer<E>.Create(comparison));
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, IComparer<E>? comparer) {
            list.Sort(sorter, comparer, 0, list.Count);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, int low, int high) where E : IComparable<E>? {
            list.Sort(sorter, Comparer<E>.Default, low, high);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, Comparison<E> comparison, int low, int high) {
            list.Sort(sorter, Comparer<E>.Create(comparison), low, high);
        }

        public static void Sort<E>(this IList<E> list, ICompareSorter<E> sorter, IComparer<E>? comparer, int low, int high) {
            ExtensionMethods.CheckRange(low, high);

            if (comparer is null) {
                comparer = Comparer<E>.Default;
            }

            sorter.Sort(list, low, high, comparer);
        }

        public static void Sort<E>(this IList<E?> list, ICompareSorter<E?> sorter) where E : struct, IComparable<E>? {
            list.Sort(sorter, Comparer<E?>.Default);
        }

        public static void Sort<E>(this IList<E?> list, ICompareSorter<E?> sorter, Comparison<E?> comparison) where E : struct {
            list.Sort(sorter, Comparer<E?>.Create(comparison));
        }

        public static void Sort<E>(this IList<E?> list, ICompareSorter<E?> sorter, IComparer<E?>? comparer) where E : struct {
            list.Sort(sorter, comparer, 0, list.Count);
        }

        public static void Sort<E>(this IList<E?> list, ICompareSorter<E?> sorter, int low, int high) where E : struct, IComparable<E>? {
            list.Sort(sorter, Comparer<E?>.Default, low, high);
        }

        public static void Sort<E>(this IList<E?> list, ICompareSorter<E?> sorter, Comparison<E?> comparison, int low, int high) where E : struct {
            list.Sort(sorter, Comparer<E?>.Create(comparison), low, high);
        }

        public static void Sort<E>(this IList<E?> list, ICompareSorter<E?> sorter, IComparer<E?>? comparer, int low, int high) where E : struct {
            ExtensionMethods.CheckRange(low, high);

            if (comparer is null) {
                comparer = Comparer<E?>.Default;
            }

            sorter.Sort(list, low, high, comparer);
        }

        public static void Sort(this IList<byte> list, IIntegerSorter<byte> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<byte> list, IIntegerSorter<byte> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<sbyte> list, IIntegerSorter<sbyte> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<sbyte> list, IIntegerSorter<sbyte> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<short> list, IIntegerSorter<short> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<short> list, IIntegerSorter<short> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this IList<ushort> list, IIntegerSorter<ushort> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<ushort> list, IIntegerSorter<ushort> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<int> list, IIntegerSorter<int> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<int> list, IIntegerSorter<int> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this IList<uint> list, IIntegerSorter<uint> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<uint> list, IIntegerSorter<uint> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<long> list, IIntegerSorter<long> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<long> list, IIntegerSorter<long> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<ulong> list, IIntegerSorter<ulong> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<ulong> list, IIntegerSorter<ulong> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<float> list, IFloatSorter<float> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<float> list, IFloatSorter<float> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<double> list, IFloatSorter<double> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<double> list, IFloatSorter<double> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<decimal> list, IFloatSorter<decimal> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<decimal> list, IFloatSorter<decimal> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<byte?> list, IIntegerSorter<byte?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<byte?> list, IIntegerSorter<byte?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<sbyte?> list, IIntegerSorter<sbyte?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<sbyte?> list, IIntegerSorter<sbyte?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<short?> list, IIntegerSorter<short?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<short?> list, IIntegerSorter<short?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this IList<ushort?> list, IIntegerSorter<ushort?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<ushort?> list, IIntegerSorter<ushort?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<int?> list, IIntegerSorter<int?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<int?> list, IIntegerSorter<int?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort<E>(this IList<uint?> list, IIntegerSorter<uint?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<uint?> list, IIntegerSorter<uint?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<long?> list, IIntegerSorter<long?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<long?> list, IIntegerSorter<long?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<ulong?> list, IIntegerSorter<ulong?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<ulong?> list, IIntegerSorter<ulong?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<float?> list, IFloatSorter<float?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<float?> list, IFloatSorter<float?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<double?> list, IFloatSorter<double?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<double?> list, IFloatSorter<double?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList<decimal?> list, IFloatSorter<decimal?> sorter) {
            list.Sort(sorter, 0, list.Count);
        }

        public static void Sort(this IList<decimal?> list, IFloatSorter<decimal?> sorter, int low, int high) {
            ExtensionMethods.CheckRange(low, high);
            sorter.Sort(list, low, high);
        }

        public static void CheckRange(int low, int high) {
            if (low >= high) {
                throw new InvalidOperationException("Starting index must be lower than ending index");
            }
        }
    }
}
