
using System;

namespace RegularExpressionToText.Collections
{
    public class TreeNode<T>
    {
        private TreeNodeList<T> _nodes;
        private string _text;

        public TreeNodeList<T> Nodes
        {
            get
            {
                return _nodes ?? (_nodes = new TreeNodeList<T>(this));
            }
        }

        public TreeNode()
        {
        }

        public TreeNode(string text)
            : this()
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException("text");

            _text = text;
        }

        public T Tag { get; set; }

        public string Text
        {
            get
            {
                return _text ?? "";
            }
            set
            {
                _text = value;
            }
        }
    }
}
