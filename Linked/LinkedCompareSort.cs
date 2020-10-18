using System;
using System.Collections.Generic;

namespace Sorting.Linked {
    public abstract class LinkedCompareSorter<T> {
        public void Sort<R>(LinkedList<R> list) where R : T, IComparable<R> {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, Comparer<R>.Default);
            }
        }

        public void Sort<R>(LinkedList<R> list, Comparison<R> comparison) where R : T {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, Comparer<R>.Create(comparison));
            }
        }

        public void Sort<R>(LinkedList<R> list, IComparer<R> comparer) where R : T {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, comparer);
            }
        }

        public void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last) where R : T, IComparable<R> {
            this.Sort(first, last, Comparer<R>.Default);
        }

        public void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last, Comparison<R> comparison) where R : T {
            this.Sort(first, last, Comparer<R>.Create(comparison));
        }

        public abstract void Sort<R>(LinkedListNode<R> first, LinkedListNode<R> last, IComparer<R> comparer) where R : T;
    }
}
