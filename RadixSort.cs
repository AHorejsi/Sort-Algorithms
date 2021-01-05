using System;
using System.Collections.Generic;

namespace Sorting {
    public enum RadixSortType { LSD, MSD, IN_PLACE_MSD }

    public sealed class RadixSorter<N> : IIntegerSorter<N>, IEquatable<RadixSorter<N>> {
        private RadixSortAlgorithm<N> algorithm;
        private RadixSortType algorithmType;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public RadixSorter(RadixSortType algorithm) {
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            this.Algorithm = algorithm;
        }

        public RadixSortType Algorithm {
            get => this.algorithmType;
            set {
                this.algorithmType = value;
                this.algorithm = value switch {
                    RadixSortType.LSD => new LSDRadixSortAlgorithm<N>(),
                    RadixSortType.MSD => new MSDRadixSortAlgorithm<N>(),
                    RadixSortType.IN_PLACE_MSD => new InPlaceMSDRadixSortAlgorithm<N>(),
                    _ => throw new InvalidOperationException()
                };
            }
        }

        public void Sort(IList<N> list, int low, int high) {
            this.algorithm.DoSort(list, low, high);
        }

        public override bool Equals(object? obj) => this.Equals(obj as RadixSorter<N>);

        public bool Equals(RadixSorter<N>? sorter) => sorter is null || this.algorithmType == sorter.algorithmType;

        public override int GetHashCode() => HashCode.Combine(this.algorithmType);
    }

    internal abstract class RadixSortAlgorithm<N> {
        public abstract void DoSort(IList<N> list, int low, int high);
    }

    internal sealed class LSDRadixSortAlgorithm<N> : RadixSortAlgorithm<N> {
        public override void DoSort(IList<N> list, int low, int high) {
            throw new NotImplementedException();
        }
    }

    internal sealed class MSDRadixSortAlgorithm<N> : RadixSortAlgorithm<N> {
        public override void DoSort(IList<N> list, int low, int high) {
            throw new NotImplementedException();
        }
    }

    internal sealed class InPlaceMSDRadixSortAlgorithm<N> : RadixSortAlgorithm<N> {
        public override void DoSort(IList<N> list, int low, int high) {
            throw new NotImplementedException();
        }
    }
}
