using System;
using System.Collections;
using System.Threading.Tasks;

namespace Sorting {
    public static class ExtensionMethods {
        public static void Sort(this IList list, ICompareSorter sorter) {
            sorter.Sort(list);
        }

        public static void Sort(this IList list, ICompareSorter sorter, IComparer comparer) {
            sorter.Sort(list, comparer);
        }

        public static void Sort(this IList list, ICompareSorter sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList list, ICompareSorter sorter, int low, int high, IComparer comparer) {
            sorter.Sort(list, low, high, comparer);
        }

        public static void Sort(this IList list, IIntegerSorter sorter) {
            sorter.Sort(list);
        }

        public static void Sort(this IList list, IIntegerSorter sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static void Sort(this IList list, FloatSorter sorter) {
            sorter.Sort(list);
        }

        public static void Sort(this IList list, FloatSorter sorter, int low, int high) {
            sorter.Sort(list, low, high);
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter) {
            await Task.Run(() => { list.Sort(sorter); });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithCompareSorter(sorter)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, IComparer comparer) {
            await Task.Run(() => { list.Sort(sorter, comparer); });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, IComparer comparer, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter, comparer);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, IComparer comparer, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter, comparer);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithCompareSorter(sorter)
                    .WithComparer(comparer)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high) {
            await Task.Run(() => { list.Sort(sorter, low, high); });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithCompareSorter(sorter)
                    .WithLowIndex(low)
                    .WithHighIndex(high)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high, IComparer comparer) {
            await Task.Run(() => { list.Sort(sorter, low, high, comparer); });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high, IComparer comparer, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high, comparer);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, ICompareSorter sorter, int low, int high, IComparer comparer, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high, comparer);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithCompareSorter(sorter)
                    .WithLowIndex(low)
                    .WithHighIndex(high)
                    .WithComparer(comparer)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter) {
            await Task.Run(() => { list.Sort(sorter); });
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithIntegerSorter(sorter)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter, int low, int high) {
            await Task.Run(() => { list.Sort(sorter, low, high); });
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter, int low, int high, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, IIntegerSorter sorter, int low, int high, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithIntegerSorter(sorter)
                    .WithLowIndex(low)
                    .WithHighIndex(high)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, FloatSorter sorter) {
            await Task.Run(() => { list.Sort(sorter); });
        }

        public static async Task SortAsync(this IList list, FloatSorter sorter, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, FloatSorter sorter, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithFloatSorter(sorter)
                    .Build();
                callback(result);
            });
        }

        public static async Task SortAsync(this IList list, FloatSorter sorter, int low, int high) {
            await Task.Run(() => { list.Sort(sorter, low, high); });
        }

        public static async Task SortAsync(this IList list, FloatSorter sorter, int low, int high, Action callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high);
                callback();
            });
        }

        public static async Task SortAsync(this IList list, FloatSorter sorter, int low, int high, Action<dynamic> callback) {
            await Task.Run(() => {
                list.Sort(sorter, low, high);

                dynamic result = new SortResultBuilder()
                    .WithList(list)
                    .WithFloatSorter(sorter)
                    .WithLowIndex(low)
                    .WithHighIndex(high)
                    .Build();
                callback(result);
            });
        }
    }
}
