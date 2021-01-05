using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class IntroSorter<N> : ICompareSorter<N>, IEquatable<IntroSorter<N>> {
        private static readonly byte simpleSortLimit = 16;
        private static readonly InsertionSorter<N> simpleSorter = new InsertionSorter<N>(SearchType.LINEAR);
        private static readonly HeapSorter<N> depthLimitSorter = new HeapSorter<N>(HeapType.BINARY);
        private static readonly HoarePartitionScheme<N> partitionScheme = new HoarePartitionScheme<N>(PivotSelectors.MedianOfThree);

        public IntroSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            this.DoSort(list, low, high - 1, comparer, (int)(2 * Math.Log2(high - low)));
        }

        private void DoSort(IList<N> list, int low, int high, IComparer<N> comparer, int depthLimit) {
            if (high - low + 1 <= IntroSorter<N>.simpleSortLimit) {
                IntroSorter<N>.simpleSorter.Sort(list, low, high + 1, comparer);
            }
            else if (0 == depthLimit) {
                IntroSorter<N>.depthLimitSorter.Sort(list, low, high + 1, comparer);
            }
            else {
                --depthLimit;

                int partitionPoint = IntroSorter<N>.partitionScheme.Partition(list, low, high, comparer)[0];

                this.DoSort(list, low, partitionPoint, comparer, depthLimit);
                this.DoSort(list, partitionPoint + 1, high, comparer, depthLimit);
            }
        }

        public override bool Equals(object? obj) => this.Equals(obj as IntroSorter<N>);

        public bool Equals(IntroSorter<N>? sorter) => !(sorter is null);

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
