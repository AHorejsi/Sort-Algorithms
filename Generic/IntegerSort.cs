using System;
using System.Collections.Generic;

namespace Sorting.Generic {
    public abstract class IntegerSorter<T> {
        protected IntegerSorter() {
            if (!SortUtils.IsIntegerType<T>()) {
                throw new InvalidOperationException("Generic type must be an integer type");
            }
        }

        public void Sort(IList<T> list) {
            this.Sort(list, 0, list.Count);
        }

        public abstract void Sort(IList<T> list, int low, int high);
    }
}
