using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sorting.Generic {
    public interface ILinkedCompareSorter<T> {
        void Sort(LinkedList<T> list) {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!);
            }
        }

        void Sort(LinkedList<T> list, Comparison<T> comparison) {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, comparison);
            }
        }

        void Sort(LinkedList<T> list, IComparer<T> comparer) {
            if (list.Count > 1) {
                this.Sort(list.First!, list.Last!, comparer);
            }
        }

        void Sort(LinkedListNode<T> first, LinkedListNode<T> last) {
            this.Sort(first, last, Comparer<T>.Default);
        }

        void Sort(LinkedListNode<T> first, LinkedListNode<T> last, Comparison<T> comparison) {
            this.Sort(first, last, Comparer<T>.Create(comparison));
        }

        void Sort(LinkedListNode<T> first, LinkedListNode<T> last, IComparer<T> comparer);

        async Task SortAsync(LinkedList<T> list) {
            await Task.Run(() => { this.Sort(list); });
        }

        async Task SortAsync(LinkedList<T> list, Comparison<T> comparison) {
            await Task.Run(() => { this.Sort(list, comparison); });
        }

        async Task SortAsync(LinkedList<T> list, IComparer<T> comparer) {
            await Task.Run(() => { this.Sort(list, comparer); });
        }

        async Task SortAsync(LinkedListNode<T> first, LinkedListNode<T> last) {
            await Task.Run(() => { this.Sort(first, last); });
        }

        async Task SortAsync(LinkedListNode<T> first, LinkedListNode<T> last, Comparison<T> comparison) {
            await Task.Run(() => { this.Sort(first, last, comparison); });
        }

        async Task SortAsync(LinkedListNode<T> first, LinkedListNode<T> last, IComparer<T> comparer) {
            await Task.Run(() => { this.Sort(first, last, comparer); });
        }
    }
}
