using System.Collections.Generic;

namespace Sorting {
    internal static class SortUtils {
        public static void Swap<T>(IList<T> list, int i, int j) {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static bool IsSorted<T>(IList<T> list, int low, int high, IComparer<T> comparer) {
            for (int index = low; index < high - 1; ++index) {
                if (comparer.Compare(list[index], list[index + 1]) > 0) {
                    return false;
                }
            }

            return true;
        }
    }
}
