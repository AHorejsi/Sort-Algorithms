using System.Collections.Generic;

namespace Sorting.Generic {
    public abstract class IntegerSorter<T> {
        public void Sort(IList<T> list) {
            this.Sort(list, 0, list.Count);
        }

        public abstract void Sort(IList<T> list, int low, int high);
    }
}
