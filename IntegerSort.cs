using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public interface IIntegerSorter {
        public void Sort(IList list) {
            this.Sort(list, 0, list.Count);
        }

        public void Sort(IList list, int low, int high);

        public async Task SortAsync(IList list) {
            await Task.Run(() => { this.Sort(list); });
        }

        public async Task SortAsync(IList list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }
    }
}
