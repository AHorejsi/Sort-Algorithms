using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    internal delegate void BrickSortAlgorithm(IList list, int low, int high, IComparer comparer);

    public class BrickSorter : CompareSorter {
        private readonly BrickSortAlgorithm algorithm;

        internal BrickSorter(BrickSortAlgorithm algorithm) {
            this.algorithm = algorithm;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            this.algorithm(list, low, high, comparer);
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

    public enum BrickSortAlgorithmType { SYNC, ASYNC }

    public static class BrickSortFactory {
        public static BrickSorter Make(BrickSortAlgorithmType type) {
            return type switch {
                BrickSortAlgorithmType.SYNC => new BrickSorter(BrickSortAlgorithms.Sync),
                BrickSortAlgorithmType.ASYNC => new BrickSorter(BrickSortAlgorithms.Async),
                _ => throw new NotImplementedException()
            };
        }
    }
}
