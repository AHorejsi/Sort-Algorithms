using System.Collections;

namespace Sorting {
    /// <summary>
    ///     Represents an algorithm that uses comparison as the basis for sorting
    /// </summary>
    public abstract class CompareSorter {
        /// <summary>
        ///     Sorts the given list using the <c>IComparable</c> interface
        /// </summary>
        /// <param name="list">
        ///     The list to be sorted
        /// </param>
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
