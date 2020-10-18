using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sorting {
    internal static class SortUtils {
        private static readonly Random rand;
        private static readonly ImmutableArray<Type> integerTypeSet;
        private static readonly ImmutableArray<Type> floatTypeSet;

        static SortUtils() {
            SortUtils.rand = new Random();

            SortUtils.integerTypeSet = ImmutableArray.Create(
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong)
            );

            SortUtils.floatTypeSet = ImmutableArray.Create(
                typeof(float),
                typeof(double),
                typeof(decimal)
            );
        }

        public static int RandomInt(int low, int high) {
            return SortUtils.rand.Next(low, high);
        }

        public static void Swap(IList list, int i, int j) {
            object? temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void Swap<T>(IList<T> list, int i, int j) {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void Swap<T>(LinkedListNode<T> i, LinkedListNode<T> j) {
            T temp = i.Value;
            i.Value = j.Value;
            j.Value = temp;
        }

        public static bool IsSorted(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high - 1; ++index) {
                if (comparer.Compare(list[index], list[index + 1]) > 0) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsIntegerType<T>() {
            return SortUtils.integerTypeSet.Contains(typeof(T));
        }

        public static bool IsFloatType<T>() {
            return SortUtils.floatTypeSet.Contains(typeof(T));
        }
    }
}
