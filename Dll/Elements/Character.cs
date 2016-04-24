
namespace Elements
{
    public class Character : Element
    {
        private string _character;

        public string TheCharacter
        {
            get
            {
                return _character;
            }
        }

        public Character(CharacterBuffer buffer, bool repetitionsOnly)
        {
            //this.Image = ImageType.Character;
            Start = buffer.IndexInOriginalBuffer;
            if (buffer.IsAtEnd)
            {
                Utility.ParseError("Reached end of buffer in Character constructor!", buffer);
                IsValid = false;
            }
            else if (repetitionsOnly)
            {
                _character = buffer.CurrentCharacter.ToString();
                ParseRepetitions(buffer);
                return;
            }
            _character = buffer.CurrentCharacter.ToString();
            Literal = _character;
            Description = Literal;
            buffer.MoveNext();
            ParseRepetitions(buffer);
        }

        public Character(CharacterBuffer buffer) : this(buffer, false)
        {
        }
    }
}