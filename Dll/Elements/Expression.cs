using System.Collections.Generic;

namespace Entities
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

        ///// <summary>
        ///// Gets or sets a value indicating whether to ignore whitespace or not.
        ///// </summary>
        ///// <value>
        /////   <c>true</c> if to ignore whitespace; otherwise, <c>false</c>.
        ///// </value>
        //public bool IgnoreWhitespace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has ECMA sytax.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has ECMA syntax; otherwise, <c>false</c>.
        /// </value>
        public bool HasEcmaSyntax { get; set; }

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
            HasEcmaSyntax = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="literal">The literal.</param>
        public Expression(string literal)
            : this()
        {
            Literal = literal;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        public Expression(CharacterBuffer buffer)
        {
            Literal = buffer.GetToEnd();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Expression"/> class.
        /// </summary>
        /// <param name="literal">The literal text.</param>
        /// <param name="hasEcmaSyntax">
        /// Set to <c>true</c> then has ECMA syntax; otherwise <c>false</c>.
        /// </param>
        public Expression(string literal, bool hasEcmaSyntax)
            : this(literal)
        {

            HasEcmaSyntax = hasEcmaSyntax;
        }

        //public Expression(string literal, int offset, bool ignoreWhitespace, bool HasEcmaSyntax)
        //    : this(literal, ignoreWhitespace, HasEcmaSyntax)
        //{
        //    Parse(offset);
        //}

        //public Expression(string literal, int offset, bool ignoreWhitespace, bool HasEcmaSyntax, bool skipFirstCaptureNumber)
        //    : this(literal, ignoreWhitespace, HasEcmaSyntax)
        //{
        //    Parse(offset, skipFirstCaptureNumber);
        //}


        #endregion

        #region Methods

       

        #endregion
    }
}