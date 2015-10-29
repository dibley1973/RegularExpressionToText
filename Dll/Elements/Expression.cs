
namespace Elements
{
    /// <summary>
    /// Represents a regular expression
    /// </summary>
    public class Expression // : CollectionBase
    {
        #region Properties

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
            IsEcma = isEcma;;
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
        }

        #endregion
    }
}