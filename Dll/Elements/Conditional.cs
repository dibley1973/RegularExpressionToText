
using Elements.Enumerations;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Elements
{
    public class Conditional : Element
    {
        public Regex ConditionalRegex = new Regex("^\\(\\?(?<Expression>\\((?>(\\\\\\(|\\\\\\)|[^()])+|\\((?<depth>)|\\)(?<-depth>))*(?(depth)(?!))\\))(?<Contents>.*)\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);

        public Regex AlphanumericRegex = new Regex("^\\w+$", RegexOptions.Compiled);

        private Conditional.ExpType expType;

        public SubExpression Exp;

        public SubExpression Yes;

        public SubExpression No;

        public string Name;


        public bool Parse(CharacterBuffer buffer)
        {
            int num;
            this.Start = buffer.IndexInOriginalBuffer;
            if (buffer.IsAtEnd)
            {
                Utility.ParseError("Reached end of buffer looking for a conditional!", buffer);
                this.IsValid = false;
                return false;
            }
            this.Literal = buffer.GetStringToMatchingParenthesis();
            Match match = this.ConditionalRegex.Match(this.Literal);
            if (!match.Success)
            {
                buffer.Move(1 - this.Literal.Length);
                return false;
            }
            var item = match.Groups["Expression"];
            int start = this.Start + item.Index;
            string str = item.Value.Substring(1, item.Value.Length - 2);
            this.Name = str;
            this.expType = Conditional.ExpType.Expression;
            this.Exp = new SubExpression(item.Value, start, buffer.IgnoreWhiteSpace, buffer.IsEcma, true)
            {
                Start = start,
                End = this.Exp.Start + item.Length
            };
            if (this.Exp.Exp[0] is Group)
            {
                Group group = (Group)this.Exp.Exp[0];
                switch (group.Type)
                {
                    case GroupType.Balancing:
                        {
                            group.Description = "Test condition cannot be a balancing group";
                            group.IsValid = false;
                            this.IsValid = false;
                            break;
                        }
                    case GroupType.Named:
                        {
                            group.Description = "Test condition cannot be a named group";
                            group.IsValid = false;
                            this.IsValid = false;
                            break;
                        }
                    case GroupType.Numbered:
                        {
                            if (Backreference.ContainsName(str))
                            {
                                this.expType = Conditional.ExpType.NamedCapture;
                                break;
                            }
                            else if (int.TryParse(str, out num))
                            {
                                this.expType = Conditional.ExpType.NumberedCapture;
                                if (Backreference.ContainsNumber(str))
                                {
                                    break;
                                }
                                this.expType = Conditional.ExpType.NonExistentNumber;
                                group.IsValid = false;
                                break;
                            }
                            else if (!this.AlphanumericRegex.Match(str).Success)
                            {
                                group.Description = "Match if prefix is present";
                                break;
                            }
                            else if (!char.IsDigit(str[0]))
                            {
                                this.expType = Conditional.ExpType.NonExistentName;
                                break;
                            }
                            else
                            {
                                this.expType = Conditional.ExpType.InvalidName;
                                group.IsValid = false;
                                break;
                            }
                        }
                    case GroupType.Noncapturing:
                    case GroupType.Greedy:
                    case GroupType.OptionsInside:
                    case GroupType.OptionsOutside:
                        {
                            group.Description = "Test condition is";
                            break;
                        }
                    case GroupType.SuffixPresent:
                        {
                            group.Description = "Match if suffix is present";
                            break;
                        }
                    case GroupType.PrefixPresent:
                        {
                            group.Description = "Match if prefix is present";
                            break;
                        }
                    case GroupType.SuffixAbsent:
                        {
                            group.Description = "Match if suffix is absent";
                            break;
                        }
                    case GroupType.PrefixAbsent:
                        {
                            group.Description = "Match if prefix is absent";
                            break;
                        }
                    case GroupType.Comment:
                        {
                            group.Description = "Test condition cannot be a comment!";
                            group.IsValid = false;
                            this.IsValid = false;
                            break;
                        }
                    default:
                        {
                            goto case GroupType.OptionsOutside;
                        }
                }
                group.Description = string.Concat(group.Description, " ", group.Literal);
            }
            else
            {
                this.expType = Conditional.ExpType.NotAGroup;
            }
            item = match.Groups["Contents"];
            string value = item.Value;
            List<int> nums = (new CharacterBuffer(value)).FindNakedPipes();
            start = this.Start + item.Index;
            switch (nums.Count)
            {
                case 0:
                    {
                        this.Yes = new SubExpression(value, start, buffer.IgnoreWhiteSpace, buffer.IsEcma)
                        {
                            Start = start,
                            End = this.Yes.Start + item.Length
                        };
                        this.Description = "Conditional Expression with \"Yes\" clause only";
                        break;
                    }
                case 1:
                    {
                        int item1 = nums[0] + 1;
                        this.Yes = new SubExpression(value.Substring(0, item1 - 1), start, buffer.IgnoreWhiteSpace, buffer.IsEcma)
                        {
                            Start = start,
                            End = this.Yes.Start + item1 - 1
                        };
                        start = this.Yes.Start + item1;
                        this.No = new SubExpression(value.Substring(item1), start, buffer.IgnoreWhiteSpace, buffer.IsEcma)
                        {
                            Start = start,
                            End = this.Yes.Start + item.Length
                        };
                        this.Description = "Conditional Expression with \"Yes\" and \"No\" clause";
                        break;
                    }
                default:
                    {
                        this.Yes = new SubExpression(value, start, buffer.IgnoreWhiteSpace, buffer.IsEcma)
                        {
                            Start = start,
                            End = this.Yes.Start + item.Length
                        };
                        this.Description = "Too many | symbols in a conditional expression";
                        this.IsValid = false;
                        break;
                    }
            }
            buffer.MoveNext();
            base.ParseRepetitions(buffer);
            return true;
        }

        private enum ExpType
        {
            Expression,
            NotAGroup,
            NamedCapture,
            NumberedCapture,
            NonExistentName,
            NonExistentNumber,
            InvalidName
        }
    }
}