﻿using Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            // ReSharper disable once ObjectCreationAsStatement
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

        [TestMethod]
        public void CurrentCharacter_ReturnsNullCharacterOfData_FaterMoveToEndIsCalled()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            const char expectedCharacter = CharacterBuffer.NullCharacter;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();
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
            var actualCurrentPosition = characterBuffer.CurrentIndex;

            // ASSERT
            Assert.AreEqual(expectedCurrentPosition, actualCurrentPosition);
        }

        [TestMethod]
        public void CurrentIndexPosition_ReturnsLastPosition_WhenClassConstructedWithDataAndMoveToEndCalled()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            int expectedIndexPosition = data.Length;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();
            var actualIndexPosition = characterBuffer.CurrentIndex;

            // ASSERT
            Assert.AreEqual(expectedIndexPosition, actualIndexPosition);
        }

        [TestMethod]
        public void CurrentIndexPosition_ReturnsZero_WhenClassConstructedWithDataAndMoveToStartCalledAfterMoveToEnd()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            const int expectedCurrentPosition = 0;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();
            characterBuffer.MoveToStart();
            var actualCurrentPosition = characterBuffer.CurrentIndex;

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

        [TestMethod]
        public void GetToEnd_ReturnsEmptyString_AfterMoveToEndIsCalled()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
            string expectedCharachter = string.Empty;

            // ACT
            characterBuffer.MoveToEnd();
            string actualData = characterBuffer.GetToEnd();
            
            // ASSERT
            Assert.AreEqual(actualData, expectedCharachter);
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
            var isAtBeginning = characterBuffer.IsAtStart;

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


        [TestMethod]
        public void NextCharacter_ReturnsNullCharacter_AfterMoveToEndIsCalled()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
            const char expectedCharacter = CharacterBuffer.NullCharacter;

            // ACT
            characterBuffer.MoveToEnd();
            var actualCharacter = characterBuffer.NextCharacter;

            // ASSERT
            Assert.AreEqual(actualCharacter, expectedCharacter);
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

        [TestMethod]
        public void PreviousCharacter_ReturnslastCharacter_AfterMoveToEnd()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            char expectedCharacter = data.ToCharArray(data.Length - 1, 1)[0];

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();
            var actualCharacter = characterBuffer.PreviousCharacter;

            // ASSERT
            Assert.AreEqual(expectedCharacter, actualCharacter);
        }

        #endregion

        #region MoveNext

        [TestMethod]
        public void MoveNext_ReturnsTrue_WhenIndexIsNotAtEnd()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);

            // ACT
            var result = characterBuffer.MoveNext();

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MoveNext_ReturnsFalse_WhenIndexIsAtEnd()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();

            // ACT
            var result = characterBuffer.MoveNext();

            // ASSERT
            Assert.IsFalse(result);
        }

        #endregion

        #region MovePrevious

        [TestMethod]
        public void MovePrevious_ReturnsTrue_WhenIndexIsNotAtStart()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);
            characterBuffer.MoveToEnd();

            // ACT
            var result = characterBuffer.MovePrevious();

            // ASSERT
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MovePrevious_ReturnsFalse_WhenIndexIsAtStart()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);

            // ACT
            var result = characterBuffer.MovePrevious();

            // ASSERT
            Assert.IsFalse(result);
        }

        #endregion


        #region MoveToEnd


        #endregion

        #region MoveToStart


        #endregion

        #region Substring

        [TestMethod]
        public void Substring_ReturnsCorrectPortionOfData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            int expectedStartIndex = 1;
            int expectedLength = 1;
            string expected = data.Substring(expectedStartIndex, expectedLength);
            var characterBuffer = new CharacterBuffer(data);

            // ACT
            var actual = characterBuffer.Substring(expectedStartIndex, expectedLength);

            // ASSERT
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Substring_ArgumentOutOfRangeExceptionThrown_WhenStartIndexIsLessThanZero()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            const int expectedStartIndex = -1;
            const int expectedLength = 1;
            var characterBuffer = new CharacterBuffer(data);

            // ACT
            characterBuffer.Substring(expectedStartIndex, expectedLength);

            // ASSERT
            // Exception thrown by now
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Substring_ArgumentOutOfRangeExceptionThrown_WhenStartIndexPlusLengthIsGreaterThanLength()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            const int expectedStartIndex = 1;
            const int expectedLength = 10;
            var characterBuffer = new CharacterBuffer(data);

            // ACT
            characterBuffer.Substring(expectedStartIndex, expectedLength);

            // ASSERT
            // Exception thrown by now
        }


        #endregion

        #region ToString

        [TestMethod]
        public void ToString_ReturnsSameAsOriginalData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;
            var characterBuffer = new CharacterBuffer(data);

            // ACT
            var actual = characterBuffer.ToString();

            // ASSERT
            Assert.AreEqual(data, actual);
        }

        #endregion

    }
}