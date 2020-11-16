using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public class QuickSorter : ICompareSorter, IEquatable<QuickSorter> {
        private readonly QuickSortAlgorithm algorithm;

        internal QuickSorter(QuickSortAlgorithm algorithm) {
            this.algorithm = algorithm;
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            SortUtils.CheckRange(low, high);

            this.algorithm.Sort(list, low, high, comparer);
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as QuickSorter);
        }

        public bool Equals(QuickSorter? sorter) {
            if (sorter is null) {
                return false;
            }
            else {
                return this.algorithm.Equals(sorter.algorithm);
            }
        }

        public override int GetHashCode() {
            return this.algorithm.GetHashCode();
        }
    }

    public enum PivotSelectorType { FIRST, MIDDLE, LAST, RANDOM, MEDIAN_OF_THREE }
    public enum PartitionSchemeType { LOMUTO, HOARE, STABLE, THREE_WAY }
    public enum QuickSortType { RECURSIVE, ASYNC_RECURSIVE, ITERATIVE }

    public class QuickSortBuilder {
        private PivotSelectorType pivotSelectorType;
        private PartitionSchemeType partitionSchemeType;
        private QuickSortType algorithmType;

        public QuickSortBuilder WithPivotSelector(PivotSelectorType pivotSelectorType) {
            this.pivotSelectorType = pivotSelectorType;

            return this;
        }

        public QuickSortBuilder WithPartitionScheme(PartitionSchemeType partitionSchemeType) {
            this.partitionSchemeType = partitionSchemeType;

            return this;
        }

        public QuickSortBuilder WithAlgorithm(QuickSortType algorithmType) {
            this.algorithmType = algorithmType;

            return this;
        }

        public QuickSorter Build() {
            QuickSortAlgorithm algorithm = this.MakeAlgorithm();

            return new QuickSorter(algorithm);
        }

        private QuickSortAlgorithm MakeAlgorithm() {
            PartitionScheme partitionScheme = this.MakePartitionScheme();
            Tuple<PartitionPointDistance, PartitionPointDistance> partitionPointDistances = this.MakePartitionPointDistances();

            return this.algorithmType switch {
                QuickSortType.RECURSIVE => new RecursiveQuickSortAlgorithm(partitionScheme, partitionPointDistances.Item1, partitionPointDistances.Item2),
                QuickSortType.ASYNC_RECURSIVE => new AsyncRecursiveQuickSortAlgorithm(partitionScheme, partitionPointDistances.Item1, partitionPointDistances.Item2),
                QuickSortType.ITERATIVE => new IterativeQuickSortAlgorithm(partitionScheme, partitionPointDistances.Item1, partitionPointDistances.Item2),
                _ => throw new InvalidOperationException()
            };
        }

        private PartitionScheme MakePartitionScheme() {
            PivotSelector pivotSelector = this.MakePivotSelector();

            return this.partitionSchemeType switch {
                PartitionSchemeType.LOMUTO => new LomutoPartitionScheme(pivotSelector),
                PartitionSchemeType.HOARE => new HoarePartitionScheme(pivotSelector),
                PartitionSchemeType.STABLE => new StablePartitionScheme(pivotSelector),
                PartitionSchemeType.THREE_WAY => new ThreeWayPartitionScheme(pivotSelector),
                _ => throw new InvalidOperationException()
            };
        }

        private PivotSelector MakePivotSelector() {
            return this.pivotSelectorType switch {
                PivotSelectorType.FIRST => PivotSelectors.First,
                PivotSelectorType.MIDDLE => PivotSelectors.Middle,
                PivotSelectorType.LAST => PivotSelectors.Last,
                PivotSelectorType.RANDOM => PivotSelectors.Random,
                PivotSelectorType.MEDIAN_OF_THREE => PivotSelectors.MedianOfThree,
                _ => throw new InvalidOperationException(),
            };
        }

        private Tuple<PartitionPointDistance, PartitionPointDistance> MakePartitionPointDistances() {
            return this.partitionSchemeType switch {
                PartitionSchemeType.LOMUTO => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.LeftOne, PartitionPointDistances.RightOne),
                PartitionSchemeType.HOARE => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.At, PartitionPointDistances.RightOne),
                PartitionSchemeType.STABLE => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.LeftOne, PartitionPointDistances.RightOne),
                PartitionSchemeType.THREE_WAY => new Tuple<PartitionPointDistance, PartitionPointDistance>(PartitionPointDistances.At, PartitionPointDistances.At),
                _ => throw new InvalidOperationException()
            };
        }
    }

    internal abstract class QuickSortAlgorithm : IEquatable<QuickSortAlgorithm> {
        protected readonly PartitionScheme partitionScheme;
        protected readonly PartitionPointDistance left;
        protected readonly PartitionPointDistance right;

        internal QuickSortAlgorithm(PartitionScheme partitionScheme, PartitionPointDistance left, PartitionPointDistance right) {
            this.partitionScheme = partitionScheme;
            this.left = left;
            this.right = right;
        }

        internal abstract void Sort(IList list, int low, int high, IComparer comparer);

        public bool Equals(QuickSortAlgorithm? algorithm) {
            return base.GetType().Equals(algorithm!.GetType()) && this.partitionScheme.Equals(algorithm!.partitionScheme);
        }

        public override int GetHashCode() {
            return base.GetType().GetHashCode() + this.partitionScheme.GetHashCode();
        }
    }

    internal class RecursiveQuickSortAlgorithm : QuickSortAlgorithm {
        internal RecursiveQuickSortAlgorithm(PartitionScheme partitionScheme, PartitionPointDistance left, PartitionPointDistance right)
            : base(partitionScheme, left, right)
        {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList list, int low, int high, IComparer comparer) {
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

    internal class AsyncRecursiveQuickSortAlgorithm : QuickSortAlgorithm {
        internal AsyncRecursiveQuickSortAlgorithm(PartitionScheme partitionScheme, PartitionPointDistance left, PartitionPointDistance right)
            : base(partitionScheme, left, right)
        {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            this.DoSort(list, low, high - 1, comparer);
        }

        private void DoSort(IList list, int low, int high, IComparer comparer) {
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
            }
        }
    }

    internal class IterativeQuickSortAlgorithm : QuickSortAlgorithm {
        internal IterativeQuickSortAlgorithm(PartitionScheme partitionScheme, PartitionPointDistance left, PartitionPointDistance right)
            : base(partitionScheme, left, right)
        {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            if (high - low > 1) {
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

    internal abstract class PartitionScheme : IEquatable<PartitionScheme> {
        internal readonly PivotSelector pivotSelector;

        internal PartitionScheme(PivotSelector pivotSelector) {
            this.pivotSelector = pivotSelector;
        }

        internal abstract int[] Partition(IList list, int low, int high, IComparer comparer);

        public bool Equals(PartitionScheme? partitionScheme) {
            return base.GetType().Equals(partitionScheme!.GetType()) &&
                    this.pivotSelector == partitionScheme!.pivotSelector;
        }

        public override int GetHashCode() {
            int pivotSelectorHashCode;

            if (this.pivotSelector == PivotSelectors.First) {
                pivotSelectorHashCode = 1555474920;
            }
            else if (this.pivotSelector == PivotSelectors.Middle) {
                pivotSelectorHashCode = -458183685;
            }
            else if (this.pivotSelector == PivotSelectors.Last) {
                pivotSelectorHashCode = -1807190262;
            }
            else if (this.pivotSelector == PivotSelectors.Random) {
                pivotSelectorHashCode = 22376691;
            }
            else { // this.pivotSelector == PivotSelectors.MedianOfThree
                pivotSelectorHashCode = 1236005618;
            }

            return pivotSelectorHashCode + this.GetType().GetHashCode();
        }
    }

    internal class LomutoPartitionScheme : PartitionScheme {
        internal LomutoPartitionScheme(PivotSelector pivotSelector) : base(pivotSelector) {
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, high);

            object? pivot = list[high];
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
        internal HoarePartitionScheme(PivotSelector pivotSelector) : base(pivotSelector) { 
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, low);

            object? pivot = list[low];
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
        internal StablePartitionScheme(PivotSelector pivotSelector) : base(pivotSelector) {
        }

        internal override int[] Partition(IList list, int low, int high, IComparer comparer) {
            int mid = low + (high + 1 - low) / 2;

            int pivotIndex = base.pivotSelector(list, low, high, comparer);
            SortUtils.Swap(list, pivotIndex, mid);

            ArrayList lesser = new ArrayList(list.Count);
            ArrayList greater = new ArrayList(list.Count);
            object? pivot = list[mid];

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

            foreach (object? val in lesser) {
                list[i] = val;
                ++i;
            }

            int partitionPoint = i;
            list[i] = pivot;
            ++i;

            foreach (object? val in greater) {
                list[i] = val;
                ++i;
            }

            return new int[] { partitionPoint };
        }
    }

    internal class ThreeWayPartitionScheme : PartitionScheme {
        internal ThreeWayPartitionScheme(PivotSelector pivotSelector) : base(pivotSelector) {
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
                object? pivot = list[high];

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
