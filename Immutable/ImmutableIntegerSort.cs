using System.Collections.Immutable;

namespace Sorting.Immutable {
    public interface IImmutableIntegerSorter<T> {
        IImmutableList<T> Sort(IImmutableList<T> list) {
            return this.Sort(list, 0, list.Count);
        }

        IImmutableList<T> Sort(IImmutableList<T> list, int low, int high);
    }
}
