using Elements;
using System;

namespace Utilities
{
    /// <summary>
    /// Responsible for converting specified regular expressions to plain text
    /// </summary>
    public class ExpressionToText
    {
        #region Properties

        public Expression Expression { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionToText"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public ExpressionToText(Expression expression)
        {
            Expression = expression;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converst the expression to text.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException">ToText is not implemented yet! </exception>
        public string ToText()
        {
            throw new NotImplementedException("ToText is not implemented yet! ");
        }

        #endregion
    }
}