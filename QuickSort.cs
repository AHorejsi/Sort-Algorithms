using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public enum PivotSelectorType { FIRST, MIDDLE, LAST, RANDOM, MEDIAN_OF_THREE }
    public enum PartitionSchemeType { LOMUTO, HOARE, STABLE, THREE_WAY }
    public enum QuickSortType { RECURSIVE, ASYNC_RECURSIVE, ITERATIVE }

    internal delegate int PivotSelector<N>(IList<N> list, int low, int high, IComparer<N> comparer);

    internal static class PivotSelectors {
        private static readonly Random rand = new Random();

#pragma warning disable IDE0060 // Remove unused parameter
        internal static int First<N>(IList<N> list, int low, int high, IComparer<N> comparer) => low;

        internal static int Middle<N>(IList<N> list, int low, int high, IComparer<N> comparer) => low + (high + 1 - low) / 2;

        internal static int Last<N>(IList<N> list, int low, int high, IComparer<N> comparer) => high;

        internal static int Random<N>(IList<N> list, int low, int high, IComparer<N> comparer) => PivotSelectors.rand.Next(low, high + 1);
#pragma warning restore IDE0060 // Remove unused parameter

        internal static int MedianOfThree<N>(IList<N> list, int low, int high, IComparer<N> comparer) {
            int mid = low + (high + 1 - low) / 2;

            if (comparer.Compare(list[high], list[low]) < 0) {
                SortUtils.Swap(list, high, low);
            }
            if (comparer.Compare(list[mid], list[low]) < 0) {
                SortUtils.Swap(list, mid, low);
            }
            if (comparer.Compare(list[high], list[mid]) < 0) {
                SortUtils.Swap(list, high, mid);
            }

            return mid;
        }
    }

    internal abstract class PartitionScheme<N> {
        internal readonly PivotSelector<N> pivotSelector;

        internal PartitionScheme(PivotSelector<N> pivotSelector) {
            this.pivotSelector = pivotSelector;
        }

        internal abstract int[] Partition(IList<N> list, int low, int high, IComparer<N> comparer);
    }

    internal class LomutoPartitionScheme<N> : PartitionScheme<N> {
        internal LomutoPartitionScheme(PivotSelector<N> pivotSelector) : base(pivotSelector) {
        }

        internal override int[] Partition(IList<N> list, int low, int high, IComparer<N> comparer) {
            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, high);

            N pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; ++j) {
                if (comparer.Compare(list[j], pivot) <= 0) {
                    ++i;
                    SortUtils.Swap(list, i, j);
                }
            }

            int swapIndex = i + 1;
            SortUtils.Swap(list, swapIndex, high);

            return new int[] { swapIndex };
        }
    }

    internal class HoarePartitionScheme<N> : PartitionScheme<N> {
        internal HoarePartitionScheme(PivotSelector<N> pivotSelector) : base(pivotSelector) {
        }

        internal override int[] Partition(IList<N> list, int low, int high, IComparer<N> comparer) {
            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, low);

            N pivot = list[low];
            int i = low - 1;
            int j = high + 1;

            while (true) {
                do {
                    ++i;
                } while (comparer.Compare(list[i], pivot) < 0);

                do {
                    --j;
                } while (comparer.Compare(list[j], pivot) > 0);

                if (i >= j) {
                    return new int[] { j };
                }

                SortUtils.Swap(list, i, j);
            }
        }
    }

    internal class StablePartitionScheme<N> : PartitionScheme<N> {
        internal StablePartitionScheme(PivotSelector<N> pivotSelector) : base(pivotSelector) {
        }

        internal override int[] Partition(IList<N> list, int low, int high, IComparer<N> comparer) {
            int mid = low + (high + 1 - low) / 2;

            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, mid);

            var lesser = new List<N>(list.Count);
            var greater = new List<N>(list.Count);
            N pivot = list[mid];

            for (int index = low; index <= high; ++index) {
                if (index != mid) {
                    int comparison = comparer.Compare(list[index], pivot);

                    if (comparison < 0) {
                        lesser.Add(list[index]);
                    }
                    else if (comparison > 0) {
                        greater.Add(list[index]);
                    }
                    else {
                        if (index < mid) {
                            lesser.Add(list[index]);
                        }
                        else {
                            greater.Add(list[index]);
                        }
                    }
                }
            }

            int i = low;

            foreach (N val in lesser) {
                list[i] = val;
                ++i;
            }

            int partitionPoint = i;
            list[i] = pivot;
            ++i;

            foreach (N val in greater) {
                list[i] = val;
                ++i;
            }

            return new int[] { partitionPoint };
        }
    }

    internal class ThreeWayPartitionScheme<N> : PartitionScheme<N> {
        internal ThreeWayPartitionScheme(PivotSelector<N> pivotSelector) : base(pivotSelector) {
        }

        internal override int[] Partition(IList<N> list, int low, int high, IComparer<N> comparer) {
            int leftPartitionPoint;
            int rightPartitionPoint;

            if (high - low <= 1) {
                if (comparer.Compare(list[low], list[high]) > 0) {
                    SortUtils.Swap(list, low, high);
                }

                leftPartitionPoint = low;
                rightPartitionPoint = high;
            }
            else {
                int pivotIndex = base.pivotSelector(list, low, high, comparer);
                SortUtils.Swap(list, pivotIndex, high);

                int mid = low;
                N pivot = list[high];

                while (mid <= high) {
                    int comparison = comparer.Compare(list[mid], pivot);

                    if (comparison < 0) {
                        SortUtils.Swap(list, low, mid);

                        ++low;
                        ++mid;
                    }
                    else if (comparison > 0) {
                        SortUtils.Swap(list, mid, high);

                        --high;
                    }
                    else {
                        ++mid;
                    }
                }

                leftPartitionPoint = low - 1;
                rightPartitionPoint = mid;
            }

            return new int[] { leftPartitionPoint, rightPartitionPoint };
        }
    }

    internal delegate int PartitionPointDistance(int partitionPoint);

    internal static class PartitionPointDistances {
        internal static int At(int partitionPoint) => partitionPoint;

        internal static int RightOne(int partitionPoint) => partitionPoint + 1;

        internal static int LeftOne(int partitionPoint) => partitionPoint - 1;
    }

    public abstract class QuickSorter<N> {
        private protected readonly PartitionScheme<N> partitionScheme;
        private protected readonly PartitionPointDistance left;
        private protected readonly PartitionPointDistance right;

        internal QuickSorter(PartitionScheme<N> partitionScheme, PartitionPointDistance left, PartitionPointDistance right) {
            this.partitionScheme = partitionScheme;
            this.left = left;
            this.right = right;
        }

        public abstract void Sort(IList<N> list, int low, int high, IComparer<N> comparer);
    }

    internal class RecursiveQuickSorter<N> : QuickSorter<N> {
        internal RecursiveQuickSorter(PartitionScheme<N> partitionScheme, PartitionPointDistance left, PartitionPointDistance right)
            : base(partitionScheme, left, right) {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList<N> list, int low, int high, IComparer<N> comparer) {
            if (low < high) {
                int[] partitionPoint = base.partitionScheme.Partition(list, low, high, comparer);

                if (2 == partitionPoint.Length) {
                    this.DoSort(list, low, base.left(partitionPoint[0]), comparer);
                    this.DoSort(list, base.right(partitionPoint[1]), high, comparer);
                }
                else {
                    this.DoSort(list, low, base.left(partitionPoint[0]), comparer);
                    this.DoSort(list, base.right(partitionPoint[0]), high, comparer);
                }
            }
        }
    }

    internal class AsyncRecursiveQuickSorter<N> : QuickSorter<N> {
        internal AsyncRecursiveQuickSorter(PartitionScheme<N> partitionScheme, PartitionPointDistance left, PartitionPointDistance right)
            : base(partitionScheme, left, right) {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList<N> list, int low, int high, IComparer<N> comparer) {
            if (low < high) {
                int[] partitionPoint = base.partitionScheme.Partition(list, low, high, comparer);

                Task task1;
                Task task2;

                if (2 == partitionPoint.Length) {
                    task1 = Task.Run(() => { this.DoSort(list, low, base.left(partitionPoint[0]), comparer); });
                    task2 = Task.Run(() => { this.DoSort(list, base.right(partitionPoint[1]), high, comparer); });
                }
                else {
                    task1 = Task.Run(() => { this.DoSort(list, low, base.left(partitionPoint[0]), comparer); });
                    task2 = Task.Run(() => { this.DoSort(list, base.right(partitionPoint[0]), high, comparer); });
                }

                Task.WaitAll(task1, task2);

                task1.Dispose();
                task2.Dispose();
            }
        }
    }

    internal class IterativeQuickSorter<N> : QuickSorter<N> {
        internal IterativeQuickSorter(PartitionScheme<N> partitionScheme, PartitionPointDistance left, PartitionPointDistance right)
            : base(partitionScheme, left, right) {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            if (high - low > 1) {
                --high;

                var recursionStack = new Stack<int>(high - low);
                recursionStack.Push(low);
                recursionStack.Push(high);

                while (recursionStack.Count != 0) {
                    high = recursionStack.Pop();
                    low = recursionStack.Pop();

                    int[] partitionPoint = base.partitionScheme.Partition(list, low, high, comparer);
                    int leftPartitionPoint;
                    int rightPartitionPoint;

                    if (2 == partitionPoint.Length) {
                        leftPartitionPoint = base.left(partitionPoint[0]);
                        rightPartitionPoint = base.right(partitionPoint[1]);
                    }
                    else {
                        leftPartitionPoint = base.left(partitionPoint[0]);
                        rightPartitionPoint = base.right(partitionPoint[0]);
                    }

                    if (leftPartitionPoint > low) {
                        recursionStack.Push(low);
                        recursionStack.Push(leftPartitionPoint);
                    }

                    if (rightPartitionPoint < high) {
                        recursionStack.Push(rightPartitionPoint);
                        recursionStack.Push(high);
                    }
                }
            }
        }
    }

    public sealed class QuickSortBuilder<N> {
        private PivotSelectorType pivotSelectorType;
        private PartitionSchemeType partitionSchemeType;
        private QuickSortType algorithmType;

        public QuickSortBuilder<N> WithPivotSelector(PivotSelectorType pivotSelectorType) {
            this.pivotSelectorType = pivotSelectorType;

            return this;
        }

        public QuickSortBuilder<N> WithPartitionScheme(PartitionSchemeType partitionSchemeType) {
            this.partitionSchemeType = partitionSchemeType;

            return this;
        }

        public QuickSortBuilder<N> WithAlgorithm(QuickSortType algorithmType) {
            this.algorithmType = algorithmType;

            return this;
        }

        public QuickSorter<N> Build() {
            PartitionScheme<N> partitionScheme = this.MakePartitionScheme();
            (PartitionPointDistance left, PartitionPointDistance right) = this.MakePartitionPointDistances();

            return this.algorithmType switch {
                QuickSortType.RECURSIVE => new RecursiveQuickSorter<N>(partitionScheme, left, right),
                QuickSortType.ASYNC_RECURSIVE => new AsyncRecursiveQuickSorter<N>(partitionScheme, left, right),
                QuickSortType.ITERATIVE => new IterativeQuickSorter<N>(partitionScheme, left, right),
                _ => throw new InvalidOperationException()
            };
        }

        private PartitionScheme<N> MakePartitionScheme() {
            PivotSelector<N> pivotSelector = this.MakePivotSelector();

            return this.partitionSchemeType switch {
                PartitionSchemeType.LOMUTO => new LomutoPartitionScheme<N>(pivotSelector),
                PartitionSchemeType.HOARE => new HoarePartitionScheme<N>(pivotSelector),
                PartitionSchemeType.STABLE => new StablePartitionScheme<N>(pivotSelector),
                PartitionSchemeType.THREE_WAY => new ThreeWayPartitionScheme<N>(pivotSelector),
                _ => throw new InvalidOperationException()
            };
        }

        private PivotSelector<N> MakePivotSelector() {
            return this.pivotSelectorType switch {
                PivotSelectorType.FIRST => PivotSelectors.First,
                PivotSelectorType.MIDDLE => PivotSelectors.Middle,
                PivotSelectorType.LAST => PivotSelectors.Last,
                PivotSelectorType.RANDOM => PivotSelectors.Random,
                PivotSelectorType.MEDIAN_OF_THREE => PivotSelectors.MedianOfThree,
                _ => throw new InvalidOperationException(),
            };
        }

        private (PartitionPointDistance, PartitionPointDistance) MakePartitionPointDistances() {
            return this.partitionSchemeType switch {
                PartitionSchemeType.LOMUTO => (PartitionPointDistances.LeftOne, PartitionPointDistances.RightOne),
                PartitionSchemeType.HOARE => (PartitionPointDistances.At, PartitionPointDistances.RightOne),
                PartitionSchemeType.STABLE => (PartitionPointDistances.LeftOne, PartitionPointDistances.RightOne),
                PartitionSchemeType.THREE_WAY => (PartitionPointDistances.At, PartitionPointDistances.At),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
