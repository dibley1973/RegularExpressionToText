using System;
using Entities;
using Entities.Constants;
using Utilities.Enumerations;

namespace Utilities
{
    /// <summary>
    /// Responsible for parsing the expression data into the expression elements
    /// </summary>
    public class ExpressionParser
    {
        #region Fields

        private readonly Expression _expression;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the expression.
        /// </summary>
        /// <value>
        /// The expression.
        /// </value>
        public Expression Expression
        {
            get { return _expression; }
        }

        /// <summary>
        /// Gets the first capture number behaviour.
        /// </summary>
        /// <value>
        /// The first capture number behaviour.
        /// </value>
        public FirstCaptureNumberBehaviour FirstCaptureNumberBehaviour { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is ECMA.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is ECMA; otherwise, <c>false</c>.
        /// </value>
        public bool IsEcma { get; set; }

        /// <summary>
        /// Gets or sets the whitespace behaviour.
        /// </summary>
        /// <value>
        /// The whitespace behaviour.
        /// </value>
        public WhitespaceBehaviour WhitespaceBehaviour { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionToText"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public ExpressionParser(Expression expression)
            : this(expression, WhitespaceBehaviour.Include, false)
        {
        }

        public ExpressionParser(Expression expression, WhitespaceBehaviour  whitespaceBehaviour, bool isEcma)
            : this(expression, whitespaceBehaviour, isEcma, FirstCaptureNumberBehaviour.Skip)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionToText"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        public ExpressionParser(Expression expression, WhitespaceBehaviour whitespaceBehaviour, bool isEcma, FirstCaptureNumberBehaviour firstCaptureNumberBehaviour)
        {
            _expression = expression;
            WhitespaceBehaviour = whitespaceBehaviour;
        }


        #endregion

        #region Methods

        /// <summary>
        /// Parses the expression into elements from the optional specified indexOffset.
        /// </summary>
        /// <param name="indexOffset">The index offset.</param>
        public void Parse(int indexOffset = 0)
        {
            Parse(indexOffset, false);
        }

        /// <summary>
        /// Parses the expression into elements from the optional specified indexOffset.
        /// </summary>
        /// <param name="indexOffset">The index offset.</param>
        /// <param name="skipFirstCaptureNumber">if set to <c>true</c> [skip first capture number].</param>
        /// <exception cref="System.NotImplementedException">Parse is not implemented yet! </exception>
        public void Parse(int indexOffset, bool skipFirstCaptureNumber)
        {
            //Character character;
            var characterBuffer = new CharacterBuffer(
                Expression.Literal, indexOffset);
            //offset,
            //IgnoreWhitespace,
            //HasEcmaSyntax
            //);

            //while (!characterBuffer.IsAtEnd)
            {
                //int indexInOriginalBuffer = characterBuffer.IndexInOriginalBuffer;
                if (WhitespaceBehaviour == WhitespaceBehaviour.Ignore)
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

            throw new NotImplementedException("Parse is not implemented yet! ");
        }

        #endregion
    }
}