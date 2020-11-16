using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Sorting.Immutable {
    public interface IImmutableIntegerSorter<T> {
        public IImmutableList<T> Sort(IImmutableList<T> list) {
            return this.Sort(list, 0, list.Count);
        }

        public IImmutableList<T> Sort(IImmutableList<T> list, int low, int high);

        public async Task<IImmutableList<T>> SortAsync(IImmutableList<T> list) {
            Task<IImmutableList<T>> task = Task.Run(() => this.Sort(list));
            await task;

            return task.Result;
        }

        public async Task<IImmutableList<T>> SortAsync(IImmutableList<T> list, int low, int high) {
            Task<IImmutableList<T>> task = Task.Run(() => this.Sort(list, low, high));
            await task;

            return task.Result;
        }
    }
}
