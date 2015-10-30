using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElementsTests.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        #region HasEcmaSyntax

        [TestMethod]
        public void IsEcma_ReturnsCorrectValue_WhenClassConstructedWithIsEcma()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            const bool expectedIsEcma = true;

            // ACT
            var expression = new Expression(expectedLiteral, expectedIsEcma);
            var actualIsEcma = expression.HasEcmaSyntax;

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
