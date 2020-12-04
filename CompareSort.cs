using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public interface ICompareSorter<T> {
        void Sort(IList<T> list, int low, int high, IComparer<T> comparer);

        async Task SortAsync(IList<T> list, int low, int high, IComparer<T> comparer) {
            await Task.Run(() => { this.Sort(list, low, high, comparer); });
        }
    }
}
