using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public sealed class EnumerationSorter<N> : ICompareSorter<N>, IEquatable<EnumerationSorter<N>> {
        private class EnumPosition {
            public N Value {
                get;
            }
            public int SortedIndex {
                get;
            }

            public EnumPosition(N value, int sortedIndex) {
                this.Value = value;
                this.SortedIndex = sortedIndex;
            }
        }

        public EnumerationSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            var positionArray = new EnumPosition[high - low];

            Parallel.For(low, high, (index) => {
                int sortedIndex = low;
                N obj = list[index];

                for (int currentIndex = low; currentIndex < high; ++currentIndex) {
                    if (index != currentIndex) {
                        int comparison = comparer.Compare(obj, list[currentIndex]);

                        if (comparison > 0 || (0 == comparison && currentIndex < index)) {
                            ++sortedIndex;
                        }
                    }
                }

                positionArray[index - low] = new EnumPosition(obj, sortedIndex);
            });

            Parallel.ForEach(positionArray, (EnumPosition enumPosition) => {
                list[enumPosition.SortedIndex] = enumPosition.Value;
            });
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as EnumerationSorter<N>);
        }

        public bool Equals(EnumerationSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}