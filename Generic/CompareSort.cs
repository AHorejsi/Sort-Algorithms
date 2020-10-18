using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public abstract class CompareSorter<T> {
        public void Sort<R>(IList<R> list) where R : T, IComparable<R> {
            this.Sort(list, Comparer<R>.Default);
        }

        public void Sort<R>(IList<R> list, Comparison<R> comparison) where R : T {
            this.Sort(list, 0, list.Count, Comparer<R>.Create(comparison));
        }

        public void Sort<R>(IList<R> list, IComparer<R> comparer) where R : T {
            this.Sort(list, 0, list.Count, comparer);
        }

        public void Sort<R>(IList<R> list, int low, int high) where R : T, IComparable<R> {
            this.Sort(list, low, high, Comparer<R>.Default);
        }

        public void Sort<R>(IList<R> list, int low, int high, Comparison<R> comparison) where R : T {
            this.Sort(list, low, high, Comparer<R>.Create(comparison));
        }

        public abstract void Sort<R>(IList<R> list, int low, int high, IComparer<R> comparer) where R : T;
    }
}
