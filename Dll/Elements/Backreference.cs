
using System.Collections;
using System.Text.RegularExpressions;

namespace Elements
{
    public class Backreference : Element
    {

        private static ArrayList Numbers;

        private static ArrayList FirstPassNumbers;

        private static ArrayList Names;

        private readonly string ASCII = "ASCII Octal ";

        private readonly string Numbered = "Backreference to capture number: ";

        private readonly string Named = "Backreference to capture named: ";

        private readonly string MissingNumber = "Backreference to missing capture number: ";

        private readonly string MissingName = "Backreference to missing capture name: ";

        private static Regex BackrefRegex;

        private static Regex OctalBackParseRegex;

        private string contents;

        private bool isOctal;

        private bool isNamed;

        public string Contents
        {
            get
            {
                return this.contents;
            }
        }

        public bool IsOctal
        {
            get
            {
                return isOctal;
            }
        }

        public static bool IsFirstPass;

        public static bool NeedsSecondPass;

        static Backreference()
        {
            Backreference.Numbers = new ArrayList();
            Backreference.FirstPassNumbers = new ArrayList();
            Backreference.Names = new ArrayList();
            Backreference.BackrefRegex = new Regex("^k[<'](?<Named>\\w+)[>']|^(?<Octal>0[0-7]{0,2})|^(?<Backreference>[1-9](?=\\D|$))|^(?<Decimal>[1-9]\\d+)\r\n\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            Backreference.OctalBackParseRegex = new Regex("^(?<Octal>[1-3][0-7]{0,2})|^(?<Octal>[4-7][0-7]?)\r\n\r\n", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            Backreference.IsFirstPass = true;
            Backreference.NeedsSecondPass = false;
        }

        public Backreference()
        {
            this.isOctal = false;
            this.isNamed = false;
            this.contents = "";
        }

        public static void AddName(string name)
        {
            if (!Backreference.Names.Contains(name))
            {
                Backreference.Names.Add(name);
            }
        }

        public static void AddNamedCaptureNumbers()
        {
            for (int i = 0; i < Backreference.Names.Count; i++)
            {
                Backreference.AddNumber();
            }
        }

        public static void AddNumber(int n)
        {
            if (!Backreference.Numbers.Contains(n))
            {
                Backreference.Numbers.Add(n);
            }
        }


        public static int AddNumber()
        {
            if (Backreference.Numbers == null)
            {
                Backreference.AddNumber(1);
                return 1;
            }
            for (int i = 1; i <= Backreference.Numbers.Count + 1; i++)
            {
                if (!Backreference.Numbers.Contains(i))
                {
                    Backreference.AddNumber(i);
                    return i;
                }
            }
            return 0;
        }

        public static bool ContainsName(string name)
        {
            return Backreference.Names.Contains(name);
        }

        public static bool ContainsNumber(string name)
        {
            if (!Regex.Match(name, "^\\d+$").Success)
            {
                return false;
            }
            if (Backreference.IsFirstPass)
            {
                return Backreference.Numbers.Contains(int.Parse(name));
            }
            return Backreference.FirstPassNumbers.Contains(int.Parse(name));
        }

        public static void InitializeSecondPass()
        {
            Backreference.AddNamedCaptureNumbers();
            Backreference.FirstPassNumbers = (ArrayList)Backreference.Numbers.Clone();
            Backreference.Numbers.Clear();
            Backreference.IsFirstPass = false;
            Backreference.NeedsSecondPass = false;
        }

        public static bool NumbersContains(int n)
        {
            if (Backreference.IsFirstPass)
            {
                return Backreference.Numbers.Contains(n);
            }
            return Backreference.FirstPassNumbers.Contains(n);
        }


        public bool Parse(CharacterBuffer buffer)
        {
            int num;
            this.Start = buffer.IndexInOriginalBuffer;
            if (buffer.CurrentCharacter != '\\')
            {
                return false;
            }
            buffer.MoveNext();
            if (buffer.IsAtEnd)
            {
                return false;
            }
            char current = buffer.CurrentCharacter;
            if (!char.IsDigit(current) && current != 'k')
            {
                buffer.Move(-1);
                return false;
            }
            Match match = Backreference.BackrefRegex.Match(buffer.GetToEnd());
            if (!match.Success)
            {
                if (current != 'k')
                {
                    return false;
                }
                this.IsValid = false;
                this.Literal = "\\k";
                this.Description = "Invalid backreference";
                this.contents = "";
                buffer.MoveNext();
                this.End = buffer.IndexInOriginalBuffer;
                return true;
            }
            string value = match.Groups["Backreference"].Value;
            string str = match.Groups["Decimal"].Value;
            string value1 = match.Groups["Octal"].Value;
            string str1 = match.Groups["Named"].Value;
            this.Literal = string.Concat('\\', match.Value);
            if (str1 != "")
            {
                if (Backreference.Names.Contains(str1))
                {
                    this.Description = string.Concat(this.Named, str1);
                }
                else if (!int.TryParse(str1, out num) || !Backreference.NumbersContains(num))
                {
                    this.Description = string.Concat(this.MissingName, str1);
                    this.IsValid = false;
                }
                else
                {
                    this.Description = string.Concat(this.Numbered, str1);
                }
                buffer.Move(match.Length);
                base.ParseRepetitions(buffer);
                this.isNamed = true;
                this.contents = str1;
                return true;
            }
            if (value1 != "")
            {
                this.Description = string.Concat(this.ASCII, value1);
                buffer.Move(match.Length);
                base.ParseRepetitions(buffer);
                //this.Image = ImageType.Character;
                this.isOctal = true;
                this.contents = value1;
                return true;
            }
            if (!buffer.IsEcma)
            {
                if (value != "")
                {
                    if (!Backreference.NumbersContains(int.Parse(value)))
                    {
                        this.Description = string.Concat(this.MissingNumber, value);
                        this.IsValid = false;
                    }
                    else
                    {
                        this.Description = string.Concat(this.Numbered, value);
                    }
                    buffer.Move(match.Length);
                    base.ParseRepetitions(buffer);
                    this.contents = value;
                    return true;
                }
                num = int.Parse(str);
                if (Backreference.NumbersContains(num))
                {
                    this.Description = string.Concat(this.Numbered, str);
                    this.contents = str;
                    buffer.Move(match.Length);
                    base.ParseRepetitions(buffer);
                    return true;
                }
                match = Backreference.OctalBackParseRegex.Match(buffer.GetToEnd());
                if (!match.Success)
                {
                    return false;
                }
                this.Literal = string.Concat('\\', match.Value);
                this.Description = string.Concat(this.ASCII, match.Groups["Octal"].Value);
                buffer.Move(match.Length);
                base.ParseRepetitions(buffer);
                //this.Image = ImageType.Character;
                this.isOctal = true;
                this.contents = match.Groups["Octal"].Value;
                return true;
            }
            if (value != "")
            {
                num = int.Parse(value);
                if (Backreference.NumbersContains(num))
                {
                    this.Description = string.Concat(this.Numbered, value);
                    buffer.Move(match.Length);
                    this.contents = value;
                    base.ParseRepetitions(buffer);
                    return true;
                }
                match = Backreference.OctalBackParseRegex.Match(buffer.GetToEnd());
                if (!match.Success)
                {
                    return false;
                }
                this.Literal = string.Concat('\\', match.Value);
                this.Description = string.Concat(this.ASCII, match.Groups["Octal"].Value);
                buffer.Move(match.Length);
                base.ParseRepetitions(buffer);
                //this.Image = ImageType.Character;
                this.isOctal = true;
                this.contents = match.Groups["Octal"].Value;
                return true;
            }
            if (str == "")
            {
                return false;
            }
            for (int i = str.Length; i > 0; i--)
            {
                string str2 = str.Substring(0, i);
                num = int.Parse(str2);
                if (Backreference.NumbersContains(num))
                {
                    this.Description = string.Concat(this.Numbered, str2);
                    this.Literal = string.Concat("\\", str2);
                    this.contents = str2;
                    buffer.Move(i);
                    base.ParseRepetitions(buffer);
                    return true;
                }
            }
            match = Backreference.OctalBackParseRegex.Match(buffer.GetToEnd());
            if (!match.Success)
            {
                return false;
            }
            this.Literal = string.Concat('\\', match.Value);
            this.Description = string.Concat(this.ASCII, match.Groups["Octal"].Value);
            buffer.Move(match.Length);
            base.ParseRepetitions(buffer);
            //this.Image = ImageType.Character;
            this.isOctal = true;
            this.contents = match.Groups["Octal"].Value;
            return true;
        }
    }
}