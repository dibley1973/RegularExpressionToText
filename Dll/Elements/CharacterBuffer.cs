using Elements.Constants;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Elements
{
    public class CharacterBuffer
    {
        #region Fields

        public const char NullCharacter = '\0';

        private string _data;
        //private char[] _data;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current character.
        /// </summary>
        /// <value>
        /// The current character.
        /// </value>
        public char CurrentCharacter
        {
            get
            {
                return Index == Length
                    ? NullCharacter
                    : _data[Index];
            }
        }

        /// <summary>
        /// Gets the current index postition.
        /// </summary>
        /// <value>
        /// The current poistion of the index.
        /// </value>
        public int CurrentIndex
        {
            get { return Index; }
        }

        /// <summary>
        /// Gets or sets the underlying data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        //private char[] Data
        //{
        //    get { return _data; }
        //    //set { _data = value; }
        //}

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; private set; }

        private int Index { get; set; }

        public bool IgnoreWhiteSpace = true;

        public bool IsEcma;

        /// <summary>
        /// Gets a value indicating whether this instance is at start.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is at start; otherwise, <c>false</c>.
        /// </value>
        public bool IsAtStart
        {
            get { return Index == 0; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is at end.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is at end; otherwise, <c>false</c>.
        /// </value>
        public bool IsAtEnd
        {
            get
            {
                return Index == Length;
            }
        }

        public int IndexInOriginalBuffer
        {
            get
            {
                return Index + Offset;
            }
        }

        /// <summary>
        /// Gets the next character.
        /// </summary>
        /// <value>
        /// The next character.
        /// </value>
        public char NextCharacter
        {
            get
            {
                return IsAtEnd
                    ? NullCharacter
                    : _data[Index + 1];
            }
        }

        public int Offset;

        /// <summary>
        /// Gets the previous character.
        /// </summary>
        /// <value>
        /// The previous character.
        /// </value>
        public char PreviousCharacter
        {
            get
            {
                return IsAtStart
                    ? NullCharacter
                    : _data[Index - 1];
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterBuffer"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentNullException">data</exception>
        public CharacterBuffer(string data)
        {
            if (string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");

            SetData(data);
            SetDataLength(data);
            SetIndex(0);
        }

        #endregion

        #region Methods

        public List<int> FindNakedPipes()
        {
            int num = 0;
            bool isEscaped = false;
            List<int> nums = new List<int>();
            while (!IsAtEnd)
            {
                if (!isEscaped && CurrentCharacter == Characters.BracketOpen)
                {
                    num++;
                }
                else if (!isEscaped && CurrentCharacter == Characters.BracketClosed)
                {
                    num--;
                }
                if (num == 0 && !isEscaped && CurrentCharacter == Characters.Pipe)
                {
                    nums.Add(CurrentIndex);
                }
                isEscaped = (CurrentCharacter == Characters.BackSlash && !isEscaped);
                MoveNext();
            }
            return nums;
        }

        internal ParsedCharacterClass GetParsedCharacterClass()
        {
            ParsedCharacterClass parsedCharacterClass = new ParsedCharacterClass();
            if (CurrentCharacter != '[')
            {
                parsedCharacterClass.ErrorMessage = "Parsing Error - Character Class did not start with [";
                return parsedCharacterClass;
            }
            parsedCharacterClass.LeftBracketIndex[0] = CurrentIndex;
            parsedCharacterClass.Count = 1;
            int currentIndex = CurrentIndex;
            bool flag = false;
            bool flag1 = false;
            while (!IsAtEnd)
            {
                MoveNext();
                if (flag1 && CurrentCharacter == '[')
                {
                    currentIndex = CurrentIndex;
                    parsedCharacterClass.LeftBracketIndex[parsedCharacterClass.Count] = currentIndex;
                    ParsedCharacterClass count = parsedCharacterClass;
                    count.Count = count.Count + 1;
                    if (parsedCharacterClass.Count > 30)
                    {
                        parsedCharacterClass.ErrorMessage = "The Analyzer cannot handle character class subtraction with depth >30";
                        parsedCharacterClass.Count = 0;
                        return parsedCharacterClass;
                    }
                }
                flag1 = (flag || CurrentCharacter != '-' ? false : true);
                if (flag || CurrentCharacter != ']')
                {
                    flag = (CurrentCharacter != '\\' ? false : !flag);
                }
                else
                {
                    if (CurrentIndex == currentIndex + 1)
                    {
                        continue;
                    }
                    if (CurrentIndex != currentIndex + 2 || Previous != '\u005E')
                    {
                        break;
                    }
                }
            }
            if (IsAtEnd)
            {
                parsedCharacterClass.Count = 0;
                parsedCharacterClass.ErrorMessage = "Unmatched bracket in character class";
                return parsedCharacterClass;
            }
            for (int i = 0; i < parsedCharacterClass.Count - 1; i++)
            {
                MoveNext();
                if (IsAtEnd)
                {
                    parsedCharacterClass.ErrorMessage = "Unmatched bracket in character class";
                    parsedCharacterClass.Count = 0;
                    return parsedCharacterClass;
                }
                if (CurrentCharacter != ']')
                {
                    parsedCharacterClass.ErrorMessage = "Syntax error in character class subtraction";
                    parsedCharacterClass.Count = 0;
                    return parsedCharacterClass;
                }
            }
            MoveNext();
            return parsedCharacterClass;
        }

        public string GetStringToMatchingParenthesis()
        {
            int currentIndex;
            if (CurrentCharacter != '(')
            {
                return "";
            }
            int num = CurrentIndex;
            int num1 = 1;
            bool flag = false;
            while (!IsAtEnd && num1 != 0)
            {
                MoveNext();
                if (!flag && CurrentCharacter == '[')
                {
                    int currentIndex1 = CurrentIndex;
                    if (GetParsedCharacterClass().Count == 0)
                    {
                        MoveTo(currentIndex1 + 1);
                    }
                }
                if (!flag && CurrentCharacter == '(')
                {
                    num1++;
                }
                else if (!flag && CurrentCharacter == ')')
                {
                    num1--;
                }
                flag = (CurrentCharacter != '\\' ? false : !flag);
            }
            if (!IsAtEnd)
            {
                currentIndex = CurrentIndex;
                return Substring(num, currentIndex - num + 1);
            }
            MovePrevious();
            currentIndex = CurrentIndex;
            return Substring(num, currentIndex - num + 1);
        }

        /// <summary>
        /// Gets the buffer from the current position to the end.
        /// </summary>
        /// <returns></returns>
        public string GetToEnd()
        {
            return IsAtEnd
                ? string.Empty
                : _data.Substring(Index);
            //return IsAtEnd
            //    ? string.Empty
            //    : new string(Data, Index, Length - Index);
        }

        public string GetWhiteSpace()
        {
            Match match = !IsEcma
                ? WhiteSpace.FindWhiteSpace.Match(_data.ToString(), Index)
                : WhiteSpace.FindEcmaWhiteSpace.Match(_data.ToString(), Index);

            if (!match.Success)
            {
                return "";
            }

            Move(match.Value.Length);
            return match.Value;
        }

        /// <summary>
        /// Moves the next.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            //if (IsAtEnd) return false;

            //SetIndex(Index + 1);
            //return true;

            if (Index == Length)
            {
                return false;
            }
            CharacterBuffer charBuffer = this;
            int index = charBuffer.Index;
            int num = index;
            charBuffer.Index = index + 1;
            if (num == Length)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Moves the previous.
        /// </summary>
        /// <returns></returns>
        public bool MovePrevious()
        {
            //    if (IsAtStart) return false;

            //    SetIndex(Index - 1);
            //    return true;

            if (Index == 0)
            {
                return false;
            }
            CharacterBuffer index = this;
            index.Index = index.Index - 1;
            return true;
        }

        public bool Move(int n)
        {
            if (n < 0)
            {
                CharacterBuffer index = this;
                index.Index = index.Index + n;
                if (Index > 0)
                {
                    return true;
                }
                Index = 0;
                return false;
            }
            CharacterBuffer charBuffer = this;
            charBuffer.Index = charBuffer.Index + n;
            if (Index < Length)
            {
                return true;
            }
            Index = Length;
            return false;
        }

        /// <summary>
        /// Moves to start.
        /// </summary>
        public void MoveToStart()
        {
            Index = 0;
        }

        /// <summary>
        /// Moves to end.
        /// </summary>
        public void MoveToEnd()
        {
            Index = Length;
        }

        public bool MoveTo(int index)
        {
            return Move(index - Index);
        }

        public char Next
        {
            get
            {
                if (Index >= Length - 1)
                {
                    return NullCharacter;
                }
                //return _data.ToCharArray(Index + 1, 1)[0];
                return _data[Index + 1];
            }
        }

        public char Previous
        {
            get
            {
                return Index < 1
                    ? NullCharacter
                    : _data[Index];
                //return _data.ToCharArray(Index - 1, 1)[0];
            }
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The data.</param>
        private void SetData(string data)
        {
            //_data = data.ToCharArray();
            _data = data;
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name="data">The data.</param>
        private void SetDataLength(string data)
        {
            Length = data.Length;
        }

        /// <summary>
        /// Sets the index.
        /// </summary>
        /// <param name="index">The index.</param>
        private void SetIndex(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");

            Index = index;
        }

        public void SkipWhiteSpace()
        {
            if (!IgnoreWhiteSpace)
            {
                return;
            }
            GetWhiteSpace();
        }


        public string Snapshot()
        {
            if (IsAtEnd)
            {
                return "";
            }

            int num = 10;
            string str1 = "";
            string str2 = "";
            int index = Index - num;
            if (index < 0)
            {
                index = 0;
            }
            string str3 = _data.Substring(index, Index - index);
            //string str3 = new string(_data, index, Index - index);
            int length = Index + num;
            if (length > Length - 1)
            {
                length = Length - 1;
            }
            //string str4 = string.Concat(str1, _data.Substring(Index, 1), str2);
            string str4 = string.Concat(str1, Substring(Index, 1), str2);

            //string str = (Index != Length - 1 ? _data.Substring(Index + 1, length - Index - 1) : "");
            string str = (Index != Length - 1 ? Substring(Index + 1, length - Index - 1) : "");
            return string.Concat(str3, str4, str);
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at
        /// a specified character position and has a specified length.
        /// </summary>
        /// <param name="startIndex">
        /// The zero-based starting character position of a substring in this instance.</param>
        /// <param name="length">
        /// The number of characters in the substring.</param>
        /// <returns>
        /// A string that is equivalent to the substring of length length that begins
        /// at startIndex in this instance, or System.String.Empty if startIndex is
        /// equal to the length of this instance and length is zero.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex;startIndex must be zero or greater
        /// or
        /// length;The length when added to the startIndex must be less than the length of the buffer
        /// </exception>
        public string Substring(int startIndex, int length)
        {
            return _data.Substring(startIndex, length);

            //if (startIndex < 0) throw new ArgumentOutOfRangeException("startIndex", startIndex, "startIndex must be zero or greater");
            //if (startIndex + length > Length) throw new ArgumentOutOfRangeException("length", length, "The length when added to the startIndex must be less than the length of the buffer");

            //return new string(Data, startIndex, length);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _data.ToString();
        }

        #endregion


    }
}