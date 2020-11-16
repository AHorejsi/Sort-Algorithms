using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Sorting.Immutable {
    public interface IImmutableIntegerSorter<T> {
        public IImmutableList<T> Sort(IImmutableList<T> list) {
            return this.Sort(list, 0, list.Count);
        }

        public IImmutableList<T> Sort(IImmutableList<T> list, int low, int high);
    }
}
