using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public interface ICompareSorter<T> {
        void Sort(IList<T> list) {
            this.Sort(list, Comparer<T>.Default);
        }

        void Sort(IList<T> list, Comparison<T> comparison) {
            this.Sort(list, Comparer<T>.Create(comparison));
        }

        void Sort(IList<T> list, IComparer<T> comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        void Sort(IList<T> list, int low, int high) {
            this.Sort(list, low, high, Comparer<T>.Default);
        }

        void Sort(IList<T> list, int low, int high, Comparison<T> comparison) {
            this.Sort(list, low, high, Comparer<T>.Create(comparison));
        }

        void Sort(IList<T> list, int low, int high, IComparer<T> comparer);

        async Task SortAsync(IList<T> list) {
            await Task.Run(() => { this.Sort(list); });
        }

        async Task SortAsync(IList<T> list, Comparison<T> comparison) {
            await Task.Run(() => { this.Sort(list, comparison); });
        }

        async Task SortAsync(IList<T> list, IComparer<T> comparer) {
            await Task.Run(() => { this.Sort(list, comparer); });
        }

        async Task SortAsync(IList<T> list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }

        async Task SortAsync(IList<T> list, int low, int high, Comparison<T> comparison) {
            await Task.Run(() => { this.Sort(list, low, high, comparison); });
        }

        async Task SortAsync(IList<T> list, int low, int high, IComparer<T> comparer) {
            await Task.Run(() => { this.Sort(list, low, high, comparer); });
        }
    }
}
