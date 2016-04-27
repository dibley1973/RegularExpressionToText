using Elements;
using Elements.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegularExpressionToText.Collections;

namespace ExpressionParsingTests
{
    [TestClass]
    public class LiteralParsingTests
    {
        const bool OptionsEcmaScript = true;
        const bool OptionsIgnorePatternWhitespace = true;
        const int DefaultOffSet = 0;


        // A-Z{2}
        // (?<NumberOfGames>\d+)
        // (?<Person>[\w ]+)
        // \b([A-Z]{1,2}\d[A-Z]|[A-Z]{1,2}\d{1,2})\ +\d[A-Z-[CIKMOV]]{2}\b - UK postal code
        // ([A-Z]){2}
        // (?=(?:.*?[A-Z]){2})
        // ^[0-9a-zA-Z!@#$%*()_+^&]*
        // ^(?=^.{8,25}$)(?=(?:.*?[!@#$%*()_+^&}{:;?.]){1})(?=(?:.*?\d){2})(?=.*[a-z])(?=(?:.*?[A-Z]){2})[0-9a-zA-Z!@#$%*()_+^&]*$

        [TestInitialize]
        public void TestInitialise()
        {
            BackReference.NeedsSecondPass = false;
        }

        [TestMethod]
        public void BeginningOfLineOrString()
        {
            // ARRANGE
            string regex = @"^";

            const string expected = "Beginning of line or string";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);
            TreeNode<Element>[] nodes = expression.GetNodes();

            // ACT
            var actual = nodes[0].Tag.Description;

            // ASSERT
            Assert.AreEqual(1, nodes.Length);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EndOfLineOrString()
        {
            // ARRANGE
            const string regex = @"$";

            const string expected = "End of line or string";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);
            TreeNode<Element>[] nodes = expression.GetNodes();

            // ACT
            var actual = nodes[0].Tag.Description;

            // ASSERT
            Assert.AreEqual(1, nodes.Length);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BeginningAndEndOfLineOrString()
        {
            // ARRANGE
            const string regex = @"^$";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(2, actuals.Length);
            var actual1 = actuals[0].Tag;
            var actual2 = actuals[1].Tag;
            Assert.AreEqual("Beginning of line or string", actual1.Description);
            Assert.AreEqual("End of line or string", actual2.Description);
        }

        [TestMethod]
        public void BeginningLiteralAndEndOfLineOrString()
        {
            // ARRANGE
            const string regex = @"^a$";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(3, actuals.Length);
            Assert.AreEqual("Beginning of line or string", actuals[0].Tag.Description);
            Assert.AreEqual("a", actuals[1].Tag.Description);
            Assert.AreEqual("End of line or string", actuals[2].Tag.Description);
        }

        [TestMethod]
        public void AnyCharacter()
        {
            // ARRANGE
            const string regex = @".";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, actuals.Length);
            var actual = actuals[0];
            Assert.IsInstanceOfType(actual.Tag, typeof(SpecialCharacter));
            Assert.AreEqual("Any character", ((SpecialCharacter)actual.Tag).Description);
        }

        [TestMethod]
        public void AnyDigit()
        {
            // ARRANGE
            const string regex = @"\d";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, actuals.Length);
            var actual = actuals[0];
            Assert.IsInstanceOfType(actual.Tag, typeof(SpecialCharacter));
            Assert.AreEqual("Any digit", ((SpecialCharacter)actual.Tag).Description);
        }

        [TestMethod]
        public void AnyDigitInACaptureGroup()
        {
            // ARRANGE
            const string regex = @"(\d)";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, actuals.Length);
            var actual = actuals[0];
            Assert.IsInstanceOfType(actual.Tag, typeof(Group));
            Assert.AreEqual("Any digit", ((Group)actual.Tag).Content.Exp[0].Description);
        }

        [TestMethod]
        public void AnyChracterInClass()
        {
            // ARRANGE
            const string regex = @"[0-9]";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, actuals.Length);
            var actual = actuals[0].Tag;
            Assert.AreEqual("Any character in this class: [0-9]", actual.Description);
        }

        [TestMethod]
        public void UpperCaseTwoRepititions()
        {
            // ARRANGE
            const string regex = @"A-Z{2}";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(3, actuals.Length);
            Assert.AreEqual("A", actuals[0].Tag.Description);
            Assert.AreEqual("-", actuals[1].Tag.Description);
            Assert.AreEqual("Z", actuals[2].Tag.Description);
        }

        [TestMethod]
        public void NamedCaptureGroup()
        {
            // ARRANGE
            const string regex = @"(?<AnimalName>[\w ])";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, actuals.Length);
            var actual = actuals[0].Tag;
            Assert.IsInstanceOfType(actual, typeof(Group));
            Assert.AreEqual("AnimalName", ((Group)actual).Name);
        }

        [TestMethod]
        public void MatchSuffixButExcludeItFromCapture()
        {
            // ARRANGE
            const string regex = @"(?=(?:.*?[A-Z]){2})";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var actuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, actuals.Length);
            var actual = actuals[0].Tag;
            Assert.IsInstanceOfType(actual, typeof(Group));
            Assert.AreEqual(GroupType.SuffixPresent, ((Group)actuals[0].Tag).Type);
        }

        [TestMethod]
        public void TwoAlternatives()
        {
            // ARRANGE
            const string regex = @"[a-z]{1,2}\d[A-Z]|[A-Z]{1,2}\d{1,2}";
            var expression = new Expression(regex, DefaultOffSet, OptionsIgnorePatternWhitespace, OptionsEcmaScript);

            // ACT
            var topLevelActuals = expression.GetNodes();

            // ASSERT
            Assert.AreEqual(1, topLevelActuals.Length);

            var secondLevelActuals = topLevelActuals[0].Nodes;
            Assert.AreEqual(2, secondLevelActuals.ChildCount);

        }
    }



    //private void Example()
    //{
    //    string regexText = "^SomeReggieHere$";
    //    BackReference.NeedsSecondPass = false;
    //    bool optionsECMAScript = true;
    //    bool optionsIgnorePatternWhitespace = true;
    //    var exp = new Expression(regexText, 0, optionsIgnorePatternWhitespace,
    //        optionsECMAScript);
    //    if (BackReference.NeedsSecondPass)
    //    {
    //        BackReference.InitializeSecondPass();
    //        exp = new Expression(regexText, 0, optionsIgnorePatternWhitespace,
    //            optionsECMAScript);
    //    }
    //    //if (exp.Cancel)
    //    //{
    //    //    e.Cancel = true;
    //    //    return;
    //    //}
    //    var sexp =  exp.Stringify();
    //    sexp.IgnoreWhitespace = optionsIgnorePatternWhitespace;
    //    sexp.IsEcma = optionsECMAScript;
    //    var AllNodes = sexp.GetNodes();
    //    var AllNodesOK = true;
    //    TreeNode[] allNodes = AllNodes;
    //    for (int i = 0; i < (int) allNodes.Length; i++)
    //    {
    //        //if (allNodes[i].SelectedImageIndex != 2)
    //        //{
    //        //    AllNodesOK = false;
    //        //}
    //    }
    //}
}