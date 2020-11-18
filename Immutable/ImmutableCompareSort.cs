using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Sorting.Immutable {
    public interface IImmutableCompareSorter<T> {
        IImmutableList<T> Sort(IImmutableList<T> list) {
            return this.Sort(list, Comparer<T>.Default);
        }

        IImmutableList<T> Sort(IImmutableList<T> list, Comparison<T> comparison) {
            return this.Sort(list, Comparer<T>.Create(comparison));
        }

        IImmutableList<T> Sort(IImmutableList<T> list, IComparer<T> comparer) {
            return this.Sort(list, 0, list.Count, comparer);
        }

        IImmutableList<T> Sort(IImmutableList<T> list, int low, int high) {
            return this.Sort(list, low, high, Comparer<T>.Default);
        }

        IImmutableList<T> Sort(IImmutableList<T> list, int low, int high, Comparison<T> comparison) {
            return this.Sort(list, low, high, Comparer<T>.Create(comparison));
        }

        IImmutableList<T> Sort(IImmutableList<T> list, int low, int high, IComparer<T> comparer);
    }
}
