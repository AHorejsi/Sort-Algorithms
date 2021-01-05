using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public abstract class BrickSorter<N> : ICompareSorter<N> {
        public abstract void Sort(IList<N> list, int low, int high, IComparer<N> comparer);
    }

    public sealed class SyncBrickSorter<N> : BrickSorter<N> {
        internal SyncBrickSorter() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            bool isSortedOnEvenIndices;
            bool isSortedOnOddIndices;

            do {
                isSortedOnEvenIndices = this.BubbleUpEvenIndices(list, low, high, comparer);
                isSortedOnOddIndices = this.BubbleUpOddIndices(list, low, high, comparer);
            } while (!(isSortedOnEvenIndices && isSortedOnOddIndices));
        }

        private bool BubbleUpEvenIndices(IList<N> list, int low, int high, IComparer<N> comparer) {
            var isSortedOnEvenIndices = true;

            for (int index = low; index < high - 1; index += 2) {
                int nextIndex = index + 1;

                if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                    SortUtils.Swap(list, index, nextIndex);
                    isSortedOnEvenIndices = false;
                }
            }

            return isSortedOnEvenIndices;
        }

        private bool BubbleUpOddIndices(IList<N> list, int low, int high, IComparer<N> comparer) {
            var isSortedOnOddIndices = true;

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

    public sealed class AsyncBrickSorter<N> : BrickSorter<N> {
        internal AsyncBrickSorter() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            var isSortedOnEvenIndices = false;
            var isSortedOnOddIndices = false;

            void evenIndexSorter() { isSortedOnEvenIndices = this.BubbleUpEvenIndices(list, low, high, comparer); }
            void oddIndexSorter() { isSortedOnOddIndices = this.BubbleUpOddIndices(list, low, high, comparer); }

            do {
                Task task1 = Task.Run(evenIndexSorter);
                Task task2 = Task.Run(oddIndexSorter);

                Task.WaitAll(task1, task2);

                task1.Dispose();
                task2.Dispose();
            } while (!(isSortedOnEvenIndices && isSortedOnOddIndices));
        }

        private bool BubbleUpEvenIndices(IList<N> list, int low, int high, IComparer<N> comparer) {
            var isSortedOnEvenIndices = true;

            for (int index = low; index < high - 1; index += 2) {
                int nextIndex = index + 1;

                if (comparer.Compare(list[nextIndex], list[index]) < 0) {
                    SortUtils.Swap(list, index, nextIndex);
                    isSortedOnEvenIndices = false;
                }
            }

            return isSortedOnEvenIndices;
        }

        private bool BubbleUpOddIndices(IList<N> list, int low, int high, IComparer<N> comparer) {
            var isSortedOnOddIndices = true;

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

    public enum BrickSortType { SYNC, ASYNC }

    public static class BrickSortFactory<N> {
        public static BrickSorter<N> Make(BrickSortType type) {
            return type switch
            {
                BrickSortType.SYNC => new SyncBrickSorter<N>(),
                BrickSortType.ASYNC => new AsyncBrickSorter<N>(),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
