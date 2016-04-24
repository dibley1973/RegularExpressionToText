using System.Collections;

namespace Elements
{
    public class ExpressionList
    {
        private ArrayList Expressions;

        public int Count
        {
            get
            {
                return Expressions.Count;
            }
        }

        public SubExpression this[int i]
        {
            get
            {
                return (SubExpression)this.Expressions[i];
            }
        }



        public ExpressionList()
        {
            this.Expressions = new ArrayList();
        }

        public void Add(SubExpression expression)
        {
            this.Expressions.Add(expression);
        }
    }
}
