using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public sealed class EnumerationSorter<N> : ICompareSorter<N> {
        public EnumerationSorter() {
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            EnumPosition<N>[] positionArray = new EnumPosition<N>[high - low];

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

                positionArray[index - low] = new EnumPosition<N>(obj, sortedIndex);
            });

            Parallel.ForEach(positionArray, (EnumPosition<N> enumPosition) => {
                list[enumPosition.SortedIndex] = enumPosition.Value;
            });
        }
    }

    internal class EnumPosition<N> {
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
}