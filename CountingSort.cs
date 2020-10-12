using System;
using System.Collections;
using System.Collections.Generic;

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

            List<int> counts = new List<int>(range);
            ArrayList result = new ArrayList(high - low);

            this.FillWithZeroes(counts, result);

            for (int index = low; index < high; ++index) {
                ++(counts[(int)(list[index])! - minimum]);
            }

            for (int index = 1; index < range; ++index) {
                counts[index] += counts[index - 1];
            }

            for (int index = high - 1; index >= low; --index) {
                result[counts[(int)(list[index])! - minimum] - 1] = list[index];
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

        private void FillWithZeroes(List<int> counts, ArrayList result) {
            for (int index = 0; index < counts.Capacity; ++index) {
                counts.Add(0);
            }

            for (int index = 0; index < result.Capacity; ++index) {
                result.Add(0);
            }
        }

        private void Move(IList list, int low, ArrayList result) {
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
            Type type = this.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
