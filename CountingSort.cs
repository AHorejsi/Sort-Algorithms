using System;
using System.Collections;

namespace Sorting {
    public class CountingSorter : IntegerSorter, IEquatable<CountingSorter> {
        private static CountingSorter? SINGLETON = null;

        private CountingSorter() {
        }

        public static CountingSorter Singleton {
            get { 
                if (CountingSorter.SINGLETON is null) {
                    CountingSorter.SINGLETON = new CountingSorter();
                }

                return CountingSorter.SINGLETON;
            }
        }

        public override void Sort(IList list, int low, int high) {
            int minimum = this.FindMinimum(list, low, high);
            int maximum = this.FindMaximum(list, low, high);
            int range = maximum - minimum + 1;

            int[] counts = new int[range];
            object[] result = new object[high - low];

            for (int index = low; index < high; ++index) {
                ++(counts[(int)(list[index])! - minimum]);
            }

            for (int index = 1; index < range; ++index) {
                counts[index] += counts[index - 1];
            }

            for (int index = high - 1; index >= low; --index) {
                result[counts[(int)(list[index])! - minimum] - 1] = list[index]!;
                --(counts[(int)(list[index])! - minimum]);
            }

            this.Move(list, low, result);
        }

        private int FindMinimum(IList list, int low, int high) {
            int minimum = (int)list[low]!;
            
            for (int index = low + 1; index < high; ++index) {
                int current = (int)list[index]!;

                if (minimum > current) {
                    minimum = current;
                } 
            }

            return minimum;
        }

        private int FindMaximum(IList list, int low, int high) {
            int maximum = (int)list[low]!;

            for (int index = low + 1; index < high; ++index) {
                int current = (int)list[index]!;

                if (maximum < current) {
                    maximum = current;
                }
            }

            return maximum;
        }

        private void Move(IList list, int low, object[] result) {
            int index = low;

            foreach (object? val in result) {
                list[index] = val;
                ++index;
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as CountingSorter);
        }

        public bool Equals(CountingSorter? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
