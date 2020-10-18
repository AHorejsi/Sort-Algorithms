using System;
using System.Collections;
using System.Dynamic;

namespace Sorting {
    internal class SortResultBuilder {
        private IList? list = null;
        private int? lowIndex = null;
        private int? highIndex = null;
        private IComparer? comparer = null;
        private dynamic? sorter = null;

        internal SortResultBuilder WithList(IList list) {
            this.list = list;

            return this;
        }

        public SortResultBuilder WithLowIndex(int lowIndex) {
            this.lowIndex = lowIndex;

            return this;
        }

        public SortResultBuilder WithHighIndex(int highIndex) {
            this.highIndex = highIndex;

            return this;
        }

        public SortResultBuilder WithComparer(IComparer comparer) {
            this.comparer = comparer;

            return this;
        }

        public SortResultBuilder WithCompareSorter(CompareSorter sorter) {
            this.sorter = sorter;

            return this;
        }

        public SortResultBuilder WithIntegerSorter(IntegerSorter sorter) {
            this.sorter = sorter;

            return this;
        }

        public SortResultBuilder WithFloatSorter(FloatSorter sorter) {
            this.sorter = sorter;

            return this;
        }

        public dynamic Build() {
            this.DoErrorCheck();

            dynamic data = new ExpandoObject();
            data.List = this.list!;
            data.Sorter = this.sorter!;

            if (this.lowIndex is null) {
                data.LowIndex = 0;
                data.HighIndex = list!.Count;
            }
            else {
                data.LowIndex = this.lowIndex!;
                data.HighIndex = this.highIndex!;
            }

            if (this.sorter is CompareSorter) {
                if (this.comparer is null) {
                    data.Compare = Comparer.Default;
                }
                else {
                    data.Compare = this.comparer;
                }
            }

            return data;
        }

        private void DoErrorCheck() {
            if (this.list is null) {
                throw new ArgumentNullException("list must not be null");
            }
            if (this.sorter is null) {
                throw new ArgumentNullException("sorter must not be null");
            }
            if (!(this.comparer is null || this.sorter is CompareSorter)) {
                throw new ArgumentException("If a comparer is to be defined, then the sorter must be of type CompareSorter");
            }
            if (this.lowIndex is null ^ this.highIndex is null) {
                throw new ArgumentException("If bounds are to be defined, both a lower bound and an upper bound must be defined. Define neither for sorting the entire list");
            }
        }
    }
}
