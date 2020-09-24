using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public class QuickSorter : ComparisonSorter {
        private readonly QuickSortAlgorithm algorithm;

        internal QuickSorter(QuickSortAlgorithm algorithm) {
            this.algorithm = algorithm;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            this.algorithm.Sort(list, low, high, comparer);
        }
    }

    public enum PivotSelectorType { FIRST, MIDDLE, LAST, RANDOM, MEDIAN_OF_THREE }
    public enum PartitionSchemeType { LOMUTO, HOARE, STABLE, THREE_WAY }
    public enum QuickSortAlgorithmType { RECURSIVE, ASYNC_RECURSIVE, ITERATIVE }

    public class QuickSorterBuilder {
        private PivotSelectorType pivotSelectorType = PivotSelectorType.MEDIAN_OF_THREE;
        private PartitionSchemeType partitionSchemeType = PartitionSchemeType.HOARE;
        private QuickSortAlgorithmType algorithmType = QuickSortAlgorithmType.RECURSIVE;

        public QuickSorterBuilder WithPivotSelectorType(PivotSelectorType pivotSelectorType) {
            this.pivotSelectorType = pivotSelectorType;

            return this;
        }

        public QuickSorterBuilder WithPartitionSchemeType(PartitionSchemeType partitionSchemeType) {
            this.partitionSchemeType = partitionSchemeType;

            return this;
        }

        public QuickSorterBuilder WithAlgorithmType(QuickSortAlgorithmType algorithmType) {
            this.algorithmType = algorithmType;

            return this;
        }

        public QuickSorter Build() {
            QuickSortAlgorithm algorithm = this.MakeAlgorithm();

            return new QuickSorter(algorithm);
        }

        private QuickSortAlgorithm MakeAlgorithm() {
            PartitionScheme partitionScheme = this.MakePartitionScheme();

            return this.algorithmType switch {
                QuickSortAlgorithmType.RECURSIVE => new RecursiveQuickSortAlgorithm(partitionScheme),
                QuickSortAlgorithmType.ASYNC_RECURSIVE => new AsyncRecursiveQuickSortAlgorithm(partitionScheme),
                QuickSortAlgorithmType.ITERATIVE => new IterativeQuickSortAlgorithm(partitionScheme),
                _ => throw new NotImplementedException()
            };
        }

        private PartitionScheme MakePartitionScheme() {
            PivotSelector pivotSelector = this.MakePivotSelector();
            Tuple<PartitionPointDistance, PartitionPointDistance> partitionPointDistances = this.MakePartitionPointDistances();

            return this.partitionSchemeType switch {
                PartitionSchemeType.LOMUTO => new LomutoPartitionScheme(pivotSelector, partitionPointDistances.Item1, partitionPointDistances.Item2),
                PartitionSchemeType.HOARE => new HoarePartitionScheme(pivotSelector, partitionPointDistances.Item1, partitionPointDistances.Item2),
                PartitionSchemeType.STABLE => new StablePartitionScheme(pivotSelector, partitionPointDistances.Item1, partitionPointDistances.Item2),
                PartitionSchemeType.THREE_WAY => new ThreeWayPartitionScheme(pivotSelector, partitionPointDistances.Item1, partitionPointDistances.Item2),
                _ => throw new NotImplementedException()
            };
        }

        private PivotSelector MakePivotSelector() {
            return this.pivotSelectorType switch {
                PivotSelectorType.FIRST => PivotSelectors.First,
                PivotSelectorType.MIDDLE => PivotSelectors.Middle,
                PivotSelectorType.LAST => PivotSelectors.Last,
                PivotSelectorType.RANDOM => PivotSelectors.Random,
                PivotSelectorType.MEDIAN_OF_THREE => PivotSelectors.MedianOfThree,
                _ => throw new NotImplementedException(),
            };
        }

        private Tuple<PartitionPointDistance, PartitionPointDistance> MakePartitionPointDistances() {
            return this.partitionSchemeType switch {
                PartitionSchemeType.LOMUTO => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.LeftOne, PartitionPointDistances.RightOne),
                PartitionSchemeType.HOARE => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.At, PartitionPointDistances.RightOne),
                PartitionSchemeType.STABLE => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.LeftOne, PartitionPointDistances.RightOne),
                PartitionSchemeType.THREE_WAY => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.At, PartitionPointDistances.At),
                _ => throw new NotImplementedException()
            };
        }
    }

    internal abstract class PartitionScheme {
        internal readonly PivotSelector pivotSelector;
        internal readonly PartitionPointDistance left;
        internal readonly PartitionPointDistance right;

        internal PartitionScheme(PivotSelector pivotSelector, PartitionPointDistance left, PartitionPointDistance right) {
            this.pivotSelector = pivotSelector;
            this.left = left;
            this.right = right;
        }

        internal abstract int[] Partition(IList list, int low, int high, IComparer comparer);
    }

    internal class LomutoPartitionScheme : PartitionScheme {
        internal LomutoPartitionScheme(PivotSelector pivotSelector, PartitionPointDistance left, PartitionPointDistance right) : base(pivotSelector, left, right) {
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, high);

            object pivot = list[high];
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

    internal class HoarePartitionScheme : PartitionScheme {
        internal HoarePartitionScheme(PivotSelector pivotSelector, PartitionPointDistance left, PartitionPointDistance right) : base(pivotSelector, left, right) { 
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, low);

            object pivot = list[low];
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

    internal class StablePartitionScheme : PartitionScheme {
        internal StablePartitionScheme(PivotSelector pivotSelector, PartitionPointDistance left, PartitionPointDistance right) : base(pivotSelector, left, right) {
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
            int mid = low + (high + 1 - low) / 2;

            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, mid);

            ArrayList lesser = new ArrayList(list.Count);
            ArrayList greater = new ArrayList(list.Count);
            object pivot = list[mid];

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

            foreach (object val in lesser) {
                list[i] = val;
                ++i;
            }

            int partitionPoint = i;
            list[i] = pivot;
            ++i;

            foreach (object val in greater) {
                list[i] = val;
                ++i;
            }

            return new int[] { partitionPoint };
        }
    }

    internal class ThreeWayPartitionScheme : PartitionScheme {
        internal ThreeWayPartitionScheme(PivotSelector pivotSelector, PartitionPointDistance left, PartitionPointDistance right) : base(pivotSelector, left, right) {
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
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
                object pivot = list[high];

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

    internal abstract class QuickSortAlgorithm {
        protected readonly PartitionScheme partitionScheme;

        internal QuickSortAlgorithm(PartitionScheme partitionScheme) {
            this.partitionScheme = partitionScheme;
        }

        internal abstract void Sort(IList list, int low, int high, IComparer comparer);
    }

    internal class RecursiveQuickSortAlgorithm : QuickSortAlgorithm {
        internal RecursiveQuickSortAlgorithm(PartitionScheme partitionScheme) : base(partitionScheme) {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList list, int low, int high, IComparer comparer) {
            if (low < high) {
                int[] partitionPoint = base.partitionScheme.Partition(list, low, high, comparer);

                if (2 == partitionPoint.Length) {
                    this.DoSort(list, low, base.partitionScheme.left(partitionPoint[0]), comparer);
                    this.DoSort(list, base.partitionScheme.right(partitionPoint[1]), high, comparer);
                }
                else {
                    this.DoSort(list, low, base.partitionScheme.left(partitionPoint[0]), comparer);
                    this.DoSort(list, base.partitionScheme.right(partitionPoint[0]), high, comparer);
                }
            }
        }
    }

    internal class AsyncRecursiveQuickSortAlgorithm : QuickSortAlgorithm {
        internal AsyncRecursiveQuickSortAlgorithm(PartitionScheme partitionScheme) : base(partitionScheme) {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList list, int low, int high, IComparer comparer) {
            if (low < high) {
                int[] partitionPoint = base.partitionScheme.Partition(list, low, high, comparer);

                if (2 == partitionPoint.Length) {
                    Parallel.Invoke(
                        SortUtils.ParallelOptions,
                        () => { this.DoSort(list, low, base.partitionScheme.left(partitionPoint[0]), comparer); },
                        () => { this.DoSort(list, base.partitionScheme.right(partitionPoint[1]), high, comparer); }
                    );
                }
                else {
                    Parallel.Invoke(
                        SortUtils.ParallelOptions,
                        () => { this.DoSort(list, low, base.partitionScheme.left(partitionPoint[0]), comparer); },
                        () => { this.DoSort(list, base.partitionScheme.right(partitionPoint[0]), high, comparer); }
                    );
                }
            }
        }
    }

    internal class IterativeQuickSortAlgorithm : QuickSortAlgorithm {
        internal IterativeQuickSortAlgorithm(PartitionScheme partitionScheme) : base(partitionScheme) {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            --high;

            Stack<int> recursionStack = new Stack<int>(high - low);
            recursionStack.Push(low);
            recursionStack.Push(high);

            while (recursionStack.Count != 0) {
                high = recursionStack.Pop();
                low = recursionStack.Pop();

                int[] partitionPoint = base.partitionScheme.Partition(list, low, high, comparer);
                int leftPartitionPoint;
                int rightPartitionPoint;

                if (2 == partitionPoint.Length) {
                    leftPartitionPoint = base.partitionScheme.left(partitionPoint[0]);
                    rightPartitionPoint = base.partitionScheme.right(partitionPoint[1]);
                }
                else {
                    leftPartitionPoint = base.partitionScheme.left(partitionPoint[0]);
                    rightPartitionPoint = base.partitionScheme.right(partitionPoint[0]);
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

    internal delegate int PivotSelector(IList list, int low, int high, IComparer comparer);

    internal static class PivotSelectors {
        internal static int First(IList list, int low, int high, IComparer comparer) {
            return low;
        }

        internal static int Middle(IList list, int low, int high, IComparer comparer) {
            return low + (high + 1 - low) / 2;
        }

        internal static int Last(IList list, int low, int high, IComparer comparer) {
            return high;
        }

        internal static int Random(IList list, int low, int high, IComparer comparer) {
            return SortUtils.RandomInt(low, high + 1);
        }

        internal static int MedianOfThree(IList list, int low, int high, IComparer comparer) {
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

    internal delegate int PartitionPointDistance(int partitionPoint);

    internal static class PartitionPointDistances {
        internal static int At(int partitionPoint) {
            return partitionPoint;
        }

        internal static int RightOne(int partitionPoint) {
            return partitionPoint + 1;
        }

        internal static int LeftOne(int partitionPoint) {
            return partitionPoint - 1;
        }
    }
}
