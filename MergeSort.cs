using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public class MergeSorter : ICompareSorter, IEquatable<MergeSorter> {
        private readonly MergeSortAlgorithm algorithm;

        internal MergeSorter(MergeSortAlgorithm algorithm) {
            this.algorithm = algorithm;
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            this.algorithm.Sort(list, low, high, comparer);
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as MergeSorter);
        }

        public bool Equals(MergeSorter? sorter) {
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

    public enum MergeType { OUT_OF_PLACE, IN_PLACE }
    public enum MergeSortType { RECURSIVE, ITERATIVE, ASYNC_RECURSIVE }

    public class MergeSortBuilder {
        private MergeType mergeType;
        private MergeSortType algorithmType;
        
        public MergeSortBuilder WithMerge(MergeType mergeType) {
            this.mergeType = mergeType;

            return this;
        }

        public MergeSortBuilder WithAlgorithm(MergeSortType algorithmType) {
            this.algorithmType = algorithmType;

            return this;
        }

        public MergeSorter Build() {
            MergeSortAlgorithm algorithm = this.MakeAlgorithm();

            return new MergeSorter(algorithm);
        }

        private MergeSortAlgorithm MakeAlgorithm() {
            Merger merger = this.MakeMerger();

            return this.algorithmType switch {
                MergeSortType.RECURSIVE => new RecursiveMergeSortAlgorithm(merger),
                MergeSortType.ASYNC_RECURSIVE => new AsyncRecursiveMergeSortAlgorithm(merger),
                MergeSortType.ITERATIVE => new IterativeMergeSortAlgorithm(merger),
                _ => throw new InvalidOperationException()
            };
        }

        private Merger MakeMerger() {
            return this.mergeType switch {
                MergeType.OUT_OF_PLACE => Mergers.OutOfPlaceMerge,
                MergeType.IN_PLACE => Mergers.InPlaceMerge,
                _ => throw new InvalidOperationException()
            };
        }
    }

    public delegate void Merger(IList list, int low, int mid, int high, IComparer comparer);

    internal static class Mergers {
        internal static void OutOfPlaceMerge(IList list, int low, int mid, int high, IComparer comparer) {
            ArrayList leftList = Mergers.MakeSublist(list, low, mid);
            ArrayList rightList = Mergers.MakeSublist(list, mid, high);

            int leftIndex = 0;
            int rightIndex = 0;
            int listIndex = low;

            while (leftIndex < leftList.Count && rightIndex < rightList.Count) {
                if (comparer.Compare(leftList[leftIndex], rightList[rightIndex]) <= 0) {
                    list[listIndex] = leftList[leftIndex];

                    ++leftIndex;
                }
                else {
                    list[listIndex] = rightList[rightIndex];

                    ++rightIndex;
                }

                ++listIndex;
            }

            while (leftIndex < leftList.Count) {
                list[listIndex] = leftList[leftIndex];

                ++leftIndex;
                ++listIndex;
            }

            while (rightIndex < rightList.Count) {
                list[listIndex] = rightList[rightIndex];

                ++rightIndex;
                ++listIndex;
            }
        }

        private static ArrayList MakeSublist(IList list, int low, int high) {
            ArrayList sublist = new ArrayList(high - low);

            for (int index = low; index < high; ++index) {
                sublist.Add(list[index]);
            }

            return sublist;
        }

        internal static void InPlaceMerge(IList list, int low, int mid, int high, IComparer comparer) {
            int start = mid;

            while (low < mid && start < high) {
                if (comparer.Compare(list[low], list[start]) <= 0) {
                    ++low;
                }
                else {
                    object? value = list[start];
                    int index = start;

                    while (index != low) {
                        list[index] = list[index - 1];
                        --index;
                    }
                        
                    list[low] = value;

                    ++low;
                    ++mid;
                    ++start;
                }
            }
        }
    }

    internal abstract class MergeSortAlgorithm : IEquatable<MergeSortAlgorithm> {
        protected Merger merger;

        internal MergeSortAlgorithm(Merger merger) {
            this.merger = merger;
        }

        internal abstract void Sort(IList list, int low, int high, IComparer comparer);

        protected void SwapAdjacent(IList list, int index, IComparer comparer) {
            int nextIndex = index + 1;

            if (comparer.Compare(list[index], list[nextIndex]) > 0) {
                SortUtils.Swap(list, index, nextIndex);
            }
        }

        public bool Equals(MergeSortAlgorithm? algorithm) {
            return this.merger == algorithm!.merger;
        }

        public override int GetHashCode() {
            int mergerHashCode;

            if (this.merger == Mergers.OutOfPlaceMerge) {
                mergerHashCode = 1528339773;
            }
            else { // this.merger == Mergers.InPlaceMerge
                mergerHashCode = 305729187;
            }

            return this.GetType().GetHashCode() + mergerHashCode;
        }
    }

    internal class RecursiveMergeSortAlgorithm : MergeSortAlgorithm {
        internal RecursiveMergeSortAlgorithm(Merger merger) : base(merger) {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            int size = high - low;

            if (size > 1) {
                if (2 == size) {
                    base.SwapAdjacent(list, low, comparer);
                }
                else {
                    int mid = low + (size / 2);

                    this.Sort(list, low, mid, comparer);
                    this.Sort(list, mid, high, comparer);

                    base.merger(list, low, mid, high, comparer);
                }
            }
        }
    }

    internal class AsyncRecursiveMergeSortAlgorithm : MergeSortAlgorithm {
        internal AsyncRecursiveMergeSortAlgorithm(Merger merger) : base(merger) {
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            int size = high - low;

            if (size > 1) {
                if (2 == size) {
                    base.SwapAdjacent(list, low, comparer);
                }
                else {
                    int mid = low + (size / 2);

                    Task task1 = Task.Run(() => { this.Sort(list, low, mid, comparer); });
                    Task task2 = Task.Run(() => { this.Sort(list, mid, high, comparer); });

                    Task.WaitAll(task1, task2);

                    base.merger(list, low, mid, high, comparer);
                }
            }
        }
    }

    internal class IterativeMergeSortAlgorithm : MergeSortAlgorithm {
        internal IterativeMergeSortAlgorithm(Merger merger) : base(merger) { 
        }

        internal override void Sort(IList list, int low, int high, IComparer comparer) {
            int size = high - low;
            int end = high - 1;

            for (int currentSize = 1; currentSize < size; currentSize *= 2) {
                for (int leftStart = low; leftStart < size; leftStart += currentSize * 2) {
                    int mid = Math.Min(leftStart + currentSize, end);
                    int rightEnd = Math.Min(leftStart + 2 * currentSize, size);

                    base.merger(list, leftStart, mid, rightEnd, comparer);
                }
            }
        }
    }
}
