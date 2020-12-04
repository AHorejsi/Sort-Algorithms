using System;
using System.Collections.Generic;

namespace Sorting {
    internal delegate int Searcher<N>(IList<N> list, int low, int index, IComparer<N> comparer);

    public sealed class InsertionSorter<N> : ICompareSorter<N> {
        private readonly Searcher<N> searcher;

        internal InsertionSorter(Searcher<N> searcher) {
            this.searcher = searcher;
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            for (int i = low + 1; i < high; ++i) {
                int sortedPosition = this.searcher(list, low, i, comparer);

                for (int j = i; j > sortedPosition; --j) {
                    SortUtils.Swap(list, j, j - 1);
                }
            }
        }
    }

    internal static class Searchers {
        public static int Linear<N>(IList<N> list, int low, int high, IComparer<N> comparer) {
            int index = low;

            while (index < high) {
                if (comparer.Compare(list[high], list[index]) < 0) {
                    break;
                }

                ++index;
            }

            return index;
        }

        public static int Binary<N>(IList<N> list, int low, int high, IComparer<N> comparer) {
            return Searchers.DoBinary(list, low, high, list[high], comparer);
        }

        public static int Exponential<N>(IList<N> list, int low, int high, IComparer<N> comparer) {
            if (comparer.Compare(list[low], list[high]) == 0) {
                return low;
            }

            int index = low + 1;
            while (index < high && comparer.Compare(list[index], list[high]) <= 0) {
                index *= 2;
            }

            return Searchers.DoBinary(list, index / 2, Math.Min(index, high), list[high], comparer);
        }

        private static int DoBinary<N>(IList<N> list, int low, int high, N key, IComparer<N> comparer) {
            int left = low;
            int right = high - 1;

            while (left <= right) {
                int mid = left + (right - left) / 2;
                int comparison = comparer.Compare(list[mid], key);

                if (comparison < 0) {
                    left = mid + 1;
                }
                else if (comparison > 0) {
                    right = mid - 1;
                }
                else {
                    return mid;
                }
            }

            return left;
        }

        public static int Jump<N>(IList<N> list, int low, int high, IComparer<N> comparer) {
            int size = high - low;
            int step = (int)Math.Sqrt(size);
            int prev = 0;

            while (comparer.Compare(list[Math.Min(step, size) - 1], list[high]) < 0) {
                prev = step;
                step += step;

                if (prev >= size) {
                    return prev;
                }
            }

            while (comparer.Compare(list[prev], list[high]) < 0) {
                ++prev;

                if (prev == Math.Min(step, size)) {
                    break;
                }
            }

            return prev;
        }
    }

    public enum SearchType { LINEAR, BINARY, EXPONENTIAL, JUMP }

    public static class InsertionSortFactory<N> {
        public static InsertionSorter<N> Make(SearchType type) {
            Searcher<N> searcher = type switch
            {
                SearchType.LINEAR => Searchers.Linear,
                SearchType.BINARY => Searchers.Binary,
                SearchType.EXPONENTIAL => Searchers.Exponential,
                SearchType.JUMP => Searchers.Jump,
                _ => throw new InvalidOperationException(),
            };

            return new InsertionSorter<N>(searcher);
        }
    }
}
