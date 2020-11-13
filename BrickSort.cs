using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    internal delegate void BrickSortAlgorithm(IList list, int low, int high, IComparer comparer);

    public class BrickSorter : ICompareSorter, IEquatable<BrickSorter> {
        private readonly BrickSortAlgorithm algorithm;

        internal BrickSorter(BrickSortAlgorithm algorithm) {
            this.algorithm = algorithm;
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            this.algorithm(list, low, high, comparer);
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as BrickSorter);
        }

        public bool Equals(BrickSorter? sorter) {
            if (sorter is null) {
                return false;
            }
            else {
                return this.algorithm == sorter.algorithm;
            }
        }

        public override int GetHashCode() {
            int algorithmHashCode;

            if (this.algorithm == BrickSortAlgorithms.Sync) {
                algorithmHashCode = -1784792866;
            }
            else { //this.algorithm == BrickSortAlgorithms.Async 
                algorithmHashCode = 1743121670;
            }

            return algorithmHashCode;
        }
    }

    internal static class BrickSortAlgorithms {
        internal static void Sync(IList list, int low, int high, IComparer comparer) {
            bool isSortedOnEvenIndices;
            bool isSortedOnOddIndices;

            do {
                isSortedOnEvenIndices = BrickSortAlgorithms.BubbleUpEvenIndices(list, low, high, comparer);
                isSortedOnOddIndices = BrickSortAlgorithms.BubbleUpOddIndices(list, low, high, comparer);
            } while (!(isSortedOnEvenIndices && isSortedOnOddIndices));
        }

        internal static void Async(IList list, int low, int high, IComparer comparer) {
            bool isSortedOnEvenIndices = false;
            bool isSortedOnOddIndices = false;

            void evenIndexSorter() { isSortedOnEvenIndices = BrickSortAlgorithms.BubbleUpEvenIndices(list, low, high, comparer); }
            void oddIndexSorter() { isSortedOnOddIndices = BrickSortAlgorithms.BubbleUpOddIndices(list, low, high, comparer); }

            do {
                Task task1 = Task.Run(evenIndexSorter);
                Task task2 = Task.Run(oddIndexSorter);

                Task.WaitAll(task1, task2);
            } while (!(isSortedOnEvenIndices && isSortedOnOddIndices));
        }

        private static bool BubbleUpEvenIndices(IList list, int low, int high, IComparer comparer) {
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

        private static bool BubbleUpOddIndices(IList list, int low, int high, IComparer comparer) {
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

    public enum BrickSortType { SYNC, ASYNC }

    public static class BrickSortFactory {
        public static BrickSorter Make(BrickSortType type) {
            return type switch {
                BrickSortType.SYNC => new BrickSorter(BrickSortAlgorithms.Sync),
                BrickSortType.ASYNC => new BrickSorter(BrickSortAlgorithms.Async),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
