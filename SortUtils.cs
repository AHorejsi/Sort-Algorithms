using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    internal static class SortUtils {
        private static readonly Random Rand = new Random();
        public static readonly ParallelOptions ParallelOptions = new ParallelOptions();

        static SortUtils() {
            SortUtils.ParallelOptions.TaskScheduler = null;
            SortUtils.ParallelOptions.MaxDegreeOfParallelism = -1;
        }

        public static int RandomInt(int low, int high) {
            return SortUtils.Rand.Next(low, high);
        }

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

        public static void Reverse(IList list, int low, int high) {
            --high;

            while (low < high) {
                SortUtils.Swap(list, low, high);

                ++low;
                --high;
            }
        }
    }
}
