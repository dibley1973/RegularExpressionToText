using System;
using Entities;
using Entities.Constants;
using Utilities.Enumerations;
using Utilities.Resources;

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
        /// Gets or sets a value indicating whether this instance is using ECMA syntax.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is using ECMA syntax; otherwise, <c>false</c>.
        /// </value>
        public bool IsUsingEcmaSyntax { get; set; }

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
            : this(expression, WhitespaceBehaviour.Include)
        {
        }

        public ExpressionParser(Expression expression, WhitespaceBehaviour whitespaceBehaviour)
            : this(expression, whitespaceBehaviour, FirstCaptureNumberBehaviour.Skip)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionToText" /> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="whitespaceBehaviour">The whitespace behaviour.</param>
        /// <param name="isEcma">if set to <c>true</c> [is ECMA].</param>
        /// <param name="firstCaptureNumberBehaviour">The first capture number behaviour.</param>
        public ExpressionParser(Expression expression, WhitespaceBehaviour whitespaceBehaviour,
            FirstCaptureNumberBehaviour firstCaptureNumberBehaviour)
        {
            _expression = expression;
            IsUsingEcmaSyntax = expression.HasEcmaSyntax;
            FirstCaptureNumberBehaviour = firstCaptureNumberBehaviour;
            WhitespaceBehaviour = whitespaceBehaviour;
        }


        #endregion

        #region Methods

        ///// <summary>
        ///// Parses the expression into elements from the optional specified indexOffset.
        ///// </summary>
        ///// <param name="indexOffset">The index offset.</param>
        //public void ParseExpression(int indexOffset = 0)
        //{
        //    ParseExpression(indexOffset, FirstCaptureNumberBehaviour.Include);
        //}

        /// <summary>
        /// Parses the expression into elements from the optional specified indexOffset.
        /// </summary>
        /// <param name="indexOffset">The index offset.</param>
        /// <exception cref="System.NotImplementedException">ParseExpression is not implemented yet!</exception>
        public void ParseExpression(int indexOffset)
        {
            //Character character;
            var characterBuffer = new CharacterBuffer(Expression.Literal, indexOffset);
            IsUsingEcmaSyntax = Expression.HasEcmaSyntax;

            while (!characterBuffer.IsAtEnd)
            {
                //int indexInOriginalBuffer = characterBuffer.IndexInOriginalBuffer;
                if (WhitespaceBehaviour == WhitespaceBehaviour.Ignore)
                {
                    // TODO: Implement white space handling
                    //string whiteSpace = characterBuffer.GetWhiteSpace();
                    //if (whiteSpace.Length > 0)
                    //{
                    //    Elements.AddElement(new WhiteSpace(indexInOriginalBuffer, whiteSpace));
                    //}
                }

                if (characterBuffer.IsAtEnd) break;

                char currentCharacter = characterBuffer.CurrentCharacter;
                if (currentCharacter > SpecialCharacters.Period)
                {
                    switch (currentCharacter)
                    {
                        case SpecialCharacters.QuestionMark: // '?'
                            {
                                AddMisplacedQuantifierCharacter(characterBuffer, Expression);
                                continue;
                            }
                        case SpecialCharacters.SquareBracketOpen: // '['
                            {
                                throw new NotImplementedException();
                                //Expression.AddElement(new CharacterClass(charBuffer)); // TODO: implement
                                //continue;
                            }
                        case SpecialCharacters.BackSlash: // '\\'
                            {
                                throw new NotImplementedException();
                                //if (!SpecialCharacter.NextIsWhitespace(charBuffer))
                                //{
                                //    Backreference backreference = new Backreference();
                                //    if (!backreference.ParseExpression(charBuffer))
                                //    {
                                //        NamedClass namedClass = new NamedClass();
                                //        if (!namedClass.ParseExpression(charBuffer))
                                //        {
                                //            this.AddElement(new SpecialCharacter(charBuffer));
                                //            continue;
                                //        }
                                //        else
                                //        {
                                //            this.AddElement(namedClass);
                                //            continue;
                                //        }
                                //    }
                                //    else
                                //    {
                                //        Backreference.NeedsSecondPass = true;
                                //        if (!backreference.IsOctal)
                                //        {
                                //            this.AddElement(backreference);
                                //            continue;
                                //        }
                                //        else
                                //        {
                                //            this.AddElement(new SpecialCharacter(backreference));
                                //            continue;
                                //        }
                                //    }
                                //}
                                //else
                                //{
                                //    this.AddElement(new SpecialCharacter(charBuffer));
                                //    continue;
                                //}
                            }
                        case SpecialCharacters.SquareBracketClose: // ']'
                            {
                                break;
                            }
                        case SpecialCharacters.CircumflexAccent: // '\u005E':
                            {
                                AddSpecialCharacter(characterBuffer, Expression);
                                continue;
                                //goto Label1;
                            }
                        default:
                            {
                                //switch (currentCharacter)
                                //{
                                //    case '{':
                                //        {
                                //            character = new Character(charBuffer, true)
                                //            {
                                //                Description = string.Concat(character.Literal, " Misplaced quantifier"),
                                //                IsValid = false
                                //            };
                                //            if (character.RepeatType != Repeat.Once)
                                //            {
                                //                this.AddElement(character);
                                //                continue;
                                //            }
                                //            else
                                //            {
                                //                this.AddElement(new Character(charBuffer));
                                //                continue;
                                //            }
                                //        }
                                //    case '|':
                                //        {
                                //            SubExpression subExpression = new SubExpression(this.Clone())
                                //            {
                                //                Literal = charBuffer.Substring(0, charBuffer.CurrentIndex),
                                //                Start = charBuffer.Offset,
                                //                End = charBuffer.IndexInOriginalBuffer
                                //            };
                                //            this.alternatives.AddElement(subExpression);
                                //            charBuffer.MoveNext();
                                //            int num = charBuffer.IndexInOriginalBuffer;
                                //            charBuffer = new CharBuffer(charBuffer.GetEnd())
                                //            {
                                //                Offset = num,
                                //                IgnoreWhiteSpace = this.IgnoreWhitespace,
                                //                IsECMA = this.IsECMA
                                //            };
                                //            this.Clear();
                                //            continue;
                                //        }
                                //}
                                break;
                            }
                    }
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
                    //                            this.AddElement(new Character(charBuffer));
                    //                            continue;
                    //                        }
                    //                        else
                    //                        {
                    //                            this.AddElement(new Comment(charBuffer));
                    //                            continue;
                    //                        }
                    //                    }
                    //                case '(':
                    //                    {
                    //                        Conditional conditional = new Conditional();
                    //                        if (!conditional.ParseExpression(charBuffer))
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
                    //                            this.AddElement(group);
                    //                            continue;
                    //                        }
                    //                        else
                    //                        {
                    //                            this.AddElement(conditional);
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
                    //                        this.AddElement(character);
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
                    Expression.AddElement(new Character(characterBuffer));
                }

                // TODO: Remove once all implementation is complete.
                // This is just here to stop infinite loops while testing!
                characterBuffer.MoveToEnd();
            }

            throw new NotImplementedException("ParseExpression is not fully implemented yet! ");
        }

        /// <summary>
        /// Adds the misplaced quantifier character.
        /// </summary>
        /// <param name="characterBuffer">The character buffer.</param>
        /// <param name="expression">The expression.</param>
        private void AddMisplacedQuantifierCharacter(CharacterBuffer characterBuffer, Expression expression)
        {
            Character character = new Character(characterBuffer, true);
            var description = GetCharacterDescription(character, CharacterDescriptions.MisplacedQuantifier);
            character.SetDescription(description);
            character.SetIsValid(false);
            expression.AddElement(character);
        }

        private void AddSpecialCharacter(CharacterBuffer characterBuffer, Expression expression)
        {
            throw new NotImplementedException("AddSpecialCharacter");
            //var element = new SpecialCharacter(characterBuffer);
            //expression.AddElement(element);
        }

        /// <summary>
        /// Gets the character description.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="formatString">The format string.</param>
        /// <returns></returns>
        public string GetCharacterDescription(Character character, string formatString)
        {
            return string.Format(formatString, character.Literal);
        }

        #endregion
    }
}