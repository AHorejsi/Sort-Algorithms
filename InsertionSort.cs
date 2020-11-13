using System;
using System.Collections;

namespace Sorting {
    internal delegate int Searcher(IList list, int low, int index, IComparer comparer);

    public class InsertionSorter : ICompareSorter, IEquatable<InsertionSorter> {
        private readonly Searcher searcher;

        internal InsertionSorter(Searcher searcher) {
            this.searcher = searcher;
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            if (high - low > 1) {
                for (int i = low + 1; i < high; ++i) {
                    int sortedPosition = this.searcher(list, low, i, comparer);

                    for (int j = i; j > sortedPosition; --j) {
                        SortUtils.Swap(list, j, j - 1);
                    }
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as InsertionSorter);
        }

        public bool Equals(InsertionSorter? sorter) {
            if (sorter is null) {
                return false;
            }
            else {
                return this.searcher == sorter.searcher;
            }
        }

        public override int GetHashCode() {
            int searcherHashCode;

            if (Searchers.Linear == this.searcher) {
                searcherHashCode = 843941155;
            }
            else if (Searchers.Binary == this.searcher) {
                searcherHashCode = -1716325658;
            }
            else if (Searchers.Jump == this.searcher) {
                searcherHashCode = 1578338564;
            }
            else if (Searchers.Exponential == this.searcher) {
                searcherHashCode = -1639881796;
            }
            else { // Searchers.Fibonacci == this.searcher
                searcherHashCode = -848830997;
            }

            return searcherHashCode;
        }
    }

    internal static class Searchers {
        public static int Linear(IList list, int low, int high, IComparer comparer) {
            int index = low;

            while (index < high) {
                if (comparer.Compare(list[high], list[index]) < 0) {
                    break;
                }

                ++index;
            }

            return index;
        }

        public static int Binary(IList list, int low, int high, IComparer comparer) {
            return Searchers.DoBinary(list, low, high, list[high], comparer);
        }

        public static int Exponential(IList list, int low, int high, IComparer comparer) {
            if (comparer.Compare(list[low], list[high]) == 0) {
                return low;
            }

            int index = low + 1;
            while (index < high && comparer.Compare(list[index], list[high]) <= 0) {
                index *= 2;
            }

            return Searchers.DoBinary(list, index / 2, Math.Min(index, high), list[high], comparer);
        }

        private static int DoBinary(IList list, int low, int high, object? key, IComparer comparer) {
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

        public static int Jump(IList list, int low, int high, IComparer comparer) {
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

        public static int Fibonacci(IList list, int low, int high, IComparer comparer) {
            int[] fibonacciData = Searchers.GetFibonacciData(high);
            int offset = -1;

            while (fibonacciData[2] > low + 1) {
                int index = Math.Min(offset + fibonacciData[0], high - 1);
                int comparison = comparer.Compare(list[index], list[high]);

                if (comparison < 0) {
                    fibonacciData[2] = fibonacciData[1];
                    fibonacciData[1] = fibonacciData[0];
                    fibonacciData[0] = fibonacciData[2] - fibonacciData[1];

                    offset = index;
                }
                else if (comparison > 0) {
                    fibonacciData[2] = fibonacciData[0];
                    fibonacciData[1] = fibonacciData[1] - fibonacciData[0];
                    fibonacciData[0] = fibonacciData[2] - fibonacciData[1];
                }
                else {
                    return index;
                }
            }

            return offset + 1;
        }

        private static int[] GetFibonacciData(int high) {
            int fib0 = 0;
            int fib1 = 1;
            int fibN = fib0 + fib1;

            while (fibN < high) {
                fib0 = fib1;
                fib1 = fibN;
                fibN = fib0 + fib1;
            }

            return new int[] { fib0, fib1, fibN };
        }
    }

    public enum SearchType { LINEAR, BINARY, FIBONACCI, EXPONENTIAL, JUMP }

    public static class InsertionSortFactory {
        public static InsertionSorter Make(SearchType type) {
            Searcher searcher = type switch {
                SearchType.LINEAR => Searchers.Linear,
                SearchType.BINARY => Searchers.Binary,
                SearchType.FIBONACCI => Searchers.Fibonacci,
                SearchType.EXPONENTIAL => Searchers.Exponential,
                SearchType.JUMP => Searchers.Jump,
                _ => throw new InvalidOperationException(),
            };

            return new InsertionSorter(searcher);
        }
    }
}
