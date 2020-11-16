using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public interface ICompareSorter<T> {
        public void Sort<R>(IList<R> list) where R : T, IComparable<T> {
            this.Sort(list, Comparer<T>.Default);
        }

        public void Sort<R>(IList<R> list, Comparison<T> comparison) where R : T {
            this.Sort(list, Comparer<T>.Create(comparison));
        }

        public void Sort<R>(IList<R> list, IComparer<T> comparer) where R : T {
            this.Sort(list, 0, list.Count, comparer);
        }

        public void Sort<R>(IList<R> list, int low, int high) where R : T, IComparable<T> {
            this.Sort(list, low, high, Comparer<T>.Default);
        }

        public void Sort<R>(IList<R> list, int low, int high, Comparison<T> comparison) where R : T {
            this.Sort(list, low, high, Comparer<T>.Create(comparison));
        }

        public void Sort<R>(IList<R> list, int low, int high, IComparer<T> comparer) where R : T;

        public async Task SortAsync<R>(IList<R> list) where R : T, IComparable<T> {
            await Task.Run(() => { this.Sort(list); });
        }

        public async Task SortAsync<R>(IList<R> list, Comparison<T> comparison) where R : T {
            await Task.Run(() => { this.Sort(list, comparison); });
        }

        public async Task SortAsync<R>(IList<R> list, IComparer<T> comparer) where R : T {
            await Task.Run(() => { this.Sort(list, comparer); });
        }

        public async Task SortAsync<R>(IList<R> list, int low, int high) where R : T, IComparable<T> {
            await Task.Run(() => { this.Sort(list, low, high); });
        }

        public async Task SortAsync<R>(IList<R> list, int low, int high, Comparison<T> comparison) where R : T {
            await Task.Run(() => { this.Sort(list, low, high, comparison); });
        }

        public async Task SortAsync<R>(IList<R> list, int low, int high, IComparer<T> comparer) where R : T {
            await Task.Run(() => { this.Sort(list, low, high, comparer); });
        }
    }
}
