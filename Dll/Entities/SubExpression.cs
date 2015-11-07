
namespace Entities
{
    public class SubExpression : Element
    {
        #region Properties

        /// <summary>
        /// Gets or sets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public Expression Expression { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SubExpression"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public SubExpression(Expression expression)
        {
            Expression = expression;
        }

        #endregion
    }
}
