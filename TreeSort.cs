using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting {
    public class TreeSorter : CompareSorter, IEquatable<TreeSorter> {
        private readonly ISortingTree tree;

        internal TreeSorter(ISortingTree tree) {
            this.tree = tree;
        }

        public override void Sort(IList list, int low, int high, IComparer comparer) {
            for (int index = low; index < high; ++index) {
                this.tree.Insert(list[index], comparer);
            }

            this.tree.Traverse(list, low);
            this.tree.Empty();
        }

        public override bool Equals(object? obj) {
            return this.Equals(obj as TreeSorter);
        }

        public bool Equals(TreeSorter? sorter) {
            if (sorter is null) {
                return false;
            }
            else {
                return this.tree.GetType().Equals(sorter.tree.GetType());
            }
        }

        public override int GetHashCode() {
            return this.tree.GetType().GetHashCode();
        }
    }

    internal interface ISortingTree {
        public void Insert(object? value, IComparer comparer);

        public void Traverse(IList list, int low);

        public void Empty();
    }

    internal class RedBlackSortingTree : ISortingTree {
        private enum ColorType { RED, BLACK }

        private class RbNode {
            public LinkedList<object?> valueList;
            public ColorType color;
            public RbNode parent = RedBlackSortingTree.Nil;
            public RbNode left = RedBlackSortingTree.Nil;
            public RbNode right = RedBlackSortingTree.Nil;

            public RbNode(ColorType color) {
                this.valueList = new LinkedList<object?>();
                this.color = color;
            }

            public RbNode(ColorType color, object? value) : this(color) {
                this.valueList.AddLast(value);
            }
        }

        private RbNode root;
        private static readonly RbNode Nil = new RbNode(ColorType.BLACK);

        public RedBlackSortingTree() {
            this.root = RedBlackSortingTree.Nil;
        }

        public void Insert(object? value, IComparer comparer) {
            if (this.root == RedBlackSortingTree.Nil) {
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

        private RbNode FindInsertionPoint(object? value, IComparer comparer) {
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
            } while (node != RedBlackSortingTree.Nil);

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

                if (pivot.left != RedBlackSortingTree.Nil) {
                    pivot.left.parent = root;
                }

                pivot.left = root;
                root.parent = pivot;
            }
            else if (this.IsRightChild(root)) {
                root.parent.right = pivot;
                pivot.parent = root.parent;
                root.right = pivot.left;

                if (pivot.left != RedBlackSortingTree.Nil) {
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

                if (pivot.right != RedBlackSortingTree.Nil) {
                    pivot.right.parent = root;
                }
                
                pivot.right = root;
                root.parent = pivot;
            }
            else if (this.IsRightChild(root)) {
                root.parent.right = pivot;
                pivot.parent = root.parent;
                root.left = pivot.right;

                if (pivot.right != RedBlackSortingTree.Nil) {
                    pivot.right.parent = root;
                }
               
                pivot.right = root;
                root.parent = pivot;
            }
        }

        public void Traverse(IList list, int index) {
            this.InorderTraverse(list, index, this.root);
        }

        private int InorderTraverse(IList list, int currentIndex, RbNode node) {
            if (node != RedBlackSortingTree.Nil) {
                currentIndex = this.InorderTraverse(list, currentIndex, node.left);

                foreach (object? val in node.valueList) {
                    list[currentIndex] = val;
                    ++currentIndex;
                }

                currentIndex = this.InorderTraverse(list, currentIndex, node.right);
            }

            return currentIndex;
        }

        public void Empty() {
            this.root = RedBlackSortingTree.Nil;
        }
    }

    public class AvlSortingTree : ISortingTree {
        public void Insert(object? value, IComparer comparer) {
            throw new NotImplementedException();
        }

        public void Traverse(IList list, int index) {
            throw new NotImplementedException();
        }

        public void Empty() {
            throw new NotImplementedException();
        }
    }

    public class SplaySortingTree : ISortingTree {
        public void Insert(object? value, IComparer comparer) {
            throw new NotImplementedException();
        }

        public void Traverse(IList list, int index) {
            throw new NotImplementedException();
        }

        public void Empty() {
            throw new NotImplementedException();
        }
    }

    public enum TreeType { RED_BLACK, AVL, SPLAY }

    public static class TreeSortFactory {
        public static TreeSorter Make(TreeType type) {
            ISortingTree sortingTree = type switch {
                TreeType.RED_BLACK => new RedBlackSortingTree(),
                TreeType.AVL => new AvlSortingTree(),
                TreeType.SPLAY => new SplaySortingTree(),
                _ => throw new InvalidOperationException(),
            };

            return new TreeSorter(sortingTree);
        }
    }
}
