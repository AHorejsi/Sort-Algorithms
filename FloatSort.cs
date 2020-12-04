﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting {
    public interface IFloatSorter<T> {
        void Sort(IList<T> list, int low, int high);

        async Task SortAsync(IList<T> list, int low, int high) {
            await Task.Run(() => { this.Sort(list, low, high); });
        }
    }
}
