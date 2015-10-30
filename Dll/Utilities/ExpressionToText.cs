using System;
using Entities;

namespace Utilities
{
    /// <summary>
    /// Responsible for converting the elemenets of the specified regular expressions to plain text
    /// </summary>
    public class ExpressionToText
    {
        #region Fields

        private readonly Expression _expression;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public Expression Expression
        {
            get { return _expression; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionToText"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public ExpressionToText(Expression expression)
        {
            _expression = expression;
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