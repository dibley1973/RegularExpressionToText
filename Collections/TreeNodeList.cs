using System;
using System.Collections.Generic;

namespace RegularExpressionToText.Collections
{
    public class TreeNodeList<T>
    {
        private readonly List<TreeNode<T>> _children;
        private readonly TreeNode<T> _owner;

        internal TreeNodeList(TreeNode<T> owner)
            : this(owner, new List<TreeNode<T>>())
        { }

        internal TreeNodeList(TreeNode<T> owner, List<TreeNode<T>> children)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            if (children == null) throw new ArgumentNullException("children");

            _children = children;
            _owner = owner;
        }

        private int AddInternal(TreeNode<T> node, int delta)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }
            //if (node.handle != IntPtr.Zero)
            //{
            //    object[] text = new object[] { node.Text };
            //    throw new ArgumentException(SR.GetString("OnlyOneControl", text), "node");
            //}
            //TreeView treeView = this.owner.TreeView;
            //if (treeView != null && treeView.Sorted)
            //{
            //    return this.owner.AddSorted(node);
            //}
            //node.parent = this.owner;
            //int fixedIndex = this.owner.Nodes.FixedIndex;
            //if (fixedIndex == -1)
            //{
            //    this.owner.EnsureCapacity(1);
            //    node.index = this.owner.childCount;
            //}
            //else
            //{
            //    node.index = fixedIndex + delta;
            //}
            //this.owner.children[node.index] = node;
            //TreeNode treeNode = this.owner;
            //treeNode.childCount = treeNode.childCount + 1;
            //node.Realize(false);
            //if (treeView != null && node == treeView.selectedNode)
            //{
            //    treeView.SelectedNode = node;
            //}
            //if (treeView != null && treeView.TreeViewNodeSorter != null)
            //{
            //    treeView.Sort();
            //}
            //return node.index;

            return -1; // TODO Correct to something meaningful!
        }

        public void AddRange(TreeNodeList<T> nodes)
        {
        }

        public void AddRange(TreeNode<T>[] nodes)
        {
            if (nodes == null)
            {
                throw new ArgumentNullException("nodes");
            }
            if (nodes.Length == 0)
            {
                return;
            }
            //TreeView treeView = this.owner.TreeView;
            //if (treeView != null && (int)nodes.Length > 200)
            //{
            //    treeView.BeginUpdate();
            //}
            //this.owner.Nodes.FixedIndex = this.owner.childCount;
            //this.owner.EnsureCapacity((int)nodes.Length);
            for (int i = (int)nodes.Length - 1; i >= 0; i--)
            {
                this.AddInternal(nodes[i], i);
            }
            //this.owner.Nodes.FixedIndex = -1;
            //if (treeView != null && (int)nodes.Length > 200)
            //{
            //    treeView.EndUpdate();
            //}
        }

        public int ChildCount
        {
            get
            {
                return Children.Count;
            }
        }

        protected internal List<TreeNode<T>> Children
        {
            get { return _children; }
        }

        protected internal TreeNode<T> Owner
        {
            get { return _owner; }
        }
    }
}
