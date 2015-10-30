using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EntitiesTests.Tests
{
    [TestClass]
    public class CharacterTests
    {
        #region Constructor

        [TestMethod]
        public void Constructor_IsValidReturnsFalse_WhenConstructedWithBufferAtEnd()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();
            var character = new Character(characterBuffer);

            // ACT
            var isValid = character.IsValid;

            // ASSERT
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void Constructor_IsValidReturnsFalse_WhenConstructedWithRepetitionsOnlySetToTrue()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
            var character = new Character(characterBuffer, true);
            const int expectedCurrentBufferIndex = 0;

            // ACT
            var actualLiteral = character.Literal;
            var actualDescription = character.Description;
            var actualIndex = characterBuffer.CurrentIndexPosition;

            // ASSERT
            Assert.IsNull(actualDescription);
            Assert.IsNull(actualLiteral);
            Assert.AreEqual(expectedCurrentBufferIndex, actualIndex);
        }

        #endregion
    }
}