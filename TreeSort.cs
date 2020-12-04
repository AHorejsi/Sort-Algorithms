using System;
using System.Collections.Generic;

namespace Sorting {
    public sealed class TreeSorter<N> : ICompareSorter<N> {
        private readonly ISortingTree<N> tree;

        internal TreeSorter(ISortingTree<N> tree) {
            this.tree = tree;
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            SortUtils.CheckRange(low, high);

            for (int index = low; index < high; ++index) {
                this.tree.Insert(list[index], comparer);
            }

            this.tree.Traverse(list, low);
            this.tree.Empty();
        }
    }

    internal interface ISortingTree<N> {
        public void Insert(N value, IComparer<N> comparer);

        public void Traverse(IList<N> list, int low);

        public void Empty();
    }

    internal class RedBlackSortingTree<N> : ISortingTree<N> {
        private enum ColorType { RED, BLACK }

        private class RbNode {
            public LinkedList<N> valueList;
            public ColorType color;
            public RbNode parent = RedBlackSortingTree<N>.Nil;
            public RbNode left = RedBlackSortingTree<N>.Nil;
            public RbNode right = RedBlackSortingTree<N>.Nil;

            public RbNode(ColorType color) {
                this.valueList = new LinkedList<N>();
                this.color = color;
            }

            public RbNode(ColorType color, N value) : this(color) {
                this.valueList.AddLast(value);
            }
        }

        private RbNode root;
        private static readonly RbNode Nil = new RbNode(ColorType.BLACK);

        public RedBlackSortingTree() {
            this.root = RedBlackSortingTree<N>.Nil;
        }

        public void Insert(N value, IComparer<N> comparer) {
            if (this.root == RedBlackSortingTree<N>.Nil) {
                this.root = new RbNode(ColorType.BLACK, value);
            }
            else {
                RbNode node = this.FindInsertionPoint(value, comparer);
                int comparison = comparer.Compare(value, node.valueList.First!.Value);

                if (0 == comparison) {
                    node.valueList.AddLast(value);
                }
                else {
                    RbNode newNode = new RbNode(ColorType.RED, value);

                    if (comparison < 0) {
                        node.left = newNode;
                    }
                    else {
                        node.right = newNode;
                    }

                    this.InsertionCleanup(newNode);
                }
            }
        }

        private RbNode FindInsertionPoint(N value, IComparer<N> comparer) {
            RbNode node = this.root;
            RbNode parent;

            do {
                int comparison = comparer.Compare(value, node.valueList.First!.Value);
                parent = node;

                if (comparison < 0) {
                    node = node.left;
                }
                else if (comparison > 0) {
                    node = node.right;
                }
                else {
                    return node;
                }
            } while (node != RedBlackSortingTree<N>.Nil);

            return parent;
        }

        private void InsertionCleanup(RbNode newNode) {
            RbNode parent = newNode.parent;

            if (ColorType.RED == parent.color) {
                RbNode uncle = this.Uncle(newNode);
                RbNode grandparent = this.Grandparent(newNode);

                if (ColorType.RED == uncle.color) {
                    newNode.parent.color = ColorType.BLACK;
                    uncle.color = ColorType.BLACK;
                    grandparent.color = ColorType.RED;

                    this.InsertionCleanup(grandparent);
                }
                else {
                    if (this.IsLeftChild(newNode) && this.IsRightChild(parent)) {
                        this.LeftRotate(parent, newNode);
                    }
                    else if (this.IsRightChild(newNode) && this.IsLeftChild(parent)) {
                        this.RightRotate(parent, newNode);
                    }

                    if (this.IsLeftChild(newNode) && this.IsRightChild(parent)) {
                        parent.color = ColorType.BLACK;
                        grandparent.color = ColorType.RED;

                        this.RightRotate(grandparent, parent);
                    }
                    else if (this.IsRightChild(newNode) && this.IsRightChild(parent)) {
                        parent.color = ColorType.BLACK;
                        grandparent.color = ColorType.RED;

                        this.LeftRotate(grandparent, parent);
                    }
                }
            }
        }

