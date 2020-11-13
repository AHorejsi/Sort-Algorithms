using System.Collections;

namespace Sorting {
    public interface IFloatSorter {
        public void Sort(IList list) {
            this.Sort(list, 0, list.Count);
        }

        public void Sort(IList list, int low, int high);
    }
}
