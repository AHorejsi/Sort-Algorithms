using System.Collections.Generic;

namespace Sorting {
    public interface IIntegerSorter<T> {
        void Sort(IList<T> list, int low, int high);
    }
}
