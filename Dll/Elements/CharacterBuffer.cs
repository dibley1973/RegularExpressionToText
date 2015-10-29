﻿using System;

namespace Elements
{
    public class CharacterBuffer
    {
        #region Fields

        public const char NullCharacter = '\0';

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

        private int Index { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is at beginning.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is at beginning; otherwise, <c>false</c>.
        /// </value>
        public bool IsAtBeginning {
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
                    ? NullCharacter
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
                return IsAtBeginning 
                    ? NullCharacter 
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
            if(string.IsNullOrEmpty(data)) throw new ArgumentNullException("data");

            SetData(data);
            SetDataLength(data);
            SetIndex(0);
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
            if(index < 0) throw new ArgumentOutOfRangeException("index");

            Index = index;
        }

        #endregion
    }
}