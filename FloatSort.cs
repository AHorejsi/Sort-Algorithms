using System.Collections.Generic;

namespace Sorting {
    public interface IFloatSorter<T> {
        void Sort(IList<T> list, int low, int high);
    }
}
