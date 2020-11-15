using System;
using System.Collections.Generic;
using  System.Collections.Immutable;

namespace Sorting.Immutable {
    public interface IImmutableCompareSort<T> {
        public IImmutableList<R> Sort<R>(IImmutableList<R> list) where R : T, IComparable<T>;

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, Comparison<T> comparison) where R : T;

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, IComparer<T> comparer) where R : T;

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, int low, int high) where R : T, IComparable<T>;

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, int low, int high, Comparison<T> comparison) where R : T;

        public IImmutableList<R> Sort<R>(IImmutableList<R> list, int low, int high, IComparer<T> comparer) where R : T;
    }
}
