using System;
using System.Collections.Generic;

namespace Sorting.Linked {
    public class LinkedCountingSorter<T> : LinkedIntegerSorter<T> {
        private static LinkedCountingSorter<T>? SINGLETON = null;

        private LinkedCountingSorter() {
        }

        public static LinkedCountingSorter<T> Singleton {
            get {
                if (SortUtils.IsIntegerType<T>()) {
                    if (LinkedCountingSorter<T>.SINGLETON is null) {
                        LinkedCountingSorter<T>.SINGLETON = new LinkedCountingSorter<T>();
                    }

                    return LinkedCountingSorter<T>.SINGLETON;
                }
                else {
                    throw new InvalidOperationException("Generic type must be an integer type");
                }
            }
        }

        public override void Sort(LinkedListNode<T> first, LinkedListNode<T> last) {
            int minimum = this.FindMinimum(first);
            int maximum = this.FindMaximum(first);
            int range = maximum - minimum + 1;
            int size = this.Distance(first);

            int[] counts = new int[range];
            T[] result = new T[size];

            for (LinkedListNode<T>? node = first; node != last.Next; node = node.Next) {
                ++(counts[Convert.ToInt32(node!.Value) - minimum]);
            }

            for (int index = 1; index < range; ++index) {
                counts[index] += counts[index - 1];
            }

            for (LinkedListNode<T> node = last; node != first.Previous; node = node.Previous!) {
                result[counts[Convert.ToInt32(node.Value) - minimum] - 1] = node.Value;
                --(counts[Convert.ToInt32(node.Value) - minimum]);
            }

            this.Move(first, result);
        }

        private int FindMinimum(LinkedListNode<T> first) {
            int minimum = Convert.ToInt32(first.Value);

            for (LinkedListNode<T>? node = first.Next; !(node is null); node = node.Next) {
                int current = Convert.ToInt32(node.Value);

                if (current < minimum) {
                    minimum = current;
                }
            }

            return minimum;
        }

        private int FindMaximum(LinkedListNode<T> first) {
            int maximum = Convert.ToInt32(first.Value);

            for (LinkedListNode<T>? node = first.Next; !(node is null); node = node.Next) {
                int current = Convert.ToInt32(node.Value);

                if (current > maximum) {
                    maximum = current;
                }
            }

            return maximum;
        }

        private int Distance(LinkedListNode<T> first) {
            int size = 0;

            for (LinkedListNode<T> node = first; !(node is null); node = node.Next) {
                ++size;
            }

            return size;
        }

        private void Move(LinkedListNode<T> node, T[] result) {
            foreach (T val in result) {
                node.Value = val;
                node = node.Next!;
            }
        }
    }
}
