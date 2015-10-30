using System;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntitiesTests.Tests
{
    [TestClass]
    public class ExpressionTests
    {
        #region AddElement

        [TestMethod]
        public void Elements_ElementCountIncreases_WhenAddIsCalledOnce()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            const int expectedElementCount = 1;
            var buffer = new CharacterBuffer(expectedLiteral);
            var expression = new Expression(buffer);
            var character = new Character(buffer);

            // ACT
            expression.AddElement(character);
            var actualElementCount = expression.Elements.Count;

            // ASSERT
            Assert.AreEqual(expectedElementCount, actualElementCount);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Elements_ThrowsArgumentNullException_WhenAddIsCalledOncewithNullElement()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            var buffer = new CharacterBuffer(expectedLiteral);
            var expression = new Expression(buffer);

            // ACT
            expression.AddElement(null);
            
            // ASSERT
            // exception thrown by here
        }

        #endregion

        #region Elements

        [TestMethod]
        public void Elements_IsInstantiated_WhenClassConstructed()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            var expression = new Expression(expectedLiteral);

            // ASSERT
            Assert.IsNotNull(expression.Elements);
        }

        [TestMethod]
        public void Elements_IsEmpty_WhenClassConstructed()
        {
            // ARRANGE
            const string expectedLiteral = Fakes.Literal.BasicLiteral;
            const int expectedElementCount = 0;
            var expression = new Expression(expectedLiteral);

            // ACT
            var actualElementCount = expression.Elements.Count;

            // ASSERT
            Assert.AreEqual(expectedElementCount, actualElementCount);
        }

        #endregion

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
