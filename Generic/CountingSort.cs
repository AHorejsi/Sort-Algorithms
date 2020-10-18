using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public class CountingSorter<T> : IntegerSorter<T>, IEquatable<CountingSorter<T>> {
        private static CountingSorter<T>? SINGLETON = null;

        private CountingSorter() {
        }

        public static CountingSorter<T> Singleton {
            get {
                if (SortUtils.IsIntegerType<T>()) {
                    if (CountingSorter<T>.SINGLETON is null) {
                        CountingSorter<T>.SINGLETON = new CountingSorter<T>();
                    }

                    return CountingSorter<T>.SINGLETON;
                }
                else {
                    throw new InvalidOperationException("Generic type must be an integer type");
                }
            }
        }

        public override void Sort(IList<T> list, int low, int high) {
            int minimum = this.FindMinimum(list, low, high);
            int maximum = this.FindMaximum(list, low, high);
            int range = maximum - minimum + 1;

            List<int> counts = new List<int>(range);
            List<T> result = new List<T>();
            this.FillWithZeroes(counts, result);

            for (int index = low; index < high; ++index) {
                ++(counts[Convert.ToInt32(list[index]) - minimum]);
            }

            for (int index = 1; index < range; ++index) {
                counts[index] += counts[index - 1];
            }

            for (int index = high - 1; index >= low; --index) {
                result[counts[Convert.ToInt32(list[index]) - minimum] - 1] = list[index];
                --(counts[Convert.ToInt32(list[index]) - minimum]);
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

        private void FillWithZeroes(List<int> counts, List<T> result) {
            for (int index = 0; index < counts.Capacity; ++index) {
                counts.Add(0);
            }

            for (int index = 0; index < result.Capacity; ++index) {
                result.Add(default!);
            }
        }

        private void Move(IList<T> list, int low, List<T> result) {
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
