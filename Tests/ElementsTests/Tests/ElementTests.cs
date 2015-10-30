using Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Entities.Enumerations;

namespace ElementsTests.Tests
{
    [TestClass]
    public class ElementTests
    {
        #region Description

        [TestMethod]
        public void Description_ReturnsCorrectValue_WhenSetDescriptionCalledWithValidValue()
        {
            // ARRANGE
            const string expectedDescription = "Test";
            var element = new FakeElement();

            // ACT
            element.SetDescription(expectedDescription);
            var actualDescription = element.Description;

            // ASSERT
            Assert.AreEqual(expectedDescription, actualDescription);
        }

        #endregion

        #region IsValid

        [TestMethod]
        public void IsValid_ReturnsFalse_WhenSetIsValidCalledWithFalseValue()
        {
            // ARRANGE
            const bool expectedIsValid = false;
            var element = new FakeElement();

            // ACT
            element.SetIsValid(expectedIsValid);
            var actualIsValid = element.IsValid;

            // ASSERT
            Assert.AreEqual(expectedIsValid, actualIsValid);
        }

        #endregion

        #region Literal

        [TestMethod]
        public void Literal_ReturnsCorrectValue_WhenSetLiteralCalledWithValidValue()
        {
            // ARRANGE
            const string expectedLiteral = "Test";
            var element = new FakeElement();

            // ACT
            element.SetLiteral(expectedLiteral);
            var actualLiteral = element.Literal;

            // ASSERT
            Assert.AreEqual(expectedLiteral, actualLiteral);
        }

        #endregion

        #region RepeatType

        [TestMethod]
        public void RepeatType_ReturnsRepeatTypeOnce_WhenElementIsConstructed()
        {
            // ARRANGE
            const RepeatType expectedRepeatType = RepeatType.Once;
            var element = new FakeElement();

            // ACT
            var actualRepeatType = element.RepeatType;

            // ASSERT
            Assert.AreEqual(expectedRepeatType, actualRepeatType);
        }

        #endregion

        #region SetDescription

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetDescription_ThrowsArgumentNullException_WhenSetDescriptionCalledWithNullValue()
        {
            // ARRANGE
            var element = new FakeElement();

            // ACT
            element.SetDescription(null);
        }

        #endregion

        #region SetLiteral

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SetLiteral_ThrowsArgumentNullException_WhenSetLiteralCalledWithNullValue()
        {
            // ARRANGE
            var element = new FakeElement();

            // ACT
            element.SetLiteral(null);
        }

        #endregion

        #region SetStartIndex

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SetStartIndex_ThrowsArgumentNullException_WhenSetStartIndexCalledWithNegativeValue()
        {
            // ARRANGE
            var element = new FakeElement();

            // ACT
            element.SetStartIndex(-1);
        }

        #endregion

        #region StartIndex

        [TestMethod]
        public void StartIndex_ReturnsCorrectValue_WhenSetStartIndexCalledWithPositiveValue()
        {
            // ARRANGE
            var element = new FakeElement();
            const int expectedStartIndex = 3;

            // ACT
            element.SetStartIndex(expectedStartIndex);
            int actualStartIndex = element.StartIndex;

            // ASSERT
            Assert.AreEqual(expectedStartIndex, actualStartIndex);
        }

        #endregion
    }
}