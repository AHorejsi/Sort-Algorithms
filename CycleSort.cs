using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class CycleSorter<N> : ICompareSorter<N>, IEquatable<CycleSorter<N>> {
        public CycleSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            int end = high - 1;

            for (int cycleStart = low; cycleStart != end; ++cycleStart) {
                N item = list[cycleStart];
                int position = cycleStart;

                for (int i = cycleStart + 1; i < high; ++i) {
                    if (comparer.Compare(list[i], item) < 0) {
                        ++position;
                    }
                }

                if (position != cycleStart) {
                    while (0 == comparer.Compare(list[position], item)) {
                        ++position;
                    }

                    if (position != cycleStart) {
                        N temp = item;
                        item = list[position];
                        list[position] = temp;
                    }

                    while (position != cycleStart) {
                        position = cycleStart;

                        for (int i = cycleStart + 1; i < high; ++i) {
                            if (comparer.Compare(list[i], item) < 0) {
                                ++position;
                            }
                        }

                        while (0 == comparer.Compare(item, list[position])) {
                            ++position;
                        }

                        int comparison = comparer.Compare(item, list[position]);

                        if (comparison < 0 || comparison > 0) {
                            N temp = item;
                            item = list[position];
                            list[position] = temp;
                        }
                    }
                }
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as CycleSorter<N>);
        }

        public bool Equals(CycleSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
