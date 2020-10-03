using System.Collections;

namespace Sorting {
    public abstract class IntegerSorter {
        public void Sort(IList list) {
            this.Sort(list, 0, list.Count);
        }

        public abstract void Sort(IList list, int low, int high);

        public abstract override bool Equals(object obj);

        public static bool operator ==(IntegerSorter sorter1, IntegerSorter sorter2) {
            return sorter1.Equals(sorter2);
        }

        public static bool operator !=(IntegerSorter sorter1, IntegerSorter sorter2) {
            return !(sorter1 == sorter2);
        }

        //public abstract override int GetHashCode();
    }
}
