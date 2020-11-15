using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public interface ILinkedCompareSorter<T> {
        public void Sort<R>(LinkedList<R> list) where R : T, IComparable<R> {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!);
            }
        }

        public void Sort<R>(LinkedList<R> list, Comparison<T> comparison) where R : T {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, comparison);
            }
        }

        public void Sort<R>(LinkedList<R> list, IComparer<T> comparer) where R : T {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, comparer);
            }
        }

        public void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last) where R : T {
            this.Sort(first, last, Comparer<T>.Default);
        }

        public void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last, Comparison<T> comparison) where R : T {
            this.Sort(first, last, Comparer<T>.Create(comparison));
        }

        public void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last, IComparer<T> comparer) where R : T;
    }
}
