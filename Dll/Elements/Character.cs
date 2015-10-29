using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class Character : Element
    {
        #region Properties

        public string CurrentCharacter { get; private set; }

        #endregion

        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="repetitionsOnly">if set to <c>true</c> [repetitions only].</param>
        public Character(CharacterBuffer buffer, bool repetitionsOnly = false)
        {
            SetStartIndex(buffer.IndexInOriginalBuffer);
            if (buffer.IsAtEnd)
            {
                //Utility.ParseError("Reached end of buffer in Character constructor!", buffer);
                SetIsValid(false);
            }
            else if (repetitionsOnly)
            {
                CurrentCharacter = buffer.CurrentCharacter.ToString();
                //base.ParseRepetitions(buffer);
                return;
            }

            CurrentCharacter = buffer.CurrentCharacter.ToString();
            SetLiteral(CurrentCharacter);
            SetDescription(CurrentCharacter);
            buffer.MoveNext();
            //base.ParseRepetitions(buffer);
        }

        

        #endregion
    }
}
