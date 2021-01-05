using System;
using System.Collections.Generic;

namespace Sorting {
    public enum TreeType { RED_BLACK, AVL, SPLAY }

    public sealed class TreeSorter<N> : ICompareSorter<N>, IEquatable<TreeSorter<N>> {
        private ISortingTree<N> tree;
        private TreeType treeType;

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public TreeSorter(TreeType treeType) {
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            this.TreeType = treeType;
        }

        public TreeType TreeType {
            get => this.treeType;
            set {
                this.treeType = value;
                this.tree = value switch {
                    TreeType.RED_BLACK => new RedBlackSortingTree<N>(),
                    TreeType.AVL => new AvlSortingTree<N>(),
                    TreeType.SPLAY => new SplaySortingTree<N>(),
                    _ => throw new InvalidOperationException(),
                };
            }
        }

        public void Sort(IList<N> list, int low, int high, IComparer<N> comparer) {
            for (int index = low; index < high; ++index) {
                this.tree.Insert(list[index], comparer);
            }

            this.tree.Traverse(list, low);
            this.tree.Empty();
        }

        public override bool Equals(object? obj) => this.Equals(obj as TreeSorter<N>);

        public bool Equals(TreeSorter<N>? sorter) => sorter is null || this.treeType == sorter.treeType;

        public override int GetHashCode() => HashCode.Combine(this.treeType);
    }

    internal interface ISortingTree<N> {
        public void Insert(N value, IComparer<N> comparer);

        public void Traverse(IList<N> list, int low);

        public void Empty();
    }

    internal sealed class RedBlackSortingTree<N> : ISortingTree<N> {
        private enum Color { RED, BLACK }

        private class RbNode {
            public LinkedList<N> ValueList { get; } = new LinkedList<N>();
            public Color Color { get; set; }
            public RbNode Parent { get; set; } = RedBlackSortingTree<N>.Nil;
            public RbNode Left { get; set; } = RedBlackSortingTree<N>.Nil;
            public RbNode Right { get; set; } = RedBlackSortingTree<N>.Nil;

            public RbNode(Color color) {
                this.Color = color;
            }

            public RbNode(Color color, N value) : this(color) {
                this.ValueList.AddLast(value);
            }
        }

        private RbNode root;
        private static readonly RbNode Nil = new RbNode(Color.BLACK);

        public RedBlackSortingTree() {
            this.root = RedBlackSortingTree<N>.Nil;
        }

        public void Insert(N value, IComparer<N> comparer) {
            if (this.root == RedBlackSortingTree<N>.Nil) {
                this.root = new RbNode(Color.BLACK, value);
            }
            else {
                RbNode insertionPoint = this.FindInsertionPoint(value, comparer);
                int comparison = comparer.Compare(value, insertionPoint.ValueList.First!.Value);

                if (0 == comparison) {
                    insertionPoint.ValueList.AddLast(value);
                }
                else {
                    var newNode = new RbNode(Color.RED, value);

                    if (comparison < 0) {
                        insertionPoint.Left = newNode;
                    }
                    else {
                        insertionPoint.Right = newNode;
                    }

                    this.InsertionCleanup(newNode);
                }
            }
        }

        private RbNode FindInsertionPoint(N value, IComparer<N> comparer) {
            RbNode node = this.root;
            RbNode parent;

            do {
                int comparison = comparer.Compare(value, node.ValueList.First!.Value);
                parent = node;

                if (comparison < 0) {
                    node = node.Left;
                }
                else if (comparison > 0) {
                    node = node.Right;
                }
                else {
                    return node;
                }
            } while (node != RedBlackSortingTree<N>.Nil);

            return parent;
        }

        private void InsertionCleanup(RbNode newNode) {
            RbNode parent = newNode.Parent;

            if (Color.RED == parent.Color) {
                RbNode uncle = this.Uncle(newNode);
                RbNode grandparent = this.Grandparent(newNode);

                if (Color.RED == uncle.Color) {
                    newNode.Parent.Color = Color.BLACK;
                    uncle.Color = Color.BLACK;
                    grandparent.Color = Color.RED;

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
                        parent.Color = Color.BLACK;
                        grandparent.Color = Color.RED;

                        this.RightRotate(grandparent, parent);
                    }
                    else if (this.IsRightChild(newNode) && this.IsRightChild(parent)) {
                        parent.Color = Color.BLACK;
                        grandparent.Color = Color.RED;

                        this.LeftRotate(grandparent, parent);
                    }
                }
            }
        }

        private bool IsLeftChild(RbNode node) {
            return this.root != node && node.Parent.Left == node;
        }

        private bool IsRightChild(RbNode node) {
		    return this.root != node && node.Parent.Right == node;
        }

        private RbNode Uncle(RbNode node) {
            if (this.IsRightChild(node)) {
                if (this.IsRightChild(node.Parent))
                    return node.Parent.Parent.Left;
                else
                    return node.Parent.Parent.Right;
            }
            else {
                if (this.IsRightChild(node.Parent))
                    return node.Parent.Parent.Left;
                else
                    return node.Parent.Parent.Right;
            }
        }

