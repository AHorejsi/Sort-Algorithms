﻿using System.Collections;

namespace Sorting {
    public class ShellSorter : ComparisonSorter {
        public override void Sort(IList list, int low, int high, IComparer comparer) {
            int size = high - low;

            for (int gap = size / 2; gap > 0; gap /= 2) {
                for (int i = gap; i < size; ++i) {
                    object temp = list[i];
                    int j;

                    for (j = i; j >= gap && comparer.Compare(temp, list[j - gap]) < 0; j -= gap) {
                        list[j] = list[j - gap];
                    }

                    list[j] = temp;
                }
            }
        }
    }
}
