using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public abstract class BrickSorter<N> : ICompareSorter<N> {
        public abstract void Sort(IList<N> list, int low, int high, IComparer<N> comparer);

        protected bool BubbleUpEvenIndices(IList<N> list, int low, int high, IComparer<N> comparer) {
            bool isSortedOnEvenIndices = true;

            for (int index = low; index < high - 1; index += 2) {
                int nextIndex = index + 1;

                if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                    SortUtils.Swap(list, index, nextIndex);
                    isSortedOnEvenIndices = false;
                }
            }

            return isSortedOnEvenIndices;
        }

        protected bool BubbleUpOddIndices(IList<N> list, int low, int high, IComparer<N> comparer) {
            bool isSortedOnOddIndices = true;

            for (int index = low + 1; index < high - 1; index += 2) {
                int nextIndex = index + 1;

                if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                    SortUtils.Swap(list, index, nextIndex);
                    isSortedOnOddIndices = false;
                }
            }

            return isSortedOnOddIndices;
        }
    }

    public sealed class SyncBrickSorter<N> : BrickSorter<N> {
        internal SyncBrickSorter() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            bool isSortedOnEvenIndices;
            bool isSortedOnOddIndices;

            do {
                isSortedOnEvenIndices = base.BubbleUpEvenIndices(list, low, high, comparer);
                isSortedOnOddIndices = base.BubbleUpOddIndices(list, low, high, comparer);
            } while (!(isSortedOnEvenIndices && isSortedOnOddIndices));
        }
    }

    public sealed class AsyncBrickSorter<N> : BrickSorter<N> {
        internal AsyncBrickSorter() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            bool isSortedOnEvenIndices = false;
            bool isSortedOnOddIndices = false;

            void evenIndexSorter() { isSortedOnEvenIndices = base.BubbleUpEvenIndices(list, low, high, comparer); }
            void oddIndexSorter() { isSortedOnOddIndices = base.BubbleUpOddIndices(list, low, high, comparer); }

            do {
                Task task1 = Task.Run(evenIndexSorter);
                Task task2 = Task.Run(oddIndexSorter);

                Task.WaitAll(task1, task2);
            } while (!(isSortedOnEvenIndices && isSortedOnOddIndices));
        }
    }

    public enum BrickSortType { SYNC, ASYNC }

    public static class BrickSortFactory {
        public static BrickSorter<N> Make<N>(BrickSortType type) {
            return type switch
            {
                BrickSortType.SYNC => new SyncBrickSorter<N>(),
                BrickSortType.ASYNC => new AsyncBrickSorter<N>(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
