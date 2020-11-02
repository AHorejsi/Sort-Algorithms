using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public class CountingSorter<T> : IntegerSorter<T>, IEquatable<CountingSorter<T>> {
        private static CountingSorter<T>? SINGLETON = null;

        private CountingSorter() : base() {
        }

        public static CountingSorter<T> Singleton {
            get {
                if (CountingSorter<T>.SINGLETON is null) {
                    CountingSorter<T>.SINGLETON = new CountingSorter<T>();
                }

                return CountingSorter<T>.SINGLETON;
            }
        }

        public override void Sort(IList<T> list, int low, int high) {
            int minimum = this.FindMinimum(list, low, high);
            int maximum = this.FindMaximum(list, low, high);
            int range = maximum - minimum + 1;

            int[] counts = new int[range];
            T[] result = new T[high - low];

            for (int index = low; index < high; ++index) {
                ++(counts[Convert.ToInt32(list[index]) - minimum]);
            }

            for (int index = 1; index < range; ++index) {
                counts[index] += counts[index - 1];
            }

            for (int index = high - 1; index >= low; --index) {
                int indexOfCounts = Convert.ToInt32(list[index]) - minimum;

                result[counts[indexOfCounts] - 1] = list[index];
                --(counts[indexOfCounts]);
            }

            this.Move(list, low, result);
        }

        private int FindMinimum(IList<T> list, int low, int high) {
            int minimum = Convert.ToInt32(list[low]);

            for (int index = low + 1; index < high; ++index) {
                int current = Convert.ToInt32(list[index]);

                if (minimum > current) {
                    minimum = current;
                }
            }

            return minimum;
        }

        private int FindMaximum(IList<T> list, int low, int high) {
            int maximum = Convert.ToInt32(list[low]);

            for (int index = low + 1; index < high; ++index) {
                int current = Convert.ToInt32(list[index]);

                if (maximum < current) {
                    maximum = current;
                }
            }

            return maximum;
        }

        private void Move(IList<T> list, int low, T[] result) {
            int index = low;

            foreach (T val in result) {
                list[index] = val;
                ++index;
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as CountingSorter<T>);
        }

        public bool Equals(CountingSorter<T>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}
