using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sorting {
    internal static class SortUtils {
        public static void Swap<T>(IList<T> list, int i, int j) {
            T temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        public static void CheckRange(int low, int high) {
            if (high <= low) {
                throw new InvalidOperationException("Starting index cannot be higher than ending index");
            }
        }

        public static void CheckLists<T>(LinkedListNode<T> first, LinkedListNode<T> last) {
            if (!object.ReferenceEquals(first.List, last.List)) {
                throw new InvalidOperationException("The nodes are not part of the same linked list");
            }
        }

        public static void CheckList<T>(LinkedList<T> list, LinkedListNode<T> first, LinkedListNode<T> last) {
            if (!(object.ReferenceEquals(list, first.List) || object.ReferenceEquals(list, last.List))) {
                throw new InvalidOperationException("Both nodes must be part of the list");
            }
        }

        public static bool IsSorted<T>(IList<T> list, int low, int high, IComparer<T> comparer) {
            for (int index = low; index < high - 1; ++index) {
                if (comparer.Compare(list[index], list[index + 1]) > 0) {
                    return false;
                }
            }

            return true;
        }

        public static bool IsSorted<T>(LinkedListNode<T> first, LinkedListNode<T> last, IComparer<T> comparer) {
            for (LinkedListNode<T> node = first; node != last.Previous; node = node.Next!) {
                if (comparer.Compare(node.Value, node.Next!.Value) > 0) {
                    return false;
                }
            }

            return true;
        }
    }
}
