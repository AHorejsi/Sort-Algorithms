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
            throw new NotImplementedException();
        }
    }
}
