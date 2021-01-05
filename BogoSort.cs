using System;
using System.Collections.Generic;

namespace Sorting {
    public enum ShuffleType { RANDOM_SHUFFLE, SWAP_TWO }

    public sealed class BogoSorter<N> : ICompareSorter<N>, IEquatable<BogoSorter<N>> {
        private Shuffler<N> shuffler;
        private ShuffleType shuffleType;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public BogoSorter(ShuffleType shuffleType) {
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            this.ShuffleType = shuffleType;
        }

        public ShuffleType ShuffleType {
            get => this.shuffleType;
            set {
                this.shuffleType = value;
                this.shuffler = value switch {
                    ShuffleType.RANDOM_SHUFFLE => Shufflers.RandomShuffle,
                    ShuffleType.SWAP_TWO => Shufflers.SwapTwo,
                    _ => throw new InvalidOperationException()
                };
            }
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            var rand = new Random();

            while (!SortUtils.IsSorted(list, low, high, comparer)) {
                this.shuffler(list, low, high, rand);
            }
        }

        public override bool Equals(object? obj) => this.Equals(obj as BogoSorter<N>);

        public bool Equals(BogoSorter<N>? sorter) => sorter is null || this.shuffleType == sorter.shuffleType;

        public override int GetHashCode() => HashCode.Combine(this.shuffleType);
    }

    internal delegate void Shuffler<N>(IList<N> list, int low, int high, Random rand);

    internal static class Shufflers {
        public static void RandomShuffle<N>(IList<N> list, int low, int high, Random rand) {
            for (int index = low + 1; index < high - 1; ++index) {
                int randomIndex = rand.Next(low, index + 1);

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
}
