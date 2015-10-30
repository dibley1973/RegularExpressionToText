using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;
using Utilities.Enumerations;

namespace UtilitiesTests
{
    [TestClass]
    public class ExpressionParserTests
    {
        #region IgnoreWhitespace

        [TestMethod]
        public void WhitespaceBehaviour_ReturnsCorrectValue_WhenClassConstructedWithExpressionThatIgnoreWhiteSpace()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            const WhitespaceBehaviour expectedWhitespaceBehaviour = WhitespaceBehaviour.Ignore;
            var expression = new Expression(expectedLiteral);

            // ACT
            var expressionParser = new ExpressionParser(expression, expectedWhitespaceBehaviour);
            var actualWhitespaceBehaviour = expressionParser.WhitespaceBehaviour;

            // ASSERT
            Assert.AreEqual(expectedWhitespaceBehaviour, actualWhitespaceBehaviour);
        }

        #endregion
    }
}
