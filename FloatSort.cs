using System.Collections;

namespace Sorting {
    public abstract class FloatSorter {
        public void Sort(IList list) {
            this.Sort(list, 0, list.Count);
        }

        public abstract void Sort(IList list, int low, int high);
    }
}
