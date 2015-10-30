using System;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace UtilitiesTests
{
    [TestClass]
    public class ExpressionToTextTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void ToText_ThrowsNotImplementedexception()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            Expression expression = new Expression(expectedLiteral);
            ExpressionToText expressionToText = new ExpressionToText(expression);

            // ACT
            expressionToText.ToText();

        }
    }
}