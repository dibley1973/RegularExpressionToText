
using Elements.Enumerations;
using System.Collections;
using System.Text;
using RegularExpressionToText.Collections;

namespace Elements
{
    /// <summary>
    /// Represents a regular expression
    /// </summary>
    public class Expression : CollectionBase
    {


        private Alternatives alternatives;



        #region Properties

        public virtual Element this[int index]
        {
            get
            {
                return (Element)base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

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

        static Expression()
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="Expression"/> class from being created.
        /// </summary>
        public Expression()
        {
            this.alternatives = new Alternatives();
            this.Literal = "";

            //Alternatives = new Alternatives();
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
            : this()
        {
            Literal = buffer.GetToEnd();
            Parse();
        }

        ///// <summary>
        ///// Initializes a new instance of the <see cref="Expression"/> class.
        ///// </summary>
        ///// <param name="literal">The literal.</param>
        ///// <param name="ignoreWhitespace">if set to <c>true</c> then ignore whitespace.</param>
        ///// <param name="isEcma">if set to <c>true</c> then is ECMA.</param>
        //private Expression(string literal, bool ignoreWhitespace, bool isEcma)
        //    : this()
        //{
        //    Literal = literal;
        //    IgnoreWhitespace = ignoreWhitespace;
        //    IsEcma = isEcma;;
        //}

        public Expression(string literal, int offset, bool ignoreWhitespace, bool isEcma)
            : this()
        {
            Literal = literal;
            IgnoreWhitespace = ignoreWhitespace;
            IsEcma = isEcma; ;
            Parse(offset);
        }

        public Expression(string literal, int offset, bool ignoreWhitespace, bool isEcma, bool skipFirstCaptureNumber)
            : this()
        {
            Literal = literal;
            IgnoreWhitespace = ignoreWhitespace;
            IsEcma = isEcma; ;
            Parse(offset, skipFirstCaptureNumber);
        }


        #endregion


        public virtual void Add(Element element)
        {
            base.List.Add(element);
        }

        public TreeNode<Element>[] GetNodes()
        {
            ArrayList arrayLists = new ArrayList();
            if (this.alternatives.Count == 0)
            {
                foreach (Element element in this)
                {
                    if (!IsWhitespaceVisible && !(element.GetType().Name != "WhiteSpace"))
                    {
                        continue;
                    }
                    arrayLists.Add(element.GetNode());
                }
            }
            else
            {
                arrayLists.Add(alternatives.GetNode());
            }


            TreeNode<Element>[] treeNodeArray = new TreeNode<Element>[arrayLists.Count];
            for (int i = 0; i < arrayLists.Count; i++)
            {
                TreeNode<Element> item = (TreeNode<Element>)arrayLists[i];
                //this.Recolor(item);
                treeNodeArray[i] = item;
            }
            return treeNodeArray;
        }

        public static bool IsWhitespaceVisible { get; set; }


        public Expression Clone()
        {
            Expression expression = new Expression();
            foreach (Element list in base.List)
            {
                expression.List.Add(list);
            }
            expression.Literal = this.Literal;
            expression.IgnoreWhitespace = IgnoreWhitespace;
            expression.IsEcma = IsEcma;
            return expression;
        }

        public void Parse()
        {
            Parse(0);
        }

        public void Parse(int offset)
        {
            Parse(offset, false);
        }

        public void Parse(int offset, bool skipFirstCaptureNumber)
        {
            Character character;
            CharacterBuffer charBuffer = new CharacterBuffer(Literal)
            {
                Offset = offset,
                IgnoreWhiteSpace = IgnoreWhitespace,
                IsEcma = IsEcma
            };
        Label2:
            while (!charBuffer.IsAtEnd)
            {
                int indexInOriginalBuffer = charBuffer.IndexInOriginalBuffer;
                if (this.IgnoreWhitespace)
                {
                    string whiteSpace = charBuffer.GetWhiteSpace();
                    if (whiteSpace.Length > 0)
                    {
                        this.Add(new WhiteSpace(indexInOriginalBuffer, whiteSpace));
                    }
                }
                if (charBuffer.IsAtEnd)
                {
                    break;
                }
                char current = charBuffer.CurrentCharacter;
                if (current > '.')
                {
                    if (current == '?')
                    {
                        goto Label0;
                    }
                    switch (current)
                    {
                        case '[':
                            {
                                this.Add(new CharacterClass(charBuffer));
                                continue;
                            }
                        case '\\':
                            {
                                if (!SpecialCharacter.NextIsWhitespace(charBuffer))
                                {
                                    BackReference backReference = new BackReference();
                                    if (!backReference.Parse(charBuffer))
                                    {
                                        NamedClass namedClass = new NamedClass();
                                        if (!namedClass.Parse(charBuffer))
                                        {
                                            this.Add(new SpecialCharacter(charBuffer));
                                            continue;
                                        }
                                        else
                                        {
                                            this.Add(namedClass);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        BackReference.NeedsSecondPass = true;
                                        if (!backReference.IsOctal)
                                        {
                                            this.Add(backReference);
                                            continue;
                                        }
                                        else
                                        {
                                            this.Add(new SpecialCharacter(backReference));
                                            continue;
                                        }
                                    }
                                }
                                else
                                {
                                    this.Add(new SpecialCharacter(charBuffer));
                                    continue;
                                }
                            }
                        case ']':
                            {
                                break;
                            }
                        case '\u005E':
                            {
                                goto Label1;
                            }
                        default:
                            {
                                switch (current)
                                {
                                    case '{':
                                        {
                                            character = new Character(charBuffer, true)
                                            {
                                                //Description = string.Concat(character.Literal, " Misplaced quantifier"),
                                                IsValid = false
                                            };
                                            character.Description = string.Concat(character.Literal,
                                                " Misplaced quantifier");
                                            if (character.RepeatType != Repeat.Once)
                                            {
                                                this.Add(character);
                                                continue;
                                            }
                                            else
                                            {
                                                this.Add(new Character(charBuffer));
                                                continue;
                                            }
                                        }
                                    case '|':
                                        {
                                            SubExpression subExpression = new SubExpression(this.Clone())
                                            {
                                                Literal = charBuffer.Substring(0, charBuffer.CurrentIndex),
                                                Start = charBuffer.Offset,
                                                End = charBuffer.IndexInOriginalBuffer
                                            };
                                            this.alternatives.Add(subExpression);
                                            charBuffer.MoveNext();
                                            int num = charBuffer.IndexInOriginalBuffer;
                                            charBuffer = new CharacterBuffer(charBuffer.GetToEnd())
                                            {
                                                Offset = num,
                                                IgnoreWhiteSpace = IgnoreWhitespace,
                                                IsEcma = IsEcma
                                            };
                                            this.Clear();
                                            continue;
                                        }
                                }
                                break;
                            }
                    }
                }
                else
                {
                    switch (current)
                    {
                        case '\t':
                        case '\n':
                        case '\r':
                            {
                                goto Label1;
                            }
                        case '\v':
                        case '\f':
                            {
                                break;
                            }
                        default:
                            {
                                switch (current)
                                {
                                    case ' ':
                                    case '$':
                                    case '.':
                                        {
                                            goto Label1;
                                        }
                                    case '#':
                                        {
                                            if (!this.IgnoreWhitespace)
                                            {
                                                this.Add(new Character(charBuffer));
                                                continue;
                                            }
                                            else
                                            {
                                                this.Add(new Comment(charBuffer));
                                                continue;
                                            }
                                        }
                                    case '(':
                                        {
                                            Conditional conditional = new Conditional();
                                            if (!conditional.Parse(charBuffer))
                                            {
                                                Group group = new Group(charBuffer, skipFirstCaptureNumber);
                                                if (group.Type == GroupType.OptionsOutside)
                                                {
                                                    if (group.SetX == CheckState.Checked)
                                                    {
                                                        this.IgnoreWhitespace = true;
                                                    }
                                                    else if (group.SetX == CheckState.Unchecked)
                                                    {
                                                        this.IgnoreWhitespace = false;
                                                    }
                                                    charBuffer.IgnoreWhiteSpace = this.IgnoreWhitespace;
                                                }
                                                this.Add(group);
                                                continue;
                                            }
                                            else
                                            {
                                                this.Add(conditional);
                                                BackReference.NeedsSecondPass = true;
                                                continue;
                                            }
                                        }
                                    case ')':
                                        {
                                            character = new Character(charBuffer)
                                            {
                                                IsValid = false,
                                                Description = "Unbalanced parenthesis"
                                            };
                                            this.Add(character);
                                            continue;
                                        }
                                    case '*':
                                    case '+':
                                        {
                                            goto Label0;
                                        }
                                }
                                break;
                            }
                    }
                }
                this.Add(new Character(charBuffer));
            }
            //if (worker != null && worker.CancellationPending)
            //{
            //    this.Cancel = true;
            //    return;
            //}
            if (this.alternatives.Count != 0)
            {
                SubExpression alternative = new SubExpression(this.Clone());
                alternative.Exp.alternatives = new Alternatives();
                alternative.Start = charBuffer.Offset;
                alternative.End = charBuffer.IndexInOriginalBuffer;
                alternative.Literal = charBuffer.Substring(0, charBuffer.CurrentIndex);
                this.alternatives.Add(alternative);
                this.alternatives.Start = 0;
                this.alternatives.End = charBuffer.IndexInOriginalBuffer;
            }
            return;
        Label0:
            character = new Character(charBuffer, true)
            {
                //Description = string.Concat(character.Literal, " Misplaced quantifier"),
                IsValid = false
            };
            character.Description = string.Concat(character.Literal, " Misplaced quantifier");
            this.Add(character);
            goto Label2;
        Label1:
            this.Add(new SpecialCharacter(charBuffer));
            goto Label2;
        }

        public Expression Stringify()
        {
            if (this.alternatives.Count != 0)
            {
                return this;
            }
            Expression expression = new Expression();
            SubExpression end = null;
            bool flag = true;
            foreach (Element element in this)
            {
                string name = element.GetType().Name;
                if ((name == "Character" || name == "SpecialCharacter" || name == "Backreference" ? false : name != "NamedClass"))
                {
                    if (!flag)
                    {
                        expression.Add(end);
                        flag = true;
                    }
                    expression.Add(element);
                }
                else if (!flag)
                {
                    end.Exp.Add(element);
                    end.End = element.End;
                    Expression exp = end.Exp;
                    exp.Literal = string.Concat(exp.Literal, element.Literal);
                    SubExpression subExpression = end;
                    subExpression.Literal = string.Concat(subExpression.Literal, element.Literal);
                }
                else
                {
                    end = new SubExpression();
                    end.Exp.Add(element);
                    end.Start = element.Start;
                    end.End = element.End;
                    end.Exp.Literal = element.Literal;
                    end.Literal = element.Literal;
                    flag = false;
                }
            }
            if (!flag)
            {
                expression.Add(end);
            }
            return expression;
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            if (this.alternatives.Count <= 0)
            {
                foreach (Element list in base.List)
                {
                    if (list.GetType().Name != "SubExpression")
                    {
                        stringBuilder.Append(list.Literal);
                    }
                    else
                    {
                        stringBuilder.Append(((SubExpression)list).Exp.ToString());
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.alternatives.Count; i++)
                {
                    SubExpression item = this.alternatives[i];
                    if (i != 0)
                    {
                        stringBuilder.Append("|");
                    }
                    stringBuilder.Append(item.ToString());
                }
            }
            return stringBuilder.ToString();
        }
    }
}