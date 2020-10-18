using System;
using System.Collections.Generic;

namespace Sorting.Linked {
    public class LinkedBubbleSorter<T> : LinkedCompareSorter<T> {
        private static LinkedBubbleSorter<T>? SINGLETON = null;

        private LinkedBubbleSorter() {
        }

        public static LinkedBubbleSorter<T> Singleton {
            get {
                if (LinkedBubbleSorter<T>.SINGLETON is null) {
                    LinkedBubbleSorter<T>.SINGLETON = new LinkedBubbleSorter<T>();
                }

                return LinkedBubbleSorter<T>.SINGLETON;
            }
        }

        public override void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last, IComparer<R> comparer) {
            for (LinkedListNode<R>? i = first.Next; !(i is null); i = i.Next) {
                bool swapped = false;
                LinkedListNode<R>? j = first;
                LinkedListNode<R>? k = j.Next;

                while (!(k is null)) {
                    if (comparer.Compare(j!.Value, k.Value) > 0) {
                        SortUtils.Swap(j, k);
                        swapped = true;
                    }

                    j = j.Next;
                    k = k.Next;
                }

                if (!swapped) {
                    return;
                }
            }
        }
    }
}
