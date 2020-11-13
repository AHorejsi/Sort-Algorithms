using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting {
    public interface ICompareSorter {
        public void Sort(IList list) {
            this.Sort(list, Comparer.Default);
        }

        public void Sort(IList list, Comparison<object?> comparison) {
            this.Sort(list, Comparer<object?>.Create(comparison));
        }

        public void Sort(IList list, IComparer comparer) {
            this.Sort(list, 0, list.Count, comparer);
        }

        public void Sort(IList list, int low, int high) {
            this.Sort(list, low, high, Comparer.Default);
        }

        public void Sort(IList list, int low, int high, Comparison<object?> comparison) {
            this.Sort(list, low, high, Comparer<object?>.Create(comparison));
        }

        public void Sort(IList list, int low, int high, IComparer comparer);
    }
}
