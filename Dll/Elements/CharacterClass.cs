
using System.Text.RegularExpressions;

namespace Elements
{
    public class CharacterClass : Element
    {
        public static Regex ClassRegex;

        private string Content;

        private bool Negate;

        public string Contents
        {
            get
            {
                return this.Content;
            }
        }

        static CharacterClass()
        {
            CharacterClass.ClassRegex = new Regex("^\\[(?<Negate>\\^?)(?<Contents>.*)]", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
        }

        public CharacterClass(CharacterBuffer buffer)
        {
            //this.Image = ImageType.CharacterClass;
            this.Start = buffer.IndexInOriginalBuffer;
            if (buffer.IsAtEnd)
            {
                Utility.ParseError("CharacterClass: Reached end of buffer looking for a character!", buffer);
                this.IsValid = false;
            }
            int currentIndex = buffer.CurrentIndex;
            ParsedCharacterClass parsedCharacterClass = buffer.GetParsedCharacterClass();
            if (parsedCharacterClass.Count == 0)
            {
                this.Description = parsedCharacterClass.ErrorMessage;
                buffer.MoveTo(currentIndex + 1);
                this.End = buffer.IndexInOriginalBuffer;
                this.Literal = "[";
                this.IsValid = false;
                return;
            }
            int num = buffer.CurrentIndex - currentIndex;
            Match match = CharacterClass.ClassRegex.Match(buffer.Substring(currentIndex, num));
            if (!match.Success)
            {
                this.Description = "Invalid Character Class";
                this.IsValid = false;
                this.Literal = "[";
            }
            else
            {
                if (match.Groups["Negate"].Value != "^")
                {
                    this.Negate = false;
                    this.MatchIfAbsent = false;
                }
                else
                {
                    this.Negate = true;
                    this.MatchIfAbsent = true;
                }
                if (match.Groups["Contents"].Value.Length == 0)
                {
                    this.Description = "Character class is empty";
                    this.IsValid = false;
                }
                else
                {
                    this.Content = match.Groups["Contents"].Value;
                }
                this.Literal = match.Value;
            }
            if (this.IsValid)
            {
                if (!this.Negate)
                {
                    this.Description = string.Concat("Any character in this class: ", this.Literal);
                }
                else
                {
                    this.Description = string.Concat("Any character that is NOT in this class: ", this.Literal.Remove(1, 1));
                }
            }
            base.ParseRepetitions(buffer);
        }
    }
}