
using Elements.Constants;
using System.Collections.Generic;

namespace Elements
{
    /// <summary>
    /// Represents a regular expression
    /// </summary>
    public class Expression // : CollectionBase
    {
        #region Properties

        /// <summary>
        /// Gets the elements.
        /// </summary>
        /// <value>
        /// The elements.
        /// </value>
        public List<Element> Elements { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether to ignore whitespace or not.
        /// </summary>
        /// <value>
        ///   <c>true</c> if to ignore whitespace; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreWhitespace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ECMA.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ECMA; otherwise, <c>false</c>.
        /// </value>
        public bool IsEcma { get; set; }

        /// <summary>
        /// The literal representation of the regular Expression.
        /// </summary>
        /// <value>
        /// The literal.
        /// </value>
        public string Literal { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Prevents a default instance of the <see cref="Expression"/> class from being created.
        /// </summary>
        private Expression()
        {
            //Alternatives = new Alternatives();
            Elements = new List<Element>();
            IgnoreWhitespace = false;
            IsEcma = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="literal">The literal.</param>
        public Expression(string literal)
            : this()
        {
            Literal = literal;
            Parse();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public Expression(CharacterBuffer buffer)
        {
            Literal = buffer.GetToEnd();
            Parse();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="literal">The literal.</param>
        /// <param name="ignoreWhitespace">if set to <c>true</c> then ignore whitespace.</param>
        /// <param name="isEcma">if set to <c>true</c> then is ECMA.</param>
        public Expression(string literal, bool ignoreWhitespace, bool isEcma)
            : this(literal)
        {

            IgnoreWhitespace = ignoreWhitespace;
            IsEcma = isEcma;
        }

        public Expression(string literal, int offset, bool ignoreWhitespace, bool isEcma)
            : this(literal, ignoreWhitespace, isEcma)
        {
            Parse(offset);
        }

        public Expression(string literal, int offset, bool ignoreWhitespace, bool isEcma, bool skipFirstCaptureNumber)
            : this(literal, ignoreWhitespace, isEcma)
        {
            Parse(offset, skipFirstCaptureNumber);
        }


        #endregion

        #region Methods

        /// <summary>
        /// Parses the expression from the optional specified indexOffset.
        /// </summary>
        /// <param name="indexOffset">The index offset.</param>
        public void Parse(int indexOffset = 0)
        {
            Parse(indexOffset, false);
        }

        public void Parse(int indexOffset, bool skipFirstCaptureNumber)
        {
            //Character character;
            var characterBuffer = new CharacterBuffer(
                Literal, indexOffset);
            //offset,
            //IgnoreWhitespace,
            //IsEcma
            //);

            //while (!characterBuffer.IsAtEnd)
            {
                //int indexInOriginalBuffer = characterBuffer.IndexInOriginalBuffer;
                if (IgnoreWhitespace)
                {
                    // TODO: Implement white space handling
                    //string whiteSpace = characterBuffer.GetWhiteSpace();
                    //if (whiteSpace.Length > 0)
                    //{
                    //    Elements.Add(new WhiteSpace(indexInOriginalBuffer, whiteSpace));
                    //}
                }

                //if (characterBuffer.IsAtEnd) break;

                char currentCharacter = characterBuffer.CurrentCharacter;
                if (currentCharacter > SpecialCharacters.Period)
                {
                    //if (current == '?')
                    //{
                    //    goto Label0;
                    //}
                    //switch (current)
                    //{
                    //    case '[':
                    //        {
                    //            this.Add(new CharacterClass(charBuffer));
                    //            continue;
                    //        }
                    //    case '\\':
                    //        {
                    //            if (!SpecialCharacter.NextIsWhitespace(charBuffer))
                    //            {
                    //                Backreference backreference = new Backreference();
                    //                if (!backreference.Parse(charBuffer))
                    //                {
                    //                    NamedClass namedClass = new NamedClass();
                    //                    if (!namedClass.Parse(charBuffer))
                    //                    {
                    //                        this.Add(new SpecialCharacter(charBuffer));
                    //                        continue;
                    //                    }
                    //                    else
                    //                    {
                    //                        this.Add(namedClass);
                    //                        continue;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    Backreference.NeedsSecondPass = true;
                    //                    if (!backreference.IsOctal)
                    //                    {
                    //                        this.Add(backreference);
                    //                        continue;
                    //                    }
                    //                    else
                    //                    {
                    //                        this.Add(new SpecialCharacter(backreference));
                    //                        continue;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                this.Add(new SpecialCharacter(charBuffer));
                    //                continue;
                    //            }
                    //        }
                    //    case ']':
                    //        {
                    //            break;
                    //        }
                    //    case '\u005E':
                    //        {
                    //            goto Label1;
                    //        }
                    //    default:
                    //        {
                    //            switch (current)
                    //            {
                    //                case '{':
                    //                    {
                    //                        character = new Character(charBuffer, true)
                    //                        {
                    //                            Description = string.Concat(character.Literal, " Misplaced quantifier"),
                    //                            IsValid = false
                    //                        };
                    //                        if (character.RepeatType != Repeat.Once)
                    //                        {
                    //                            this.Add(character);
                    //                            continue;
                    //                        }
                    //                        else
                    //                        {
                    //                            this.Add(new Character(charBuffer));
                    //                            continue;
                    //                        }
                    //                    }
                    //                case '|':
                    //                    {
                    //                        SubExpression subExpression = new SubExpression(this.Clone())
                    //                        {
                    //                            Literal = charBuffer.Substring(0, charBuffer.CurrentIndex),
                    //                            Start = charBuffer.Offset,
                    //                            End = charBuffer.IndexInOriginalBuffer
                    //                        };
                    //                        this.alternatives.Add(subExpression);
                    //                        charBuffer.MoveNext();
                    //                        int num = charBuffer.IndexInOriginalBuffer;
                    //                        charBuffer = new CharBuffer(charBuffer.GetEnd())
                    //                        {
                    //                            Offset = num,
                    //                            IgnoreWhiteSpace = this.IgnoreWhitespace,
                    //                            IsECMA = this.IsECMA
                    //                        };
                    //                        this.Clear();
                    //                        continue;
                    //                    }
                    //            }
                    //            break;
                    //        }
                    //}
                }
                else
                {
                    //switch (current)
                    //{
                    //    case '\t':
                    //    case '\n':
                    //    case '\r':
                    //        {
                    //            goto Label1;
                    //        }
                    //    case '\v':
                    //    case '\f':
                    //        {
                    //            break;
                    //        }
                    //    default:
                    //        {
                    //            switch (current)
                    //            {
                    //                case ' ':
                    //                case '$':
                    //                case '.':
                    //                    {
                    //                        goto Label1;
                    //                    }
                    //                case '#':
                    //                    {
                    //                        if (!this.IgnoreWhitespace)
                    //                        {
                    //                            this.Add(new Character(charBuffer));
                    //                            continue;
                    //                        }
                    //                        else
                    //                        {
                    //                            this.Add(new Comment(charBuffer));
                    //                            continue;
                    //                        }
                    //                    }
                    //                case '(':
                    //                    {
                    //                        Conditional conditional = new Conditional();
                    //                        if (!conditional.Parse(charBuffer))
                    //                        {
                    //                            Group group = new Group(charBuffer, SkipFirstCaptureNumber);
                    //                            if (group.Type == GroupType.OptionsOutside)
                    //                            {
                    //                                if (group.SetX == CheckState.Checked)
                    //                                {
                    //                                    this.IgnoreWhitespace = true;
                    //                                }
                    //                                else if (group.SetX == CheckState.Unchecked)
                    //                                {
                    //                                    this.IgnoreWhitespace = false;
                    //                                }
                    //                                charBuffer.IgnoreWhiteSpace = this.IgnoreWhitespace;
                    //                            }
                    //                            this.Add(group);
                    //                            continue;
                    //                        }
                    //                        else
                    //                        {
                    //                            this.Add(conditional);
                    //                            Backreference.NeedsSecondPass = true;
                    //                            continue;
                    //                        }
                    //                    }
                    //                case ')':
                    //                    {
                    //                        character = new Character(charBuffer)
                    //                        {
                    //                            IsValid = false,
                    //                            Description = "Unbalanced parenthesis"
                    //                        };
                    //                        this.Add(character);
                    //                        continue;
                    //                    }
                    //                case '*':
                    //                case '+':
                    //                    {
                    //                        goto Label0;
                    //                    }
                    //            }
                    //            break;
                    //        }
                    //}
                }
                //Elements.Add(new Character(characterBuffer));
            }

        }

        #endregion
    }
}