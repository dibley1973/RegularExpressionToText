using System;

namespace Elements
{
    public class TreeNodeCollection // : IList, ICollection, IEnumerable
    {
        private TreeNode owner;

        internal TreeNodeCollection(TreeNode owner)
        {
            this.owner = owner;
        }

        private int AddInternal(TreeNode node, int delta)
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


        public virtual void AddRange(TreeNode[] nodes)
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

    }
}
