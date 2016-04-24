
namespace Elements
{
    public class Alternatives : Element
    {
        private ExpressionList Expressions;

        public int Count
        {
            get
            {
                return this.Expressions.Count;
            }
        }

        public SubExpression this[int i]
        {
            get
            {
                return this.Expressions[i];
            }
        }

        public void Add(SubExpression expression)
        {
            if (this.Expressions.Count != 0)
            {
                Alternatives alternative = this;
                alternative.Literal = string.Concat(alternative.Literal, "|", expression.Literal);
            }
            else
            {
                this.Start = expression.Start;
                this.Literal = expression.Literal;
            }
            this.End = expression.End;
            this.Expressions.Add(expression);
        }

        public Alternatives()
        {
            this.Expressions = new ExpressionList();
            //this.Image = ImageType.Alternative;
            this.Literal = "";
        }
    }
}