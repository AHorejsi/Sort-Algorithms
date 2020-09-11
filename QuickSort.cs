﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting {
    public delegate int PivotSelector(IList list, int low, int high, IComparer comparer);
    public delegate dynamic PartitionScheme(IList list, int low, int high, IComparer comparer, PivotSelector pivotSelector);
    public delegate int PartitionPointDistance(int partitionPoint);
    public delegate void QuickSortAlgorithm(IList list, int low, int high, IComparer comparer, PartitionDetails partitionDetails);
    
    public class PartitionDetails {
        public PivotSelector PivotSelector { get; }
        public PartitionScheme PartitionScheme { get; }
        public PartitionPointDistance Left { get; }
        public PartitionPointDistance Right { get; }

        internal PartitionDetails(
            PivotSelector pivotSelector,
            PartitionScheme partitionScheme,
            PartitionPointDistance left,
            PartitionPointDistance right
        ) {
            this.PivotSelector = pivotSelector;
            this.PartitionScheme = partitionScheme;
            this.Left = left;
            this.Right = right;
        }
    }

    public class QuickSorter : ComparisonSorter {
        public PartitionDetails PartitionDetails { get; }
        public QuickSortAlgorithm Algorithm { get; }

        internal QuickSorter(PartitionDetails partitionDetails, QuickSortAlgorithm algorithm) {
            this.PartitionDetails = partitionDetails;
            this.Algorithm = algorithm;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            this.Algorithm(list, low, high, comparer, this.PartitionDetails);
        }
    }

    public class QuickSorterBuilder {
        private PivotSelector pivotSelector = PivotSelectors.MedianOfThree;
        private PartitionScheme partitionScheme = PartitionSchemes.Hoare;
        private PartitionPointDistance left = PartitionPointDistances.At;
        private PartitionPointDistance right = PartitionPointDistances.RightOne;
        private QuickSortAlgorithm algorithm = QuickSortAlgorithms.Recursive;
        private bool concurrent = false;

        public QuickSorterBuilder() {
        }

        public QuickSorterBuilder WithPivotSelector(PivotSelector pivotSelector) {
            this.pivotSelector = pivotSelector;

            return this;
        }

        public QuickSorterBuilder WithPartitionScheme(PartitionScheme partitionScheme) {
            this.partitionScheme = partitionScheme;

            return this;
        }

        public QuickSorterBuilder WithLeft(PartitionPointDistance left) {
            this.left = left;

            return this;
        }

        public QuickSorterBuilder WithRight(PartitionPointDistance right) {
            this.right = right;

            return this;
        }

        public QuickSorterBuilder WithAlgorithm(QuickSortAlgorithm algorithm) {
            this.algorithm = algorithm;

            return this;
        }

        public QuickSorterBuilder WithConcurrency(bool concurrent) {
            this.concurrent = concurrent;

            return this;
        }

        public QuickSorter Build() {
            PartitionDetails partitionDetails = new PartitionDetails(
                this.pivotSelector,
                this.partitionScheme,
                this.left,
                this.right
            );

            return new QuickSorter(partitionDetails, this.algorithm);
        }
    }

    public static class PivotSelectors {
        public static int First(IList list, int low, int high, IComparer comparer) {
            return low;
        }

        public static int Middle(IList list, int low, int high, IComparer comparer) {
            return low + (high + 1 - low) / 2;
        }

        public static int Last(IList list, int low, int high, IComparer comparer) {
            return high;
        }

        public static int Random(IList list, int low, int high, IComparer comparer) {
            return RngHolder.GetRng().Next(low, high + 1);
        }

        public static int MedianOfThree(IList list, int low, int high, IComparer comparer) {
            int mid = low + (high + 1 - low) / 2;

            if (comparer.Compare(list[high], list[low]) < 0) {
                SortingUtils.Swap(list, high, low);
            }
            if (comparer.Compare(list[mid], list[low]) < 0) {
                SortingUtils.Swap(list, mid, low);
            }
            if (comparer.Compare(list[high], list[mid]) < 0) {
                SortingUtils.Swap(list, high, mid);
            }

            return mid;
        }
    }

    public static class PartitionSchemes {
        public static dynamic Lomuto(IList list, int low, int high, IComparer comparer, PivotSelector pivotSelector) {
            int pivotIndex = pivotSelector(list, low, high, comparer);
            SortingUtils.Swap(list, pivotIndex, high);

            object pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; ++j) {
                if (comparer.Compare(list[j], pivot) <= 0) {
                    ++i;
                    SortingUtils.Swap(list, i, j);
                }
            }

            int swapIndex = i + 1;
            SortingUtils.Swap(list, swapIndex, high);

            return swapIndex;
        }

        public static dynamic Hoare(IList list, int low, int high, IComparer comparer, PivotSelector pivotSelector) {
            int pivotIndex = pivotSelector(list, low, high, comparer);
            SortingUtils.Swap(list, pivotIndex, low);

            object pivot = list[low];
            int i = low - 1;
            int j = high + 1;

            while (true) {
                do {
                    ++i;
                } while (comparer.Compare(list[i], pivot) < 0);

                do {
                    --j;
                } while (comparer.Compare(list[j], pivot) > 0);

                if (i >= j) {
                    return j;
                }

                SortingUtils.Swap(list, i, j);
            }
        }

        public static dynamic Stable(IList list, int low, int high, IComparer comparer, PivotSelector pivotSelector) {
            int mid = low + (high + 1 - low) / 2;

            int pivotIndex = pivotSelector(list, low, high, comparer);
            SortingUtils.Swap(list, pivotIndex, mid);

            ArrayList lesser = new ArrayList(list.Count);
            ArrayList greater = new ArrayList(list.Count);
            object pivot = list[mid];

            for (int index = low; index <= high; ++index) {
                if (index != mid) {
                    int comparison = comparer.Compare(list[index], pivot);

                    if (comparison < 0) {
                        lesser.Add(list[index]);
                    }
                    else if (comparison > 0) {
                        greater.Add(list[index]);
                    }
                    else {
                        if (index < mid) {
                            lesser.Add(list[index]);
                        }
                        else {
                            greater.Add(list[index]);
                        }
                    }
                }
            }
            
            int i = low;

            foreach (object val in lesser) {
                list[i] = val;
                ++i;
            }

            int partitionPoint = i;
            list[i] = pivot;
            ++i;

            foreach (object val in greater) {
                list[i] = val;
                ++i;
            }

            return partitionPoint;
        }

        public static dynamic ThreeWay(IList list, int low, int high, IComparer comparer, PivotSelector pivotSelector) {
            int leftPartitionPoint;
            int rightPartitionPoint;

            if (high - low <= 1) {
                if (comparer.Compare(list[low], list[high]) > 0) {
                    SortingUtils.Swap(list, low, high);
                }

                leftPartitionPoint = low;
                rightPartitionPoint = high;
            }
            else {
                int pivotIndex = pivotSelector(list, low, high, comparer);
                SortingUtils.Swap(list, pivotIndex, high);

                int mid = low;
                object pivot = list[high];

                while (mid <= high) {
                    int comparison = comparer.Compare(list[mid], pivot);

                    if (comparison < 0) {
                        SortingUtils.Swap(list, low, mid);

                        ++low;
                        ++mid;
                    }
                    else if (comparison > 0) {
                        SortingUtils.Swap(list, mid, high);

                        --high;
                    }
                    else {
                        ++mid;
                    }
                }

                leftPartitionPoint = low - 1;
                rightPartitionPoint = mid;
            }

            return new Tuple<int, int>(leftPartitionPoint, rightPartitionPoint);
        }
    }

    public static class PartitionPointDistances {
        public static int At(int partitionPoint) {
            return partitionPoint;
        }

        public static int RightOne(int partitionPoint) {
            return partitionPoint + 1;
        }

        public static int LeftOne(int partitionPoint) {
            return partitionPoint - 1;
        }
    }

    public static class QuickSortAlgorithms {
        public static void Recursive(IList list, int low, int high, IComparer comparer, PartitionDetails partitionDetails) {
            QuickSortAlgorithms.DoRecursive(list, low, high - 1, comparer, partitionDetails);
        }

        private static void DoRecursive(IList list, int low, int high, IComparer comparer, PartitionDetails partitionDetails) {
            if (low < high) {
                dynamic partitionPoint = partitionDetails.PartitionScheme(list, low, high, comparer, partitionDetails.PivotSelector);

                if (partitionPoint is Tuple<int, int>) {
                    QuickSortAlgorithms.DoRecursive(list, low, partitionDetails.Left(partitionPoint.Item1), comparer, partitionDetails);
                    QuickSortAlgorithms.DoRecursive(list, partitionDetails.Right(partitionPoint.Item2), high, comparer, partitionDetails);
                }
                else {
                    QuickSortAlgorithms.DoRecursive(list, low, partitionDetails.Left(partitionPoint), comparer, partitionDetails);
                    QuickSortAlgorithms.DoRecursive(list, partitionDetails.Right(partitionPoint), high, comparer, partitionDetails);
                }
            }
        }

        public static void Iterative(IList list, int low, int high, IComparer comparer, PartitionDetails partitionDetails) {
            --high;

            Stack<int> recursionStack = new Stack<int>(high - low);
            recursionStack.Push(low);
            recursionStack.Push(high);

            while (recursionStack.Count != 0) {
                recursionStack.TryPop(out high);
                recursionStack.TryPop(out low);

                dynamic partitionPoint = partitionDetails.PartitionScheme(list, low, high, comparer, partitionDetails.PivotSelector);
                int leftPartitionPoint;
                int rightPartitionPoint;

                if (partitionPoint is Tuple<int, int>) {
                    leftPartitionPoint = partitionDetails.Left(partitionPoint.Item1);
                    rightPartitionPoint = partitionDetails.Right(partitionPoint.Item2);
                }
                else {
                    leftPartitionPoint = partitionDetails.Left(partitionPoint);
                    rightPartitionPoint = partitionDetails.Right(partitionPoint);
                }

                if (leftPartitionPoint > low) {
                    recursionStack.Push(low);
                    recursionStack.Push(leftPartitionPoint);
                }

                if (rightPartitionPoint < high) {
                    recursionStack.Push(rightPartitionPoint);
                    recursionStack.Push(high);
                }
            }
        }
    }
}
