using System.Collections.Generic;

namespace Sorting.Generic {
    public interface ILinkedIntegerSorter<T> {
        public void Sort(LinkedList<T> list) {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!);
            }
        }

        public void Sort(LinkedListNode<T> first, LinkedListNode<T> last);
    }
}
