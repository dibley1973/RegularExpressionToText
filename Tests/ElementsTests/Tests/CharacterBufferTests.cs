using System;
using Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElementsTests.Tests
{
    [TestClass]
    public class CharacterBufferTests
    {
        #region Construction

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ThrowsArgumentNullException_WhenClassConstructedWithEmptyString()
        {
            // ARRANGE
            string data = string.Empty;

            // ACT
            new CharacterBuffer(data);
            
            // ASSERT
            // Exception thrown by here!
        }

        #endregion

        #region CurrentCharacter

        [TestMethod]
        public void CurrentCharacter_ReturnsFirstCharacterOfData_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            char expectedCharacter = data.ToCharArray(0, 1)[0];

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualCharacter = characterBuffer.CurrentCharacter;

            // ASSERT
            Assert.AreEqual(expectedCharacter, actualCharacter);
        }

        #endregion

        #region CurrentIndexPosition

        [TestMethod]
        public void CurrentIndexPosition_ReturnsZero_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            const int expectedCurrentPosition = 0;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualCurrentPosition = characterBuffer.CurrentIndexPosition;

            // ASSERT
            Assert.AreEqual(expectedCurrentPosition, actualCurrentPosition);
        }

        #endregion

        #region Length

        [TestMethod]
        public void DataLength_ReturnsCorrectValue_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualDataLength = characterBuffer.Length;

            // ASSERT
            Assert.AreEqual(data.Length, actualDataLength);
        }

        #endregion

        #region GetToEnd

        [TestMethod]
        public void GetToEnd_ReturnsFullDataLength_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
                
            // ACT
            var actualData = characterBuffer.GetToEnd();
            var actualDataLength = actualData.Length;

            // ASSERT
            Assert.AreEqual(data.Length, actualDataLength);
        }

        #endregion

        #region IsAtBeginning

        [TestMethod]
        public void IsAtBeginning_ReturnsTrue_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var isAtBeginning = characterBuffer.IsAtBeginning;

            // ASSERT
            Assert.IsTrue(isAtBeginning);
        }

        #endregion
        
        #region IsAtEnd

        [TestMethod]
        public void IsAtEnd_ReturnsFalse_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var isAtEnd = characterBuffer.IsAtEnd;

            // ASSERT
            Assert.IsFalse(isAtEnd);
        }

        #endregion

        #region NextCharacter

        [TestMethod]
        public void NextCharacter_ReturnsSecondCharacter_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            char expectedCharacter = data.ToCharArray(1, 1)[0];

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualCharacter = characterBuffer.NextCharacter;

            // ASSERT
            Assert.AreEqual(expectedCharacter, actualCharacter);
        }

        #endregion

        #region PreviousCharacter

        [TestMethod]
        public void PreviousCharacter_ReturnsNullCharacter_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            const char expectedCharacter = CharacterBuffer.NullCharacter;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualCharacter = characterBuffer.PreviousCharacter;

            // ASSERT
            Assert.AreEqual(expectedCharacter, actualCharacter);
        }

        #endregion
    }
}