using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public interface ICompareSorter {
        void Sort(IList list) {
            this.Sort(list, Comparer.Default);
        }

        void Sort(IList list, Comparison<object?> comparison) {
            this.Sort(list, Comparer<object?>.Create(comparison));
        }

        void Sort(IList list, IComparer comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        void Sort(IList list, int low, int high) {
            this.Sort(list, low, high, Comparer.Default);
        }

        void Sort(IList list, int low, int high, Comparison<object?> comparison) {
            this.Sort(list, low, high, Comparer<object?>.Create(comparison));
        }

        void Sort(IList list, int low, int high, IComparer comparer);

        async Task SortAsync(IList list) {
            await Task.Run(() => { this.Sort(list); });
        }

        async Task SortAsync(IList list, Comparison<object?> comparison) {
            await Task.Run(() => { this.Sort(list, comparison); });
        }

        async Task SortAsync(IList list, IComparer comparer) {
            await Task.Run(() => { this.Sort(list, comparer); });
        }

        async Task SortAsync(IList list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }

        async Task SortAsync(IList list, int low, int high, Comparison<object?> comparison) {
            await Task.Run(() => { this.Sort(list, low, high, comparison); });
        }

        async Task SortAsync(IList list, int low, int high, IComparer comparer) {
            await Task.Run(() => { this.Sort(list, low, high, comparer); });
        }
    }
}