        private bool IsLeftChild(RbNode node) {
            return this.root != node ? node.parent.left == node : false;
        }

        private bool IsRightChild(RbNode node) {
		    return this.root != node && node.parent.right == node;
        }

        private RbNode Uncle(RbNode node) {
            if (this.IsRightChild(node)) {
                if (this.IsRightChild(node.parent))
                    return node.parent.parent.left;
                else
                    return node.parent.parent.right;
            }
            else {
                if (this.IsRightChild(node.parent))
                    return node.parent.parent.left;
                else
                    return node.parent.parent.right;
            }
        }

        private RbNode Grandparent(RbNode node) {
            return node.parent.parent;
        }

        private void LeftRotate(RbNode root, RbNode pivot) {
            if (this.IsLeftChild(root)) {
                root.parent.left = pivot;
                pivot.parent = root.parent;
                root.right = pivot.left;

                if (pivot.left != RedBlackSortingTree<N>.Nil) {
                    pivot.left.parent = root;
                }

                pivot.left = root;
                root.parent = pivot;
            }
            else if (this.IsRightChild(root)) {
                root.parent.right = pivot;
                pivot.parent = root.parent;
                root.right = pivot.left;

                if (pivot.left != RedBlackSortingTree<N>.Nil) {
                    pivot.left.parent = root;
                }

                pivot.left = root;
                root.parent = pivot;
            }
        }

        private void RightRotate(RbNode root, RbNode pivot) {
            if (this.IsLeftChild(root)) {
                root.parent.left = pivot;
                pivot.parent = root.parent;
                root.left = pivot.right;

                if (pivot.right != RedBlackSortingTree<N>.Nil) {
                    pivot.right.parent = root;
                }
                
                pivot.right = root;
                root.parent = pivot;
            }
            else if (this.IsRightChild(root)) {
                root.parent.right = pivot;
                pivot.parent = root.parent;
                root.left = pivot.right;

                if (pivot.right != RedBlackSortingTree<N>.Nil) {
                    pivot.right.parent = root;
                }
               
                pivot.right = root;
                root.parent = pivot;
            }
        }

        public void Traverse(IList<N> list, int index) {
            this.InorderTraverse(list, index, this.root);
        }

        private int InorderTraverse(IList<N> list, int currentIndex, RbNode node) {
            if (node != RedBlackSortingTree<N>.Nil) {
                currentIndex = this.InorderTraverse(list, currentIndex, node.left);

                foreach (N val in node.valueList) {
                    list[currentIndex] = val;
                    ++currentIndex;
                }

                currentIndex = this.InorderTraverse(list, currentIndex, node.right);
            }

            return currentIndex;
        }

        public void Empty() {
            this.root = RedBlackSortingTree<N>.Nil;
        }
    }

    public class AvlSortingTree<N> : ISortingTree<N> {
        public void Insert(N value, IComparer<N> comparer) {
            throw new NotImplementedException();
        }

        public void Traverse(IList<N> list, int index) {
            throw new NotImplementedException();
        }

        public void Empty() {
            throw new NotImplementedException();
        }
    }

    public class SplaySortingTree<N> : ISortingTree<N> {
        public void Insert(N value, IComparer<N> comparer) {
            throw new NotImplementedException();
        }

        public void Traverse(IList<N> list, int index) {
            throw new NotImplementedException();
        }

        public void Empty() {
            throw new NotImplementedException();
        }
    }

    public enum TreeType { RED_BLACK, AVL, SPLAY }

    public static class TreeSortFactory<N> {
        public static TreeSorter<N> Make(TreeType type) {
            ISortingTree<N> sortingTree = type switch {
                TreeType.RED_BLACK => new RedBlackSortingTree<N>(),
                TreeType.AVL => new AvlSortingTree<N>(),
                TreeType.SPLAY => new SplaySortingTree<N>(),
                _ => throw new InvalidOperationException(),
            };

            return new TreeSorter<N>(sortingTree);
        }
    }
}
