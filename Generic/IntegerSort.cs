using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public interface IIntegerSorter<T> {
        void Sort(IList<T> list) {
            this.Sort(list, 0, list.Count);
        }

        void Sort(IList<T> list, int low, int high);
    }
}
