
using RegularExpressionToText.Collections;

namespace Elements
{
    public class SubExpression : Element
    {

        public Expression Exp;



        public SubExpression()
        {
            this.Exp = new Expression();
            //this.Image = ImageType.Expression;
        }

        public SubExpression(Expression expression)
        {
            this.Exp = expression;
            //this.Image = ImageType.Expression;
        }

        public SubExpression(string literal, int offset, bool WS, bool IsECMA)
        {
            this.Exp = new Expression(literal, offset, WS, IsECMA);
            this.Literal = literal;
            this.Start = offset;
            this.End = offset + literal.Length;
            //this.Image = ImageType.Expression;
        }

        public SubExpression(string literal, int offset, bool WS, bool IsECMA, bool SkipFirstCaptureNumber)
        {
            this.Exp = new Expression(literal, offset, WS, IsECMA, SkipFirstCaptureNumber);
            this.Literal = literal;
            this.Start = offset;
            this.End = offset + literal.Length;
            //this.Image = ImageType.Expression;
        }

        public override TreeNode<Element> GetNode()
        {
            TreeNode<Element> treeNode;
            TreeNode<Expression>[] nodes = this.Exp.GetNodes();
            if ((int)nodes.Length > 1)
            {
                treeNode = new TreeNode<Element>(this.Exp.Literal);
                treeNode.Nodes.AddRange(this.Exp.GetNodes());
                Element.SetNode(treeNode, this);
            }
            else if ((int)nodes.Length != 1)
            {
                treeNode = new TreeNode<Element>("NULL");
                Element.SetNode(treeNode, this);
            }
            else
            {
                treeNode = nodes[0];
            }
            return treeNode;
        }

        public override string ToString()
        {
            return Exp.ToString();
        }

    }
}
