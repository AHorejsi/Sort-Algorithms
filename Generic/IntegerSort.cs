using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public interface IIntegerSorter<T> {
        public void Sort(IList<T> list) {
            this.Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int low, int high);

        public async Task SortAsync(IList<T> list) {
            await Task.Run(() => { this.Sort(list); });
        }

        public async Task SortAsync(IList<T> list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }
    }
}
