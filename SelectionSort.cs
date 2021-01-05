using System.Collections.Generic;

namespace Sorting {
    public abstract class SelectionSorter<N> : ICompareSorter<N> {
        internal SelectionSorter() {
        }

        public abstract void Sort(IList<N> list, int low, int high, IComparer<N> comparer);
    }

    public sealed class StandardSelectionSorter<N> : SelectionSorter<N> {
        internal StandardSelectionSorter() : base() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            for (int i = low; i < high; ++i) {
                int minIndex = this.FindMin(list, i, high, comparer);

                if (i != minIndex) {
                    SortUtils.Swap(list, i, minIndex);
                }
            }
        }

        private int FindMin(IList<N> list, int low, int high, IComparer<N> comparer) {
            int minIndex = low;

            for (int i = low + 1; i < high; ++i) {
                if (comparer.Compare(list[i], list[minIndex]) < 0) {
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }

    public sealed class StableSelectionSorter<N> : SelectionSorter<N> {
        internal StableSelectionSorter() : base() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            for (int i = low; i < high; ++i) {
                int minIndex = this.FindMin(list, i, high, comparer);

                if (i != minIndex) {
                    this.StableSwap(list, i, minIndex);
                }
            }
        }

        private int FindMin(IList<N> list, int low, int high, IComparer<N> comparer) {
            int minIndex = low;

            for (int i = low + 1; i < high; ++i) {
                if (comparer.Compare(list[i], list[minIndex]) < 0) {
                    minIndex = i;
                }
            }

            return minIndex;
        }

        private void StableSwap(IList<N> list, int i, int j) {
            if (j > i) {
                N temp = list[j];

                for (int index = j; index > i; --index) {
                    list[index] = list[index - 1];
                }

                list[i] = temp;
            }
            else {
                N temp = list[i];

                for (int index = i; index < j; ++index) {
                    list[index - 1] = list[index];
                }

                list[j] = temp;
            }
        }
    }

    public sealed class DoubleSelectionSorter<N> : SelectionSorter<N> {
        internal DoubleSelectionSorter() : base() {
        }

        public override void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            while (low < high) {
                (int minIndex, int maxIndex) = this.FindMinAndMaxIndices(list, low, high, comparer);
                int end = high - 1;

                SortUtils.Swap(list, low, minIndex);

                if (comparer.Compare(list[minIndex], list[maxIndex]) > 0) {
                    SortUtils.Swap(list, end, minIndex);
                }
                else {
                    SortUtils.Swap(list, end, maxIndex);
                }

                ++low;
                --high;
            }
        }
        private (int, int) FindMinAndMaxIndices(IList<N> list, int index, int high, IComparer<N> comparer) {
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

            return (minIndex, maxIndex);
        }
    }

    public static class SelectionSortFactory<N> {

    }
}
