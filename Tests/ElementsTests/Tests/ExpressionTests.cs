using Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElementsTests.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        #region IgnoreWhitespace

        [TestMethod]
        public void IgnoreWhitespace_ReturnsCorrectValue_WhenClassConstructedWithIgnoreWhiteSpace()
        {

            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            const bool expectedIgnoreWhitespace = true;

            // ACT
            var expression = new Expression(expectedLiteral, expectedIgnoreWhitespace, false);
            var actualIgnoreWhitespace = expression.IgnoreWhitespace;

            // ASSERT
            Assert.AreEqual(expectedIgnoreWhitespace, actualIgnoreWhitespace);
        }

        #endregion

        #region IsEcma

        [TestMethod]
        public void IsEcma_ReturnsCorrectValue_WhenClassConstructedWithIsEcma()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            const bool expectedIsEcma = true;

            // ACT
            var expression = new Expression(expectedLiteral, expectedIsEcma, false);
            var actualIsEcma = expression.IgnoreWhitespace;

            // ASSERT
            Assert.AreEqual(expectedIsEcma, actualIsEcma);
        }

        #endregion

        #region Literal

        [TestMethod]
        public void Literal_ReturnsCorrectValue_WhenClassConstructedWithLiteral()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            
            // ACT
            var expression = new Expression(expectedLiteral);
            var actualLiteral = expression.Literal;

            // ASSERT
            Assert.AreEqual(expectedLiteral, actualLiteral);
        }

        [TestMethod]
        public void Literal_ReturnsCorrectValue_WhenClassConstructedWithCharacterBuffer()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(expectedLiteral);
            
            // ACT
            var expression = new Expression(characterBuffer);
            var actualLiteral = expression.Literal;

            // ASSERT
            Assert.AreEqual(expectedLiteral, actualLiteral);
        }

        #endregion
    }
}
