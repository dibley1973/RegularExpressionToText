
using Elements.Enumerations;
using System.Text.RegularExpressions;

namespace Elements
{
    public class SpecialCharacter : Element
    {
        private string character;

        public bool Escaped;

        public CharType CharacterType;

        private static Regex EscapableCharacters;

        private static Regex RegNumeric;

        static SpecialCharacter()
        {
            SpecialCharacter.EscapableCharacters = new Regex(
                "[-=!@#$%^&*()_+~`{}|\\][:\"';<>?/.,\\\\]",
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline |
                RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
            SpecialCharacter.RegNumeric = new Regex(
                "^[0-7]{3} | ^x[0-9A-Fa-f]{2} | ^u[0-9A-Fa-f]{4} | ^c[A-Z\\[\\\\\\]\\^_]",
                RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Compiled |
                RegexOptions.IgnorePatternWhitespace);
        }

        public SpecialCharacter(BackReference back)
        {
            if (!back.IsOctal)
            {
                Utility.ExpressoError("Error trying to convert a Backreference item to a Special Character");
                return;
            }
            this.Description = string.Concat("Octal ", back.Contents);
            this.CharacterType = CharType.Octal;
            this.character = back.Contents;
            this.Literal = string.Concat("\\", back.Contents);
            this.Escaped = false;
            this.End = back.End;
            this.AsFewAsPossible = back.AsFewAsPossible;
            this.m = back.m;
            this.n = back.n;
            this.MatchIfAbsent = back.MatchIfAbsent;
            this.RepeatType = back.RepeatType;
            this.Start = back.Start;
        }

        public SpecialCharacter(CharacterBuffer buffer)
        {
            //this.Image = ImageType.SpecialCharacter;
            this.Escaped = false;
            this.Start = buffer.IndexInOriginalBuffer;
            if (buffer.IsAtEnd)
            {
                this.CharacterType = CharType.Invalid;
                this.character = "\\";
                this.Literal = "\\";
                this.Description = "Illegal \\ at end of pattern";
                this.IsValid = false;
                this.Start = this.Start - 1;
                this.End = this.Start + 1;
            }
            else if (buffer.CurrentCharacter == '[')
            {
                buffer.MoveNext();
                if (buffer.CurrentCharacter != '\u005E')
                {
                    buffer.MoveNext();
                    this.Literal = string.Concat("[\\", buffer.CurrentCharacter);
                }
                else
                {
                    this.MatchIfAbsent = true;
                    buffer.Move(2);
                    this.Literal = string.Concat("[^\\", buffer.CurrentCharacter);
                }
                this.S = buffer.CurrentCharacter.ToString();
                if (this.MatchIfAbsent)
                {
                    this.Description = string.Concat("Any character other than ", this.Description);
                }
                buffer.MoveNext();
                SpecialCharacter specialCharacter = this;
                specialCharacter.Literal = string.Concat(specialCharacter.Literal, buffer.CurrentCharacter);
                buffer.MoveNext();
            }
            else if (buffer.CurrentCharacter != '\\')
            {
                this.S = buffer.CurrentCharacter.ToString();
                this.Literal = this.S;
                buffer.MoveNext();
            }
            else
            {
                buffer.MoveNext();
                if (!buffer.IsAtEnd)
                {
                    Match match = SpecialCharacter.RegNumeric.Match(buffer.GetToEnd());
                    if (!match.Success)
                    {
                        this.Escaped = true;
                        this.S = buffer.CurrentCharacter.ToString();
                        this.Literal = string.Concat("\\", this.S);
                        buffer.MoveNext();
                    }
                    else
                    {
                        string str = match.Value.Substring(0, 1);
                        string str1 = str;
                        if (str != null)
                        {
                            if (str1 == "x")
                            {
                                this.Description = string.Concat("Hex ", match.Value.Substring(1));
                                this.CharacterType = CharType.Hex;
                                this.character = match.Value.Substring(1);
                                goto Label0;
                            }
                            else if (str1 == "u")
                            {
                                this.Description = string.Concat("Unicode ", match.Value.Substring(1));
                                this.CharacterType = CharType.Unicode;
                                this.character = match.Value.Substring(1);
                                goto Label0;
                            }
                            else
                            {
                                if (str1 != "c")
                                {
                                    goto Label2;
                                }
                                this.Description = string.Concat("Control ", match.Value.Substring(1, 1));
                                this.CharacterType = CharType.Control;
                                this.character = match.Value.Substring(1);
                                goto Label0;
                            }
                        }
                    Label2:
                        this.Description = string.Concat("Octal ", match.Value);
                        this.CharacterType = CharType.Octal;
                        this.character = match.Value.Substring(2);
                    Label0:
                        this.Literal = string.Concat("\\", match.Value);
                        buffer.Move(match.Length);
                    }
                }
                else
                {
                    Utility.ParseError("Illegal \\ at end of pattern", buffer);
                }
            }
            base.ParseRepetitions(buffer);
        }

        private string S
        {
            get { return this.character; }
            set
            {
                this.character = value;
                string str = "";
                string str1 = value;
                string str2 = str1;
                if (str1 != null)
                {
                    switch (str2)
                    {
                        case "s":
                        {
                            str = string.Concat(str, "Whitespace");
                            this.CharacterType = CharType.CharClass;
                            break;
                        }
                        case ".":
                        {
                            if (this.Escaped)
                            {
                                str = string.Concat(str, "Literal ", this.character);
                                this.CharacterType = CharType.Escaped;
                                break;
                            }
                            else
                            {
                                str = string.Concat(str, "Any character");
                                this.CharacterType = CharType.CharClass;
                                break;
                            }
                        }
                        case "d":
                        {
                            str = string.Concat(str, "Any digit");
                            this.CharacterType = CharType.CharClass;
                            break;
                        }
                        case "w":
                        {
                            str = string.Concat(str, "Alphanumeric");
                            this.CharacterType = CharType.CharClass;
                            break;
                        }
                        case "S":
                        {
                            str = string.Concat(str, "Anything other than whitespace");
                            this.MatchIfAbsent = true;
                            this.CharacterType = CharType.CharClass;
                            break;
                        }
                        case "D":
                        {
                            str = string.Concat(str, "Any character that is not a digit");
                            this.MatchIfAbsent = true;
                            this.CharacterType = CharType.CharClass;
                            break;
                        }
                        case "W":
                        {
                            str = string.Concat(str, "Any character that is not alphanumeric");
                            this.MatchIfAbsent = true;
                            this.CharacterType = CharType.CharClass;
                            break;
                        }
                        case "a":
                        {
                            str = string.Concat(str, "Bell");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "t":
                        {
                            str = string.Concat(str, "Tab");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "r":
                        {
                            str = string.Concat(str, "Carriage return");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "v":
                        {
                            str = string.Concat(str, "Vertical tab");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "f":
                        {
                            str = string.Concat(str, "Form feed");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "n":
                        {
                            str = string.Concat(str, "New line");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "e":
                        {
                            str = string.Concat(str, "Escape");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "\t":
                        {
                            str = string.Concat(str, "Tab");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "\r":
                        {
                            str = string.Concat(str, "Return");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "\n":
                        {
                            str = string.Concat(str, "New line");
                            this.CharacterType = CharType.Escaped;
                            break;
                        }
                        case "^":
                        {
                            if (this.Escaped)
                            {
                                str = string.Concat(str, "Literal ", this.character);
                                this.CharacterType = CharType.Escaped;
                                break;
                            }
                            else
                            {
                                str = string.Concat(str, "Beginning of line or string");
                                this.CharacterType = CharType.ZeroWidth;
                                break;
                            }
                        }
                        case "$":
                        {
                            if (this.Escaped)
                            {
                                str = string.Concat(str, "Literal ", this.character);
                                this.CharacterType = CharType.Escaped;
                                break;
                            }
                            else
                            {
                                str = string.Concat(str, "End of line or string");
                                this.CharacterType = CharType.ZeroWidth;
                                break;
                            }
                        }
                        case "A":
                        {
                            str = string.Concat(str, "Beginning of string");
                            this.CharacterType = CharType.ZeroWidth;
                            break;
                        }
                        case "Z":
                        {
                            str = string.Concat(str, "End of string or before new line at end of string");
                            this.CharacterType = CharType.ZeroWidth;
                            break;
                        }
                        case "z":
                        {
                            str = string.Concat(str, "End of string");
                            this.CharacterType = CharType.ZeroWidth;
                            break;
                        }
                        case "G":
                        {
                            str = string.Concat(str, "Beginning of current search");
                            this.CharacterType = CharType.ZeroWidth;
                            break;
                        }
                        case "b":
                        {
                            str = string.Concat(str, "First or last character in a word");
                            this.CharacterType = CharType.ZeroWidth;
                            break;
                        }
                        case "B":
                        {
                            str = string.Concat(str, "Anything other than the first or last character in a word");
                            this.MatchIfAbsent = true;
                            this.CharacterType = CharType.ZeroWidth;
                            break;
                        }
                        case " ":
                        {
                            str = string.Concat(str, "Space");
                            this.CharacterType = CharType.Regular;
                            break;
                        }
                        default:
                        {
                            goto Label0;
                        }
                    }
                }
                else
                {
                    goto Label0;
                }
                this.Description = str;
                return;
                Label0:
                if (!SpecialCharacter.EscapableCharacters.Match(value.ToString()).Success)
                {
                    str = string.Concat(str, "Illegal escape character: ", value.ToString());
                    this.IsValid = false;
                    this.CharacterType = CharType.Invalid;
                    this.Description = str;
                    return;
                }
                else
                {
                    str = string.Concat(str, "Literal ", value.ToString());
                    this.CharacterType = CharType.Escaped;
                    this.Description = str;
                    return;
                }
            }
        }

        public string TheCharacter
        {
            get { return this.character; }
        }



        public static bool NextIsWhitespace(CharacterBuffer buffer)
        {
            bool flag = false;
            char next = buffer.Next;
            switch (next)
            {
                case '\t':
                case '\n':
                case '\r':
                {
                    flag = true;
                    return flag;
                }
                case '\v':
                case '\f':
                {
                    return flag;
                }
                default:
                {
                    if (next != ' ')
                    {
                        return flag;
                    }
                    else
                    {
                        flag = true;
                        return flag;
                    }
                }
            }
        }
    }
}