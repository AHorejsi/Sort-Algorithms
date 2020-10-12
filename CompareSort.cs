using System.Collections;

namespace Sorting {
    public abstract class CompareSorter {
        public void Sort(IList list) {
            this.Sort(list, Comparer.Default);
        }

        public void Sort(IList list, IComparer comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        public void Sort(IList list, int low, int high) {
            this.Sort(list, low, high, Comparer.Default);
        }

        public abstract void Sort(IList list, int low, int high, IComparer comparer);
    }
}
