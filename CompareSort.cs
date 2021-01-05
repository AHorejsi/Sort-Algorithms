using System.Collections.Generic;

namespace Sorting {
    public interface ICompareSorter<T> {
        void Sort(IList<T> list, int low, int high, IComparer<T> comparer);
    }
}
