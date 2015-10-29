using Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ElementsTests.Tests
{
    [TestClass]
    public class CharacterBufferTests
    {
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

        #region DataLength

        [TestMethod]
        public void DataLength_ReturnsCorrectValue_WhenClassConstructedWithData()
        {
            // ARRANGE
            const string data = Fakes.Literal.BasicLiteral;

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualDataLength = characterBuffer.DataLength;

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

            // ACT
            var characterBuffer = new CharacterBuffer(data);
            var actualData = characterBuffer.GetToEnd();
            var actualDataLength = actualData.Length;

            // ASSERT
            Assert.AreEqual(data.Length, actualDataLength);
        }

        #endregion
    }
}