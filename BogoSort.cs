using System;
using System.Collections;

namespace Sorting {
    internal delegate void Shuffler(IList list, int low, int high);

    public class BogoSorter : ICompareSorter, IEquatable<BogoSorter> {
        private readonly Shuffler shuffler;

        internal BogoSorter(Shuffler shuffler) {
            this.shuffler = shuffler;
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            while (!SortUtils.IsSorted(list, low, high, comparer)) {
                this.shuffler(list, low, high);
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as BogoSorter);
        }

        public bool Equals(BogoSorter? sorter) {
            if (sorter is null) {
                return false;
            }
            else {
                return this.shuffler == sorter.shuffler;
            }
        }

        public override int GetHashCode() {
            int shufflerHashCode;

            if (Shufflers.RandomShuffle == this.shuffler) {
                shufflerHashCode = 999557639;
            }
            else { // Shufflers.SwapTwo == this.shuffler
                shufflerHashCode = 619567930;
            }

            return shufflerHashCode;
        }
    }

    internal static class Shufflers {
        public static void RandomShuffle(IList list, int low, int high) {
            for (int index = low + 2; index < high; ++index) {
                int randomIndex = SortUtils.RandomInt(low, index);

                if (randomIndex != index) {
                    SortUtils.Swap(list, index, randomIndex);
                }
            }
        }

        public static void SwapTwo(IList list, int low, int high) {
            int randomIndex1 = SortUtils.RandomInt(low, high);
            int randomIndex2 = SortUtils.RandomInt(low, high);

            if (randomIndex1 != randomIndex2) {
                SortUtils.Swap(list, randomIndex1, randomIndex2);
            }
        }
    }

    public enum ShufflerType { RANDOM_SHUFFLE, SWAP_TWO }

    public static class BogoSortFactory {
        public static BogoSorter Make(ShufflerType type) {
            Shuffler shuffler = type switch {
                ShufflerType.RANDOM_SHUFFLE => Shufflers.RandomShuffle,
                ShufflerType.SWAP_TWO => Shufflers.SwapTwo,
                _ => throw new InvalidOperationException()
            };

            return new BogoSorter(shuffler);
        }
    }
}
