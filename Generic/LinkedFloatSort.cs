using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public interface ILinkedFloatSorter<T> {
        public void Sort(LinkedList<T> list) {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!);
            }
        }

        public void Sort(LinkedListNode<T> first, LinkedListNode<T> last);

        public async Task SortAsync(LinkedList<T> list) {
            await Task.Run(() => { this.Sort(list); });
        }

        public async Task SortAsync(LinkedListNode<T> first, LinkedListNode<T> last) {
            await Task.Run(() => { this.Sort(first, last); });
        }
    }
}
