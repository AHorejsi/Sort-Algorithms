using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public class EnumerationSorter : ICompareSorter, IEquatable<EnumerationSorter> {
        private static EnumerationSorter? instance = null;

        private EnumerationSorter() {
        }

        public static EnumerationSorter Singleton {
            get {
                if (EnumerationSorter.instance is null) {
                    EnumerationSorter.instance = new EnumerationSorter();
                }

                return EnumerationSorter.instance;
            }
        }

        public void Sort(IList list, int low, int high, IComparer comparer) {
            EnumPosition[] positionArray = new EnumPosition[high - low];

            Parallel.For(low, high, (index) => {
                int sortedIndex = low;
                object? obj = list[index];

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

            Parallel.ForEach(positionArray, (enumPosition) => {
                list[enumPosition.SortedIndex] = enumPosition.Value;
            });
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as EnumerationSorter);
        }

        public bool Equals(EnumerationSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }

    internal class EnumPosition {
        public object? Value {
            get;
        }
        public int SortedIndex {
            get;
        }

        public EnumPosition(object? value, int sortedIndex) {
            this.Value = value;
            this.SortedIndex = sortedIndex;
        }
    }
}