        private RbNode Grandparent(RbNode node) {
            return node.Parent.Parent;
        }

        private void LeftRotate(RbNode root, RbNode pivot) {
            if (this.IsLeftChild(root)) {
                root.Parent.Left = pivot;
                pivot.Parent = root.Parent;
                root.Right = pivot.Left;

                if (pivot.Left != RedBlackSortingTree<N>.Nil) {
                    pivot.Left.Parent = root;
                }

                pivot.Left = root;
                root.Parent = pivot;
            }
            else if (this.IsRightChild(root)) {
                root.Parent.Right = pivot;
                pivot.Parent = root.Parent;
                root.Right = pivot.Left;

                if (pivot.Left != RedBlackSortingTree<N>.Nil) {
                    pivot.Left.Parent = root;
                }

                pivot.Left = root;
                root.Parent = pivot;
            }
        }

        private void RightRotate(RbNode root, RbNode pivot) {
            if (this.IsLeftChild(root)) {
                root.Parent.Left = pivot;
                pivot.Parent = root.Parent;
                root.Left = pivot.Right;

                if (pivot.Right != RedBlackSortingTree<N>.Nil) {
                    pivot.Right.Parent = root;
                }
                
                pivot.Right = root;
                root.Parent = pivot;
            }
            else if (this.IsRightChild(root)) {
                root.Parent.Right = pivot;
                pivot.Parent = root.Parent;
                root.Left = pivot.Right;

                if (pivot.Right != RedBlackSortingTree<N>.Nil) {
                    pivot.Right.Parent = root;
                }
               
                pivot.Right = root;
                root.Parent = pivot;
            }
        }

        public void Traverse(IList<N> list, int index) {
            this.InorderTraverse(list, index, this.root);
        }

        private int InorderTraverse(IList<N> list, int index, RbNode node) {
            if (node != RedBlackSortingTree<N>.Nil) {
                index = this.InorderTraverse(list, index, node.Left);

                foreach (N value in node.ValueList) {
                    list[index] = value;
                    ++index;
                }

                index = this.InorderTraverse(list, index, node.Right);
            }

            return index;
        }

