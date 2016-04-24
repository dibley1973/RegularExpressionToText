
using System.Text.RegularExpressions;

namespace Elements
{
    public class NamedClass : Element
    {
        private static Regex NamedClassRegex;

        private string ClassName;

        private string FriendlyName;


        public bool Parse(CharacterBuffer buffer)
        {
            string str;
            this.Start = buffer.IndexInOriginalBuffer;
            if (buffer.IsAtEnd)
            {
                return false;
            }
            if (buffer.Next != 'p' && buffer.Next != 'P')
            {
                return false;
            }
            Match match = NamedClass.NamedClassRegex.Match(buffer.GetToEnd());
            if (!match.Success || match.Groups["Type"].Length <= 0)
            {
                this.Description = "Syntax error in Unicode character class";
                this.Literal = "\\p";
                buffer.Move(2);
                this.End = buffer.IndexInOriginalBuffer;
                this.IsValid = false;
                return true;
            }
            if (match.Groups["Type"].Value != "P")
            {
                this.MatchIfAbsent = false;
            }
            else
            {
                this.MatchIfAbsent = true;
            }
            this.ClassName = match.Groups["Name"].Value;
            int length = (int)UnicodeCategories.UnicodeAbbrev.Length;
            this.FriendlyName = "";
            int num = 0;
            while (num < length)
            {
                if (UnicodeCategories.UnicodeAbbrev[num] != this.ClassName)
                {
                    num++;
                }
                else
                {
                    this.FriendlyName = UnicodeCategories.UnicodeName[num];
                    break;
                }
            }
            if (this.ClassName == "")
            {
                str = "Empty Unicode character class";
                this.IsValid = false;
            }
            else if (this.FriendlyName != "")
            {
                str = string.Concat("a Unicode character class: \"", this.FriendlyName, "\"");
            }
            else
            {
                str = string.Concat("Possibly unrecognized Unicode character class: [", this.ClassName, "]");
                this.IsValid = false;
            }
            this.Literal = match.Value;
            if (!this.IsValid)
            {
                this.Description = str;
            }
            else if (!this.MatchIfAbsent)
            {
                this.Description = string.Concat("Any character from ", str);
            }
            else
            {
                this.Description = string.Concat("Any character NOT from ", str);
            }
            buffer.Move(match.Length);
            base.ParseRepetitions(buffer);
            return true;
        }
    }
}
