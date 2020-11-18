using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public interface IIntegerSorter<T> {
        void Sort(IList<T> list) {
            this.Sort(list, 0, list.Count);
        }

        void Sort(IList<T> list, int low, int high);

        async Task SortAsync(IList<T> list) {
            await Task.Run(() => { this.Sort(list); });
        }

        async Task SortAsync(IList<T> list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }
    }
}
