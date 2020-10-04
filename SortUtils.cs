using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    internal static class SortUtils {
        private static readonly Random Rand = new Random();

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
    }
}
