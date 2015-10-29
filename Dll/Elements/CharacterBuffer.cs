
using System;

namespace Elements
{
    public class CharacterBuffer
    {
        #region Fields

        private const string NullCharacter = "\0";

        #endregion

        #region Properties

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

        // Consider storing the data as an array as there
        // could be many ToCharArray() calls!
        protected string Data { get; private set; }



        public int DataLength { get; private set; }
        protected int Index { get; private set; }

        public bool IsAtEnd
        {
            get { return Index == DataLength; }
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
            return IsAtEnd ? string.Empty : Data.Substring(Index);
        }

        /// <summary>
        /// Sets the data.
        /// </summary>
        /// <param name="data">The data.</param>
        private void SetData(string data)
        {
            Data = data;
        }

        /// <summary>
        /// Sets the length.
        /// </summary>
        /// <param name="data">The data.</param>
        private void SetDataLength(string data)
        {
            DataLength = data.Length;
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