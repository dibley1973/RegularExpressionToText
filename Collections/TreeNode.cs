
using System;

namespace RegularExpressionToText.Collections
{
    public class TreeNode
    {
        private TreeNodeList _nodes;
        private string _text;

        public TreeNodeList Nodes
        {
            get
            {
                return _nodes ?? (_nodes = new TreeNodeList(this));
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

        public object Tag { get; set; }

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
