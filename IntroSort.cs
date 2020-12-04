using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class IntroSorter<N> : ICompareSorter<N> {
        private readonly byte simpleSortLimit = 16;
        private readonly InsertionSorter<N> simpleSorter = InsertionSortFactory<N>.Make(SearchType.LINEAR);
        private readonly HeapSorter<N> depthLimitSorter = HeapSortFactory<N>.Make(HeapType.BINARY);
        private readonly HoarePartitionScheme<N> partitionScheme = new HoarePartitionScheme<N>(PivotSelectors.MedianOfThree);

        public IntroSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);
            this.DoSort(list, low, high - 1, comparer, (int)(2 * Math.Log2(high - low)));
        }

        private void DoSort(IList<N> list, int low, int high, IComparer<N> comparer, int depthLimit) {
            if (high - low + 1 <= this.simpleSortLimit) {
                this.simpleSorter.Sort(list, low, high + 1, comparer);
            }
            else if (0 == depthLimit) {
                this.depthLimitSorter.Sort(list, low, high + 1, comparer);
            }
            else {
                --depthLimit;

                int partitionPoint = this.partitionScheme.Partition(list, low, high, comparer)[0];

                this.DoSort(list, low, partitionPoint, comparer, depthLimit);
                this.DoSort(list, partitionPoint + 1, high, comparer, depthLimit);
            }
        }
    }
}
