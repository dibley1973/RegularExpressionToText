using System;
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

        ///// <summary>
        ///// Prevents a default instance of the <see cref="Expression"/> class from being created.
        ///// </summary>
        //private Expression()
        //{
        //    //Alternatives = new Alternatives();
        //    Elements = new List<Element>();
        //    HasEcmaSyntax = false;
        //}

        public Expression()
        {
            //Alternatives = new Alternatives();
            Elements = new List<Element>();
            Literal = "";
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
            : this()
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

        #endregion

        #region Methods

        /// <summary>
        /// Adds the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        public virtual void AddElement(Element element)
        {
            if(element == null) throw new ArgumentNullException("element");

            Elements.Add(element);
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns></returns>
        public Expression Clone()
        {
            Expression expression = new Expression();
            //foreach (Element element in Elements)
            //{
            //    expression.Elements.Add(element);
            //}
            expression.Elements.AddRange(Elements);
            expression.Literal = Literal;
            //expression.IgnoreWhitespace = this.IgnoreWhitespace;
            expression.HasEcmaSyntax = HasEcmaSyntax;
            return expression;
        }

        #endregion
    }
}