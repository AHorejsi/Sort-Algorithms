using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public interface ICompareSorter<T> {
        public void Sort<R>(IList<R> list) where R : T, IComparable<T> {
            this.Sort(list, Comparer<T>.Default);
        }

        void Sort<R>(IList<R> list, Comparison<T> comparison) where R : T {
            this.Sort(list, Comparer<T>.Create(comparison));
        }

        void Sort<R>(IList<R> list, IComparer<T> comparer) where R : T {
            this.Sort(list, 0, list.Count, comparer);
        }

        void Sort<R>(IList<R> list, int low, int high) where R : T, IComparable<T> {
            this.Sort(list, low, high, Comparer<T>.Default);
        }

        void Sort<R>(IList<R> list, int low, int high, Comparison<T> comparison) where R : T {
            this.Sort(list, low, high, Comparer<T>.Create(comparison));
        }

        void Sort<R>(IList<R> list, int low, int high, IComparer<T> comparer) where R : T;
    }
}
