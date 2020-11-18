using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public interface IFloatSorter {
        void Sort(IList list) {
            this.Sort(list, 0, list.Count);
        }

        void Sort(IList list, int low, int high);

        async Task SortAsync(IList list) {
            await Task.Run(() => { this.Sort(list); });
        }

        async Task SortAsync(IList list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }
    }
}
