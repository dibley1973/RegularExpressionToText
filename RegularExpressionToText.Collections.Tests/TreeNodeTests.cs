using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RegularExpressionToText.Collections.Tests
{
    [TestClass]
    public class TreeNodeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenInstantiatedWithNullText_ThrowsException()
        {
            // ACT
            var treeNode = new TreeNode<byte>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_WhenInstantiatedWithemptyText_ThrowsException()
        {
            // ACT
            var treeNode = new TreeNode<byte>("");
        }

        [TestMethod]
        public void Nodes_AfterInstantiation_ReturnsEmptyList()
        {
            // ARRANGE
            var treeNode = new TreeNode<byte>();

            // ACT
            var actual = treeNode.Nodes;

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.ChildCount);
        }

        [TestMethod]
        public void Tag_AfterInstantiation_ReturnsNull()
        {
            // ARRANGE
            var treeNode = new TreeNode<string>();

            // ACT
            var actual = treeNode.Tag;

            // ASSERT
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Tag_AfterSetting_ReturnsSetObject()
        {
            // ARRANGE
            byte[] expected = { 1, 2, 3 };
            var treeNode = new TreeNode<byte[]> { Tag = expected };

            // ACT
            var actual = treeNode.Tag;

            // ASSERT
            Assert.IsNotNull(actual);
            Assert.AreSame(expected, actual);
        }

        [TestMethod]
        public void Text_AfterInstantiationWithoutText_ReturnsEmptyString()
        {
            // ARRANGE
            var treeNode = new TreeNode<byte>();

            // ACT
            var actual = treeNode.Text;

            // ASSERT
            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void Text_AfterInstantiationWithText_ReturnsInstantiatedValue()
        {
            // ARRANGE
            const string expected = "Yo!";
            var treeNode = new TreeNode<byte>(expected);

            // ACT
            var actual = treeNode.Text;

            // ASSERT
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Text_AfterSetting_ReturnsSetValue()
        {
            // ARRANGE
            const string expected = "Yo!";
            var treeNode = new TreeNode<byte>(expected)
            {
                Text = expected
            };

            // ACT
            var actual = treeNode.Text;

            // ASSERT
            Assert.AreEqual(expected, actual);
        }
    }
}