using Elements.Enumerations;
using System;

namespace Elements
{
    public abstract class Element
    {
        #region Properties

        public string Description { get; private set; }

        public int EndIndex { get; private set; }

        public bool IsValid { get; private set; }

        public string Literal { get; private set; }

        public RepeatType RepeatType { get; private set; }

        public int StartIndex { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Element"/> class.
        /// </summary>
        protected Element()
        {
            IsValid = true;
            RepeatType = RepeatType.Once;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the description.
        /// </summary>
        /// <param name="description">The description.</param>
        public void SetDescription(string description)
        {
            if (string.IsNullOrEmpty(description)) throw new ArgumentNullException("description");

            Description = description;
        }

        /// <summary>
        /// Sets the is valid.
        /// </summary>
        /// <param name="value">
        /// Set value to <c>true</c> to indicate this insatnce is valid.
        /// </param>
        public void SetIsValid(bool value)
        {
            IsValid = value;
        }

        /// <summary>
        /// Sets the literal.
        /// </summary>
        /// <param name="literal">The literal.</param>
        /// <exception cref="System.ArgumentNullException">literal</exception>
        public void SetLiteral(string literal)
        {
            if (string.IsNullOrEmpty(literal)) throw new ArgumentNullException("literal");

            Literal = literal;
        }

        /// <summary>
        /// Sets the start index.
        /// </summary>
        /// <param name="index">The index.</param>
        public void SetStartIndex(int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException("index");

            StartIndex = index;
        }

        #endregion
    }
}