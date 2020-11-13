using System.Collections;

namespace Sorting {
    public interface IIntegerSorter {
        public void Sort(IList list) {
            this.Sort(list, 0, list.Count);
        }

        public void Sort(IList list, int low, int high);
    }
}
