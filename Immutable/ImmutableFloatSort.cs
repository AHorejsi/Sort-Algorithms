using System.Collections.Immutable;

namespace Sorting.Immutable {
    public interface IImmutableFloatSorter<T> {
        public IImmutableList<T> Sort(IImmutableList<T> list) {
            return this.Sort(list, 0, list.Count);
        }

        public IImmutableList<T> Sort(IImmutableList<T> list, int low, int high);
    }
}
