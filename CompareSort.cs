using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public interface ICompareSorter {
        public void Sort(IList list) {
            this.Sort(list, Comparer.Default);
        }

        public void Sort(IList list, Comparison<object?> comparison) {
            this.Sort(list, Comparer<object?>.Create(comparison));
        }

        public void Sort(IList list, IComparer comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        public void Sort(IList list, int low, int high) {
            this.Sort(list, low, high, Comparer.Default);
        }

        public void Sort(IList list, int low, int high, Comparison<object?> comparison) {
            this.Sort(list, low, high, Comparer<object?>.Create(comparison));
        }

        public void Sort(IList list, int low, int high, IComparer comparer);

        public async Task SortAsync(IList list) {
            await Task.Run(() => { this.Sort(list); });
        }

        public async Task SortAsync(IList list, Comparison<object?> comparison) {
            await Task.Run(() => { this.Sort(list, comparison); });
        }

        public async Task SortAsync(IList list, IComparer comparer) {
            await Task.Run(() => { this.Sort(list, comparer); });
        }

        public async Task SortAsync(IList list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }

        public async Task SortAsync(IList list, int low, int high, Comparison<object?> comparison) {
            await Task.Run(() => { this.Sort(list, low, high, comparison); });
        }

        public async Task SortAsync(IList list, int low, int high, IComparer comparer) {
            await Task.Run(() => { this.Sort(list, low, high, comparer); });
        }
    }
}
