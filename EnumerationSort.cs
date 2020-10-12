using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    /*public class EnumerationSorter : CompareSorter {
        private static EnumerationSorter? SINGLETON = null;

        private EnumerationSorter() {
        }

        public static EnumerationSorter Singleton {
            get {
                if (EnumerationSorter.SINGLETON is null) {
                    EnumerationSorter.SINGLETON = new EnumerationSorter();
                }

                return EnumerationSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            List<EnumPosition> enumPositionList = new List<EnumPosition>(high - low);
            HashSet<CurrentPosition> currentPositionSet = new HashSet<CurrentPosition>();

            Parallel.For(low, high, (index) => {
                int amount = 0;
                object? value = list[index];

                for (int currentIndex = low; currentIndex < high; ++currentIndex) {
                    if (currentIndex != index) {
                        if (comparer.Compare(value, list[currentIndex]) > 0) {
                            ++amount;
                        }
                    }
                }
            });

            Parallel.ForEach(enumPositionList, (item) => {
                list[item.SortedIndex] = item.Value;
            });
        }
    }

    internal class EnumPosition : IEquatable<EnumPosition> {
        public object? Value {
            get;
        }
        public int SortedIndex {
            get;
        }

        public EnumPosition(object value, int sortedIndex) {
            this.Value = value;
            this.SortedIndex = sortedIndex;
        }
    }

    internal class CurrentPosition : IEquatable<CurrentPosition> {
        public object? Value {
            get;
        }
        public int CurrentIndex {
            get;
        }

        public CurrentPosition(object? value, int currentIndex) {
            this.Value = value;
            this.CurrentIndex = currentIndex;
        }
    }*/
}