        public void Empty() {
            this.root = RedBlackSortingTree<N>.Nil;
        }
    }

    internal sealed class AvlSortingTree<N> : ISortingTree<N> {
        private class AvlNode {
            public LinkedList<N> ValueList { get; } = new LinkedList<N>();
            public int Height { get; set; }
            public AvlNode Left { get; set; } = AvlSortingTree<N>.Nil;
            public AvlNode Right { get; set; } = AvlSortingTree<N>.Nil;

            public AvlNode(int height) {
                this.Height = height;
            }

            public AvlNode(N value) {
                this.ValueList.AddLast(value);
            }

            public AvlNode(N value, int height) : this(height) {
                this.ValueList.AddLast(value);
            }
        }

        private AvlNode root;
        private static readonly AvlNode Nil = new AvlNode(-1);

        public AvlSortingTree() {
            this.root = AvlSortingTree<N>.Nil;
        }

        public void Insert(N value, IComparer<N> comparer) {
            if (AvlSortingTree<N>.Nil == this.root) {
                this.root = new AvlNode(value, 0);
            }
            else {
                AvlNode insertionPoint = this.FindInsertionPoint(value, comparer);
                int comparison = comparer.Compare(value, insertionPoint.ValueList.First!.Value);
                
                if (0 == comparison) {
                    insertionPoint.ValueList.AddLast(value);
                }
                else {
                    var newNode = new AvlNode(value);

                    if (comparison < 0) {
                        insertionPoint.Left = newNode;
                    }
                    else {
                        insertionPoint.Right = newNode;
                    }

                    this.InsertionCleanup(newNode);
                }
            }
        }

        private AvlNode FindInsertionPoint(N value, IComparer<N> comparer) {
            AvlNode node = this.root;
            AvlNode parent;

            do {
                int comparison = comparer.Compare(value, node.ValueList.First!.Value);
                parent = node;

                if (comparison < 0) {
                    node = node.Left;
                }
                else if (comparison > 0) {
                    node = node.Right;
                }
                else {
                    return node;
                }
            } while (node != AvlSortingTree<N>.Nil);

            return parent;
        }

        private void InsertionCleanup(AvlNode node) {
            this.UpdateHeight(node);

            int balance = this.Balance(node);

            if (balance > 1) {
                if (node.Right.Right.Height > node.Right.Left.Height) {
                    this.RotateLeft(node);
                }
                else {
                    this.RotateRight(node.Right);
                    this.RotateLeft(node);
                }
            }
            else if (balance < -1) {
                if (node.Left.Left.Height > node.Left.Right.Height) {
                    this.RotateRight(node);
                }
                else {
                    this.RotateLeft(node.Left);
                    this.RotateRight(node);
                }
            }
        }

        private void UpdateHeight(AvlNode node) {
            node.Height = 1 + Math.Max(node.Left.Height, node.Right.Height);
        }

        private int Balance(AvlNode node) => node.Right.Height - node.Left.Height;

        private void RotateLeft(AvlNode node) {
            AvlNode child = node.Right;
            AvlNode grandchild = child.Left;

            grandchild.Left = node;
            node.Right = grandchild;

            this.UpdateHeight(node);
            this.UpdateHeight(child);
        }

        private void RotateRight(AvlNode node) {
            AvlNode child = node.Left;
            AvlNode grandchild = child.Right;

            child.Right = node;
            node.Left = grandchild;

            this.UpdateHeight(node);
            this.UpdateHeight(child);
        }

        public void Traverse(IList<N> list, int index) {
            this.InorderTraverse(list, index, this.root);
        }

        private int InorderTraverse(IList<N> list, int index, AvlNode node) {
            if (node != AvlSortingTree<N>.Nil) {
                index = this.InorderTraverse(list, index, node.Left);

                foreach (N value in node.ValueList) {
                    list[index] = value;
                    ++index;
                }

                index = this.InorderTraverse(list, index, node.Right);
            }

            return index;
        }

        public void Empty() {
            this.root = AvlSortingTree<N>.Nil;
        }
    }

    internal sealed class SplaySortingTree<N> : ISortingTree<N> {
        private class SplayNode {
            public LinkedList<N> ValueList { get; } = new LinkedList<N>();
            public SplayNode? Left { get; set; }
            public SplayNode? Right { get; set; }

            public SplayNode(N value) {
                this.ValueList.AddLast(value);
            }
        }

        private SplayNode? root;

        public void Insert(N value, IComparer<N> comparer) {
            if (this.root is null) {
                this.root = new SplayNode(value);
            }
            else {
                SplayNode insertionPoint = this.FindInsertionPoint(value, comparer);
                int comparison = comparer.Compare(value, insertionPoint.ValueList.First!.Value);

                if (0 == comparison) {
                    insertionPoint.ValueList.AddLast(value);
                }
                else {
                    var newNode = new SplayNode(value);

                    if (comparison < 0) {
                        insertionPoint.Left = newNode;
                    }
                    else {
                        insertionPoint.Right = newNode;
                    }

                    this.InsertionCleanup(newNode, newNode.ValueList.First!.Value, comparer);
                }
            }
        }

        private SplayNode FindInsertionPoint(N value, IComparer<N> comparer) {
            SplayNode node = this.root!;
            SplayNode parent;

            do {
                int comparison = comparer.Compare(value, node!.ValueList.First!.Value);
                parent = node;

                if (comparison < 0) {
                    node = node.Left!;
                }
                else if (comparison > 0) {
                    node = node.Right!;
                }
                else {
                    return node;
                }
            } while (node != null);

            return parent;
        }

        private SplayNode? InsertionCleanup(SplayNode? node, N key, IComparer<N> comparer) {
            if (node is null || 0 == comparer.Compare(node.ValueList.First!.Value, key)) {
                return node;
            }

            if (comparer.Compare(node.ValueList.First!.Value, key) > 0) {
                if (node.Left is null) {
                    return node;
                }

                if (comparer.Compare(node.Left.ValueList.First!.Value, key) > 0) {
                    node.Left.Left = this.InsertionCleanup(node.Left.Left, key, comparer);
                    this.RightRotate(node);
                }
                else if (comparer.Compare(node.Left.ValueList.First!.Value, key) < 0)   {
                    node.Left.Right = this.InsertionCleanup(node.Left.Right, key, comparer);

                    if (!(node.Left.Right is null)) {
                        this.LeftRotate(node.Left);
                    }
                }

                return (node.Left is null) ? node : this.RightRotate(node);
            }
            else {
                if (node.Right is null) {
                    return root;
                }

                if (comparer.Compare(node.Right.ValueList.First!.Value, key) > 0) {
                    node.Right.Left = this.InsertionCleanup(node.Right.Left, key, comparer);

                    if (!(node.Right.Left is null)) {
                        node.Right = this.RightRotate(node.Right);
                    }
                }
                else if (comparer.Compare(node.Right.ValueList.First!.Value, key) < 0) {
                    node.Right.Right = this.InsertionCleanup(node.Right.Right, key, comparer);
                    node = this.LeftRotate(node);
                }
                
                return (node!.Right is null) ? node : this.LeftRotate(node);
            }
        }

        private SplayNode? RightRotate(SplayNode root) {
            SplayNode? node = root.Left;

            root.Left = node!.Right;
            node!.Right = root;

            return node;
        }
 
        private SplayNode? LeftRotate(SplayNode root) {
            SplayNode? node = root.Right;

            root.Right = node!.Left;
            node!.Left = root;

            return node;
        }

        public void Traverse(IList<N> list, int index) {
            this.InorderTraverse(list, this.root, index);
        }

        private int InorderTraverse(IList<N> list, SplayNode? current, int index) {
            if (!(current is null)) {
                index = this.InorderTraverse(list, current.Left, index);

                foreach (N value in current.ValueList) {
                    list[index] = value;
                    ++index;
                }

                index = this.InorderTraverse(list, current.Right, index);
            }

            return index;
        }

        public void Empty() {
            this.root = null;
        }
    }
}
