using System.Collections.Generic;

namespace Sorting.Linked {
    public abstract class LinkedIntegerSorter<T> {
        public void Sort(LinkedList<T> list) {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!);
            }
        }

        public abstract void Sort(LinkedListNode<T> first, LinkedListNode<T> last);
    }
}
