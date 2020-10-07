using System;
using System.Collections;

namespace Sorting {
    internal delegate void SelectionSortAlgorithm(IList list, int low, int high, IComparer comparer);

    public class SelectionSorter : CompareSorter {
        private readonly SelectionSortAlgorithm algorithm;

        internal SelectionSorter(SelectionSortAlgorithm algorithm) {
            this.algorithm = algorithm;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            this.algorithm(list, low, high, comparer);
        }
    }

    internal static class SelectionSortAlgorithms {
        public static void Standard(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high; ++index) {
                int minIndex = SelectionSortAlgorithms.FindMinIndex(list, index, high, comparer);

                if (minIndex != index) {
                    SortUtils.Swap(list, index, minIndex);
                }
            }
        }

        public static void Stable(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high; ++index) {
                int minIndex = SelectionSortAlgorithms.FindMinIndex(list, index, high, comparer);

                if (minIndex != index) {
                    SelectionSortAlgorithms.StableSwap(list, index, minIndex);
                }
            }
        }

        public static void StableSwap(IList list, int targetIndex, int index) {
            object temp = list[targetIndex];

            while (index > targetIndex) {
                SortUtils.Swap(list, index, index - 1);

                --index;
            }

            list[targetIndex] = temp;
        }

        private static int FindMinIndex(IList list, int index, int high, IComparer comparer) {
            int minIndex = index;
            ++index;

            while (index < high) {
                if (comparer.Compare(list[index], list[minIndex]) < 0) {
                    minIndex = index;
                }

                ++index;
            }

            return minIndex;
        }

        public static void Double(IList list, int low, int high, IComparer comparer) {
            while (low < high) {
                Tuple<int, int> minAndMax = SelectionSortAlgorithms.FindMinAndMaxIndices(list, low, high, comparer);
                int end = high - 1;

                SortUtils.Swap(list, low, minAndMax.Item1);

                if (comparer.Compare(list[minAndMax.Item1], list[minAndMax.Item2]) > 0) {
                    SortUtils.Swap(list, end, minAndMax.Item1);
                }
                else {
                    SortUtils.Swap(list, end, minAndMax.Item2);
                }

                ++low;
                --high;
            }
        }

        private static Tuple<int, int> FindMinAndMaxIndices(IList list, int index, int high, IComparer comparer) {
            int minIndex = index;
            int maxIndex = index;
            ++index;

            while (index < high) {
                if (comparer.Compare(list[index], list[minIndex]) < 0) {
                    minIndex = index;
                }
                if (comparer.Compare(list[index], list[maxIndex]) > 0) {
                    maxIndex = index;
                }

                ++index;
            }

            return new Tuple<int, int>(minIndex, maxIndex);
        }
    }

    public enum SelectionSortAlgorithmType { STANDARD, STABLE, DOUBLE }

    public static class SelectionSortFactory {
        public static SelectionSorter Make(SelectionSortAlgorithmType type) {
            SelectionSortAlgorithm algorithm = type switch {
                SelectionSortAlgorithmType.STANDARD => SelectionSortAlgorithms.Standard,
                SelectionSortAlgorithmType.STABLE => SelectionSortAlgorithms.Stable,
                SelectionSortAlgorithmType.DOUBLE => SelectionSortAlgorithms.Double,
                _ => throw new InvalidOperationException()
            };

            return new SelectionSorter(algorithm);
        }
    }
}
