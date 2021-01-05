using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class PigeonholeSorter<N> : IIntegerSorter<N>, IEquatable<PigeonholeSorter<N>> {
        public PigeonholeSorter() {
        }

        public void Sort(IList<N> list, int low, int high) {
            int min = this.FindMinimum(list, low, high);
            int max = this.FindMaximum(list, low, high);
            int range = max - min + 1;

            var holes = new List<N>[range];
            this.FillHolesWithLists(holes);

            for (int index = low; index < high; ++index) {
                N current = list[index];
                holes[Convert.ToInt32(current) - min].Add(current);
            }

            int i = low;
            for (var j = 0; j < range; ++j) {
                foreach (N val in holes[j]) {
                    list[i] = val;
                    ++i;
                }
            }
        }

        private int FindMinimum(IList<N> list, int low, int high) {
            int min = Convert.ToInt32(list[low]);

            for (int index = low + 1; index < high; ++index) {
                int current = Convert.ToInt32(list[index]);

                if (current < min) {
                    min = current;
                }
            }

            return min;
        }

        private int FindMaximum(IList<N> list, int low, int high) {
            int max = Convert.ToInt32(list[low]);

            for (int index = low + 1; index < high; ++index) {
                int current = Convert.ToInt32(list[index]);

                if (current > max) {
                    max = current;
                }
            }

            return max;
        }

        private void FillHolesWithLists(List<N>[] holes) {
            for (int index = 0; index < holes.Length; ++index) {
                holes[index] = new List<N>();
            }
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as PigeonholeSorter<N>);
        }

        public bool Equals(PigeonholeSorter<N>? sorter) {
            return !(sorter is null);
        }

        public override int GetHashCode() {
            Type type = base.GetType();

            return HashCode.Combine(type, type.Name);
        }
    }
}
