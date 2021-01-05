using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    internal delegate void Merger<N>(IList<N> list, int low, int mid, int high, IComparer<N> comparer);

    internal static class Mergers {
        internal static void OutOfPlaceMerge<N>(IList<N> list, int low, int mid, int high, IComparer<N> comparer) {
            List<N> leftList = Mergers.MakeSublist(list, low, mid);
            List<N> rightList = Mergers.MakeSublist(list, mid, high);

            var leftIndex = 0;
            var rightIndex = 0;
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

        private static List<N> MakeSublist<N>(IList<N> list, int low, int high) {
            var sublist = new List<N>(high - low);

            for (int index = low; index < high; ++index) {
                sublist.Add(list[index]);
            }

            return sublist;
        }

        internal static void InPlaceMerge<N>(IList<N> list, int low, int mid, int high, IComparer<N> comparer) {
            int start = mid;

            while (low < mid && start < high) {
                if (comparer.Compare(list[low], list[start]) <= 0) {
                    ++low;
                }
                else {
                    N value = list[start];
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

    public abstract class MergeSorter<N> : ICompareSorter<N> {
        internal Merger<N> merger;

        internal MergeSorter(Merger<N> merger) {
            this.merger = merger;
        }

        public abstract void Sort(IList<N> list, int low, int high, IComparer<N> comparer);

        protected void SwapAdjacent(IList<N> list, int index, IComparer<N> comparer) {
            int nextIndex = index + 1;

            if (comparer.Compare(list[index], list[nextIndex]) > 0) {
                SortUtils.Swap(list, index, nextIndex);
            }
        }
    }

    public sealed class RecursiveMergeSorter<N> : MergeSorter<N> {
        internal RecursiveMergeSorter(Merger<N> merger) : base(merger) {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
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

    public sealed class AsyncRecursiveMergeSorter<N> : MergeSorter<N> {
        internal AsyncRecursiveMergeSorter(Merger<N> merger) : base(merger) {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
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

                    task1.Dispose();
                    task2.Dispose();

                    base.merger(list, low, mid, high, comparer);
                }
            }
        }
    }

    public sealed class IterativeMergeSorter<N> : MergeSorter<N> {
        internal IterativeMergeSorter(Merger<N> merger) : base(merger) { 
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            int size = high - low;
            int end = high - 1;

            for (var currentSize = 1; currentSize < size; currentSize *= 2) {
                for (int leftStart = low; leftStart < size; leftStart += currentSize * 2) {
                    int mid = Math.Min(leftStart + currentSize, end);
                    int rightEnd = Math.Min(leftStart + 2 * currentSize, size);

                    base.merger(list, leftStart, mid, rightEnd, comparer);
                }
            }
        }
    }

    public enum MergeType { OUT_OF_PLACE, IN_PLACE }
    public enum MergeSortType { ITERATIVE, RECURSIVE, ASYNC_RECURSIVE }

    public class MergeSortBuilder<N> {
        private MergeType mergeType;
        private MergeSortType algorithmType;

        public MergeSortBuilder<N> WithMerge(MergeType mergeType) {
            this.mergeType = mergeType;

            return this;
        }

        public MergeSortBuilder<N> WithAlgorithm(MergeSortType algorithmType) {
            this.algorithmType = algorithmType;

            return this;
        }

        public MergeSorter<N> Build() {
            Merger<N> merger = this.MakeMerger();

            return this.algorithmType switch {
                MergeSortType.RECURSIVE => new RecursiveMergeSorter<N>(merger),
                MergeSortType.ASYNC_RECURSIVE => new AsyncRecursiveMergeSorter<N>(merger),
                MergeSortType.ITERATIVE => new IterativeMergeSorter<N>(merger),
                _ => throw new InvalidOperationException()
            };
        }

        private Merger<N> MakeMerger() {
            return this.mergeType switch
            {
                MergeType.OUT_OF_PLACE => Mergers.OutOfPlaceMerge,
                MergeType.IN_PLACE => Mergers.InPlaceMerge,
                _ => throw new InvalidOperationException()
            };
        }
    }
}
