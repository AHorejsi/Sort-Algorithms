using System.Collections;

namespace Sorting {
    internal static class SortingUtils {
        public static void Swap(IList list, int i, int j) {
            object temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static bool IsSorted(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high - 1; ++index) {
                if (comparer.Compare(list[index], list[index + 1]) > 0) {
                    return false;
                }
            }

            return true;
        }

        public static int MinIndex(IList list, int low, int high, IComparer comparer) {
            object minElement = list[low];
            int minIndex = low;

            for (int index = low + 1; index < high; ++index) {
                object current = list[index];

                if (comparer.Compare(current, minElement) < 0) {
                    minElement = current;
                    minIndex = index;
                }
            }

            return minIndex;
        }

        public static int MaxIndex(IList list, int low, int high, IComparer comparer) {
            object maxElement = list[low];
            int maxIndex = low;

            for (int index = low + 1; index < high; ++index) {
                object current = list[index];

                if (comparer.Compare(current, maxElement) > 0) {
                    maxElement = current;
                    maxIndex = index;
                }
            }

            return maxIndex;
        }

        public static void Reverse(IList list, int low, int high) {
            --high;

            while (low < high) {
                SortingUtils.Swap(list, low, high);

                ++low;
                --high;
            }
        }
    }
}
