using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class CountingSorter<N> : IIntegerSorter<N>, IEquatable<CountingSorter<N>> {
        public CountingSorter() {
        }

        public void Sort(IList<N> list, int low, int high) {
            int minimum = this.FindMinimum(list, low, high);
            int maximum = this.FindMaximum(list, low, high);
            int range = maximum - minimum + 1;

            int[] counts = new int[range];
            N[] result = new N[high - low];

            for (int index = low; index < high; ++index) {
                ++(counts[Convert.ToInt32(list[index]) - minimum]);
            }

            for (int index = 1; index < range; ++index) {
                counts[index] += counts[index - 1];
            }

            for (int index = high - 1; index >= low; --index) {
                int indexOfCounts = Convert.ToInt32(list[index]) - minimum;

                result[counts[indexOfCounts] - 1] = list[index]!;
                --(counts[indexOfCounts]);
            }

            this.Move(list, low, result);
        }

        private int FindMinimum(IList<N> list, int low, int high) {
            int minimum = Convert.ToInt32(list[low]);

            for (int index = low + 1; index < high; ++index) {
                int current = Convert.ToInt32(list[index]);

                if (minimum > current) {
                    minimum = current;
                }
            }

            return minimum;
        }

        private int FindMaximum(IList<N> list, int low, int high) {
            int maximum = Convert.ToInt32(list[low]);

            for (int index = low + 1; index < high; ++index) {
                int current = Convert.ToInt32(list[index]);

                if (maximum < current) {
                    maximum = current;
                }
            }

            return maximum;
        }

        private void Move(IList<N> list, int low, N[] result) {
            int index = low;

            foreach (N val in result) {
                list[index] = val;
                ++index;
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as CountingSorter<N>);
        }

        public bool Equals(CountingSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return type.GetHashCode() + type.Name.GetHashCode();
        }
    }
}