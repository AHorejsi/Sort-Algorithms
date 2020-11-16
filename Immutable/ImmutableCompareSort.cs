using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Sorting.Immutable {
    public interface IImmutableCompareSorter<T> {
        public IImmutableList<R> Sort<R>(IImmutableList<R> list) where R : T, IComparable<T> {
            return this.Sort(list, Comparer<T>.Default);
        }

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, Comparison<T> comparison) where R : T {
            return this.Sort(list, Comparer<T>.Create(comparison));
        }

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, IComparer<T> comparer) where R : T {
            return this.Sort(list, 0, list.Count, comparer);
        }

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, int low, int high) where R : T, IComparable<T> {
            return this.Sort(list, low, high, Comparer<T>.Default);
        }

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, int low, int high, Comparison<T> comparison) where R : T {
            return this.Sort(list, low, high, Comparer<T>.Create(comparison));
        }

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, int low, int high, IComparer<T> comparer) where R : T;
    }
}
