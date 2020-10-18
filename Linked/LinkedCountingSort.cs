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
    }
}
