using System;
using Entities.Constants;

namespace Entities
{
    public class CharacterBuffer
    {
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
                    ? SpecialCharacters.NullCharacter
                    : Data[Index];
            }
        }

        /// <summary>
        /// Gets the current index postition.
        /// </summary>
        /// <value>
        /// The current poistion of the index.
        /// </value>
        public int CurrentIndexPosition
        {
            get { return Index; }
        }

        /// <summary>
        /// Gets or sets the underlying data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        private char[] Data { get; set; }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        public int Length { get; private set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        private int Index { get; set; }

        /// <summary>
        /// Gets or sets the offset of the index.
        /// </summary>
        /// <value>
        /// The index offset.
        /// </value>
        private int IndexOffset { get; set; }

        /// <summary>
        /// Gets the index in original buffer.
        /// </summary>
        /// <value>
        /// The index in original buffer.
        /// </value>
        /// <returns>The Index plust the offset</returns>
        public int IndexInOriginalBuffer
        {
            get
            {
                return Index + IndexOffset;
            }
        }

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
            get { return Index == Length; }
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
                    ? SpecialCharacters.NullCharacter
                    : Data[Index + 1];
            }
        }

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
                    ? SpecialCharacters.NullCharacter
                    : Data[Index - 1];
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

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterBuffer"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="indexOffset">The index offset.</param>
        public CharacterBuffer(string data, int indexOffset)
            : this(data)
        {
            SetIndexOffset(indexOffset);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the buffer from the current position to the end.
        /// </summary>
        /// <returns></returns>
        public string GetToEnd()
        {
            return IsAtEnd
                ? string.Empty
                : new string(Data, Index, Length - Index);
        }

        /// <summary>
        /// Moves the specified move by.
        /// </summary>
        /// <param name="amount">The move by.</param>
        /// <returns></returns>
        public bool MoveBy(int amount)
        {
            int newIndex = Index + amount;
            bool validNewIndexPosition = (0 <= newIndex && newIndex < Length);

            if (!validNewIndexPosition) return false;

            SetIndex(newIndex);
            return true;
        }

        /// <summary>
        /// Moves the next.
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            if (IsAtEnd) return false;

            SetIndex(Index + 1);
            return true;
        }

        /// <summary>
        /// Moves the previous.
        /// </summary>
        /// <returns></returns>
        public bool MovePrevious()
        {
            if (IsAtStart) return false;

            SetIndex(Index - 1);
            return true;
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

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The data.</param>
        private void SetData(string data)
        {
            Data = data.ToCharArray();
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

        /// <summary>
        /// Sets the index offset.
        /// </summary>
        /// <param name="indexOffset">The index offset.</param>
        public void SetIndexOffset(int indexOffset)
        {
            if (indexOffset < 0) throw new ArgumentOutOfRangeException("indexOffset");

            IndexOffset = indexOffset;
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
            if (startIndex < 0) throw new ArgumentOutOfRangeException("startIndex", startIndex, "startIndex must be zero or greater");
            if (startIndex + length > Length) throw new ArgumentOutOfRangeException("length", length, "The length when added to the startIndex must be less than the length of the buffer");

            return new string(Data, startIndex, length);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return new string(Data);
        }

        #endregion
    }
}
