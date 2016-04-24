using Elements.Enumerations;

namespace Elements
{
    public class TreeNode
    {
        private string _text;
        private TreeNodeCollection _nodes;

        public TreeNodeCollection Nodes
        {
            get
            {
                return _nodes ?? (_nodes = new TreeNodeCollection(this));
            }
        }

        public Element Element { get; set; }
        public NodeType Type { get; set; }

        public TreeNode()
        {
        }

        public TreeNode(string text)
            : this()
        {
            _text = text;
        }

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