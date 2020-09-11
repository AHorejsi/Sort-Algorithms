using System.Collections;

namespace Sorting {
    internal static class SortingUtils {
        public static void Swap(IList list, int i, int j) {
            object temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static bool IsSorted(IList list) {
            return SortingUtils.IsSorted(list, Comparer.Default);
        }

        public static bool IsSorted(IList list, IComparer comparer) {
            return SortingUtils.IsSorted(list, 0, list.Count, comparer);
        }

        public static bool IsSorted(IList list, int low, int high) {
            return SortingUtils.IsSorted(list, low, high, Comparer.Default);
        }

        public static bool IsSorted(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high - 1; ++index) {
                if (comparer.Compare(list[index], list[index + 1]) > 0) {
                    return false;
                }
            }

            return true;
        }
    }
}
