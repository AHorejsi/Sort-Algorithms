using System;
using System.Collections.Generic;

namespace Sorting {
    internal delegate void Shuffler<N>(IList<N> list, int low, int high, Random rand);

    public sealed class BogoSorter<N> : ICompareSorter<N> {
        private readonly Shuffler<N> shuffler;

        internal BogoSorter(Shuffler<N> shuffler) {
            this.shuffler = shuffler;
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            Random rand = new Random();

            while (!SortUtils.IsSorted(list, low, high, comparer)) {
                this.shuffler(list, low, high, rand);
            }
        }
    }

    internal static class Shufflers {
        public static void RandomShuffle<N>(IList<N> list, int low, int high, Random rand) {
            for (int index = low + 2; index < high; ++index) {
                int randomIndex = rand.Next(low, index);

                if (randomIndex != index) {
                    SortUtils.Swap(list, index, randomIndex);
                }
            }
        }

        public static void SwapTwo<N>(IList<N> list, int low, int high, Random rand) {
            int randomIndex1 = rand.Next(low, high);
            int randomIndex2 = rand.Next(low, high);

            if (randomIndex1 != randomIndex2) {
                SortUtils.Swap(list, randomIndex1, randomIndex2);
            }
        }
    }

    public enum ShufflerType { RANDOM_SHUFFLE, SWAP_TWO }

    public static class BogoSortFactory {
        public static BogoSorter<N> Make<N>(ShufflerType type) {
            Shuffler<N> shuffler = type switch
            {
                ShufflerType.RANDOM_SHUFFLE => Shufflers.RandomShuffle,
                ShufflerType.SWAP_TWO => Shufflers.SwapTwo,
                _ => throw new InvalidOperationException()
            };

            return new BogoSorter<N>(shuffler);
        }
    }
}
