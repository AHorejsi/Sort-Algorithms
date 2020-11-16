using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public sealed class IntroSorter : ICompareSorter, IEquatable<IntroSorter> {
        private readonly byte simpleSortLimit = 16;
        private readonly ICompareSorter simpleSorter = InsertionSortFactory.Make(SearchType.LINEAR);
        private readonly ICompareSorter depthLimitSorter = HeapSortFactory.Make(HeapType.BINARY);
        private readonly PartitionScheme partitionScheme = new HoarePartitionScheme(PivotSelectors.MedianOfThree);
        private static IntroSorter? instance = null;

        private IntroSorter() {
        }

        public static IntroSorter Singleton {
            get {
                if (IntroSorter.instance is null) {
                    IntroSorter.instance = new IntroSorter();
                }

                return IntroSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            SortUtils.CheckRange(low, high);

            this.DoSort(list, low, high - 1, comparer, (int)(2 * Math.Log2(high - low)));
        }

        private void DoSort(IList list, int low, int high, IComparer comparer, int depthLimit) {
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

        public override bool Equals(object? obj) {
            return this.Equals(obj as IntroSorter);
        }

        public bool Equals(IntroSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
