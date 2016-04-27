
using System.Text.RegularExpressions;
using Elements.Enumerations;
using RegularExpressionToText.Collections;

namespace Elements
{
    public class Element
    {
        public string Literal;

        public string Description;

        public bool IsValid = true;

        public int n; // TODO: find out what this is

        public int m; // TODO: find out what this is

        public bool AsFewAsPossible;

        public bool MatchIfAbsent;

        public Repeat RepeatType;

        private Regex RepeatRegex = new Regex(
            "^\\{(?<N>\\d+),(?<M>\\d*)\\}|^\\{(?<Exact>\\d+)\\}", 
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        public int Start;

        public int End;

        public Element()
        {
            RepeatType = Repeat.Once;
        }

        public virtual TreeNode<Expression> GetNode()
        {
            TreeNode<Expression> treeNode = new TreeNode<Expression>(this.ToString());
            Element.SetNode(treeNode, this);
            return treeNode;
        }

        private void ParseBrackets(CharacterBuffer buffer)
        {
            try
            {
                Match match = RepeatRegex.Match(buffer.GetToEnd());
                if (match.Success)
                {
                    Element element = this;
                    element.Literal = string.Concat(element.Literal, match.Value);
                    buffer.Move(match.Length);
                    string value = match.Groups["N"].Value;
                    string str = match.Groups["M"].Value;
                    string value1 = match.Groups["Exact"].Value;
                    if (value == "" && value1 == "")
                    {
                        Utility.ParseError("Error parsing the quantifier!", buffer);
                        this.IsValid = false;
                    }
                    else if (value1 != "")
                    {
                        this.n = int.Parse(value1);
                        this.RepeatType = Repeat.Exact;
                    }
                    else if (value != "" && str != "")
                    {
                        this.n = int.Parse(value);
                        this.m = int.Parse(str);
                        this.RepeatType = Repeat.Between;
                        if (this.n > this.m)
                        {
                            this.IsValid = false;
                            this.Description = "N is greater than M in quantifier!";
                        }
                    }
                    else if (!(value != "") || !(str == ""))
                    {
                        Utility.ParseError("Error parsing the quantifier!", buffer);
                        this.IsValid = false;
                    }
                    else
                    {
                        this.n = int.Parse(value);
                        this.RepeatType = Repeat.AtLeast;
                    }
                }
            }
            catch
            {
                Utility.ParseError("Error parsing the quantifier", buffer);
                this.IsValid = false;
            }
        }


        public void ParseRepetitions(CharacterBuffer buffer)
        {
            int currentIndex = buffer.CurrentIndex;
            buffer.SkipWhiteSpace();
            if (buffer.IsAtEnd)
            {
                RepeatType = Repeat.Once;
                buffer.MoveTo(currentIndex);
                End = buffer.IndexInOriginalBuffer;
                return;
            }
            char current = buffer.CurrentCharacter;
            switch (current)
            {
                case '*':
                    {
                        this.RepeatType = Repeat.Any;
                        Element element = this;
                        element.Literal = string.Concat(element.Literal, buffer.CurrentCharacter);
                        buffer.MoveNext();
                        break;
                    }
                case '+':
                    {
                        this.RepeatType = Repeat.OneOrMore;
                        Element element1 = this;
                        element1.Literal = string.Concat(element1.Literal, buffer.CurrentCharacter);
                        buffer.MoveNext();
                        break;
                    }
                default:
                    {
                        if (current == '?')
                        {
                            this.RepeatType = Repeat.ZeroOrOne;
                            Element element2 = this;
                            element2.Literal = string.Concat(element2.Literal, buffer.CurrentCharacter);
                            buffer.MoveNext();
                            break;
                        }
                        else
                        {
                            if (current != '{')
                            {
                                this.RepeatType = Repeat.Once;
                                buffer.MoveTo(currentIndex);
                                this.End = buffer.IndexInOriginalBuffer;
                                return;
                            }
                            this.ParseBrackets(buffer);
                            break;
                        }
                    }
            }
            currentIndex = buffer.CurrentIndex;
            buffer.SkipWhiteSpace();
            if (buffer.IsAtEnd || buffer.CurrentCharacter != '?')
            {
                buffer.MoveTo(currentIndex);
            }
            else
            {
                this.AsFewAsPossible = true;
                Element element3 = this;
                element3.Literal = string.Concat(element3.Literal, buffer.CurrentCharacter);
                buffer.MoveNext();
            }
            this.End = buffer.IndexInOriginalBuffer;
        }

        public static void SetNode(TreeNode<Element> node, Element element)
        {
            node.Tag = element;
            if (element.IsValid)
            {
                //node. = ElementType.Character;
                //node.ImageIndex = 2;
                //node.SelectedImageIndex = 2;
                return;
            }
            //node.ForeColor = Color.Red;
            //node.ImageIndex = 1;
            //node.SelectedImageIndex = 1;
        }
    }
}
