
using Elements.Enumerations;
using System.Text.RegularExpressions;

namespace Elements
{
    public class Group : Element
    {
        public GroupType Type;

        public string Name;

        public string Name2;

        public SubExpression Content;

        private string Options;

        public CheckState SetX;

        public CheckState SetI;

        public CheckState SetM;

        public CheckState SetS;

        public CheckState SetN;

        private static Regex RegGroup;

        static Group()
        {
            Group.RegGroup = new Regex("^\\(\\?['<](?<Name>[a-zA-Z]?[\\w\\d]*)(?<GroupType>-)(?<Name2>[a-zA-Z]?[\\w\\d]*)['>](?<Contents>.*)\\)|\r\n^\\(\\?(?<Options>[imnsx-]{1,15}:)(?<Contents>.*)\\)|\r\n^\\(\\?(?<Options>[imnsx-]{1,15})\\)|\r\n^\\(\\?(?<GroupType>\\(|\\<\\!|\\!|\\>|\\#|\\:|\\=|\\<\\=|[<'](?<Name>[a-zA-Z][\\w\\d]*)[>']|[<'](?<Number>\\d*)[>'])(?<Contents>.*)\\)|\r\n^\\((?<Contents>.*)\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
        }

        public Group(CharacterBuffer buffer)
            : this(buffer, false)
        {
        }

        public Group(CharacterBuffer buffer, bool SkipCaptureNumber)
        {
            char literal;
            char chr;
            int num;
            bool flag;
            bool flag1;
            //this.Image = ImageType.Group;
            this.Start = buffer.IndexInOriginalBuffer;
            bool flag2 = true;
            this.Literal = buffer.GetStringToMatchingParenthesis();
            if (this.Literal == "")
            {
                return;
            }
            Match match = Group.RegGroup.Match(this.Literal);
            if (!match.Success)
            {
                this.Type = GroupType.Invalid;
                this.Content = new SubExpression("", 0, false, false);
                this.IsValid = false;
                flag2 = false;
                this.Description = "Syntax error in group definition";
                buffer.Move(1 - this.Literal.Length);
            }
            else
            {
                string value = match.Groups["GroupType"].Value;
                string str = match.Groups["Name"].Value;
                string value1 = match.Groups["Number"].Value;
                string str1 = match.Groups["Options"].Value;
                string value2 = match.Groups["Contents"].Value;
                int start = this.Start + match.Groups["Contents"].Index;
                this.Name2 = match.Groups["Name2"].Value;
                if (str1 == "")
                {
                    string str2 = value;
                    string str3 = str2;
                    if (str2 != null)
                    {
                        switch (str3)
                        {
                            case ":":
                                {
                                    this.Type = GroupType.Noncapturing;
                                    goto Label0;
                                }
                            case "=":
                                {
                                    this.Type = GroupType.SuffixPresent;
                                    goto Label0;
                                }
                            case "<=":
                                {
                                    this.Type = GroupType.PrefixPresent;
                                    goto Label0;
                                }
                            case "<!":
                                {
                                    this.Type = GroupType.PrefixAbsent;
                                    goto Label0;
                                }
                            case "!":
                                {
                                    this.Type = GroupType.SuffixAbsent;
                                    goto Label0;
                                }
                            case "#":
                                {
                                    this.Type = GroupType.Comment;
                                    flag2 = false;
                                    goto Label0;
                                }
                            case ">":
                                {
                                    this.Type = GroupType.Greedy;
                                    goto Label0;
                                }
                            case "(":
                                {
                                    this.Type = GroupType.Invalid;
                                    this.Content = new SubExpression("", 0, false, false);
                                    this.IsValid = false;
                                    this.Description = "Syntax error in group definition";
                                    flag2 = false;
                                    buffer.Move(1 - this.Literal.Length);
                                    goto Label0;
                                }
                            case "":
                                {
                                    if (value2.Length <= 0 || !(value2.Substring(0, 1) == "?"))
                                    {
                                        this.Type = GroupType.Numbered;
                                        if (SkipCaptureNumber)
                                        {
                                            this.Name = "";
                                            goto Label0;
                                        }
                                        else
                                        {
                                            this.Name = Backreference.AddNumber().ToString();
                                            goto Label0;
                                        }
                                    }
                                    else
                                    {
                                        this.Type = GroupType.Invalid;
                                        value2 = value2.Substring(1);
                                        this.Content = new SubExpression(value2, start + 1, buffer.IgnoreWhiteSpace, buffer.IsEcma);
                                        this.Description = "Illegal group syntax";
                                        this.IsValid = false;
                                        goto Label0;
                                    }
                                }
                            case "-":
                                {
                                    int index = match.Groups["Name"].Index - 1;
                                    int index1 = match.Groups["Name2"].Index + match.Groups["Name2"].Length;
                                    literal = this.Literal[index];
                                    chr = this.Literal[index1];
                                    if (literal != '<' || chr != '>')
                                    {
                                        flag1 = (literal != '\'' ? false : chr == '\'');
                                    }
                                    else
                                    {
                                        flag1 = true;
                                    }
                                    this.IsValid = flag1;
                                    if (!this.IsValid)
                                    {
                                        this.Description = "Invalid syntax for balancing group";
                                    }
                                    this.Type = GroupType.Balancing;
                                    this.Name = str;
                                    if (this.Name != "")
                                    {
                                        if (!int.TryParse(this.Name, out num))
                                        {
                                            Backreference.AddName(this.Name);
                                        }
                                        else
                                        {
                                            Backreference.AddNumber(num);
                                        }
                                    }
                                    if (!int.TryParse(this.Name2, out num))
                                    {
                                        if (Backreference.ContainsName(this.Name2))
                                        {
                                            goto Label0;
                                        }
                                        this.Description = string.Concat("Invalid group name in a balancing group: ", this.Name2);
                                        this.IsValid = false;
                                        goto Label0;
                                    }
                                    else
                                    {
                                        if (Backreference.ContainsNumber(this.Name2))
                                        {
                                            goto Label0;
                                        }
                                        this.Description = string.Concat("Invalid group number in a balancing group: ", this.Name2);
                                        this.IsValid = false;
                                        goto Label0;
                                    }
                                }
                        }
                    }
                    literal = value[0];
                    chr = value[value.Length - 1];
                    if (literal != '<' || chr != '>')
                    {
                        flag = (literal != '\'' ? false : chr == '\'');
                    }
                    else
                    {
                        flag = true;
                    }
                    this.IsValid = flag;
                    if (str.Length > 0)
                    {
                        this.Type = GroupType.Named;
                        this.Name = str;
                        Backreference.AddName(str);
                        if (!this.IsValid)
                        {
                            this.Description = string.Concat("[", str, "] Invalid syntax for named group");
                        }
                    }
                    else if (value1.Length <= 0)
                    {
                        this.Type = GroupType.Named;
                        this.Name = "";
                        this.IsValid = false;
                        this.Description = "Missing name for a named group";
                    }
                    else
                    {
                        this.Type = GroupType.Numbered;
                        this.Name = value1;
                        Backreference.AddNumber(int.Parse(value1));
                        if (!this.IsValid)
                        {
                            this.Description = string.Concat("[", value1, "] Invalid syntax for numbered group");
                        }
                    }
                }
                else
                {
                    this.DecodeOptions(str1);
                    if (this.Type == GroupType.OptionsOutside)
                    {
                        flag2 = false;
                    }
                }
            Label0:
                bool ignoreWhiteSpace = buffer.IgnoreWhiteSpace;
                if (this.Type == GroupType.OptionsInside || this.Type == GroupType.OptionsOutside)
                {
                    if (this.SetX == CheckState.Checked)
                    {
                        ignoreWhiteSpace = true;
                    }
                    else if (this.SetX == CheckState.Unchecked)
                    {
                        ignoreWhiteSpace = false;
                    }
                }
                if (this.IsValid || this.Type == GroupType.Named || this.Type == GroupType.Numbered || this.Type == GroupType.Balancing)
                {
                    this.Content = new SubExpression(value2, start, ignoreWhiteSpace, buffer.IsEcma);
                }
            }
            buffer.MoveNext();
            if (!flag2)
            {
                this.End = buffer.IndexInOriginalBuffer;
                this.RepeatType = Repeat.Once;
            }
            else
            {
                base.ParseRepetitions(buffer);
            }
            this.SetDescription();
        }

        private void DecodeOptions(string options)
        {
            CharacterBuffer charBuffer = new CharacterBuffer(options);
            this.SetI = CheckState.Indeterminate;
            this.SetM = CheckState.Indeterminate;
            this.SetS = CheckState.Indeterminate;
            this.SetN = CheckState.Indeterminate;
            this.SetX = CheckState.Indeterminate;
            if (options.Substring(options.Length - 1, 1) != ":")
            {
                this.Type = GroupType.OptionsOutside;
                this.Options = string.Concat("Change options within the enclosing group [", options, "]");
            }
            else
            {
                this.Type = GroupType.OptionsInside;
                this.Options = string.Concat("Change options within a new noncapturing group [", options, "]");
            }
            bool flag = true;
            while (!charBuffer.IsAtEnd)
            {
                char current = charBuffer.CurrentCharacter;
                if (current > 'i')
                {
                    switch (current)
                    {
                        case 'm':
                            {
                                if (!flag)
                                {
                                    this.SetM = CheckState.Unchecked;
                                    break;
                                }
                                else
                                {
                                    this.SetM = CheckState.Checked;
                                    break;
                                }
                            }
                        case 'n':
                            {
                                if (!flag)
                                {
                                    this.SetN = CheckState.Unchecked;
                                    break;
                                }
                                else
                                {
                                    this.SetN = CheckState.Checked;
                                    break;
                                }
                            }
                        default:
                            {
                                if (current == 's')
                                {
                                    if (!flag)
                                    {
                                        this.SetS = CheckState.Unchecked;
                                        break;
                                    }
                                    else
                                    {
                                        this.SetS = CheckState.Checked;
                                        break;
                                    }
                                }
                                else if (current != 'x')
                                {
                                    goto Label0;
                                }
                                else if (!flag)
                                {
                                    this.SetX = CheckState.Unchecked;
                                    break;
                                }
                                else
                                {
                                    this.SetX = CheckState.Checked;
                                    break;
                                }
                            }
                    }
                }
                else if (current == '-')
                {
                    flag = false;
                }
                else if (current != ':')
                {
                    if (current != 'i')
                    {
                        goto Label0;
                    }
                    if (!flag)
                    {
                        this.SetI = CheckState.Unchecked;
                    }
                    else
                    {
                        this.SetI = CheckState.Checked;
                    }
                }
            Label2:
                charBuffer.MoveNext();
            }
            return;
        Label0:
            Utility.ParseError("Error in options construct!", charBuffer);
            this.IsValid = false;
            //goto Label2; // TODO: This does not complile so threw lines below...
            charBuffer.MoveNext();
            return;
        }

        private void SetDescription()
        {
            string options = "";
            switch (this.Type)
            {
                case GroupType.Balancing:
                    {
                        if (this.Name != "" && this.Name2 != "")
                        {
                            string[] name = new string[] { "Balancing group. Restore previous match for [", this.Name, "]. Store the interval from that match to current match in [", this.Name2, "]" };
                            options = string.Concat(name);
                            break;
                        }
                        else if (this.Name == "" && this.Name2 != "")
                        {
                            options = string.Concat("Balancing group. Remove the most recent [", this.Name2, "] capture from the stack");
                            break;
                        }
                        else if (this.Name != "")
                        {
                            this.Description = "Balancing group. Missing the second group name.";
                            this.IsValid = false;
                            break;
                        }
                        else
                        {
                            this.Description = "Balancing group. Missing both group names.";
                            this.IsValid = false;
                            break;
                        }
                    }
                case GroupType.Named:
                    {
                        options = string.Concat("[", this.Name, "]: A named capture group");
                        break;
                    }
                case GroupType.Numbered:
                    {
                        options = string.Concat("[", this.Name, "]: A numbered capture group");
                        break;
                    }
                case GroupType.Noncapturing:
                    {
                        options = "Match expression but don't capture it";
                        break;
                    }
                case GroupType.SuffixPresent:
                    {
                        options = "Match a suffix but exclude it from the capture";
                        break;
                    }
                case GroupType.PrefixPresent:
                    {
                        options = "Match a prefix but exclude it from the capture";
                        break;
                    }
                case GroupType.SuffixAbsent:
                    {
                        options = "Match if suffix is absent";
                        break;
                    }
                case GroupType.PrefixAbsent:
                    {
                        options = "Match if prefix is absent";
                        break;
                    }
                case GroupType.Greedy:
                    {
                        options = "Greedy subexpression";
                        break;
                    }
                case GroupType.Comment:
                    {
                        options = "Comment";
                        break;
                    }
                case GroupType.OptionsInside:
                case GroupType.OptionsOutside:
                    {
                        options = this.Options;
                        break;
                    }
                case GroupType.Alternatives:
                case GroupType.Conditional:
                case GroupType.NoGroup:
                    {
                        options = "";
                        break;
                    }
                case GroupType.Invalid:
                    {
                        options = "";
                        break;
                    }
                default:
                    {
                        goto case GroupType.NoGroup;
                    }
            }
            if (this.IsValid)
            {
                if (this.Type == GroupType.OptionsOutside)
                {
                    this.Description = options;
                    return;
                }
                if (this.Content != null)
                {
                    Group group = this;
                    string description = group.Description;
                    string[] literal = new string[] { description, options, ". [", this.Content.Literal, "]" };
                    group.Description = string.Concat(literal);
                    return;
                }
                Group group1 = this;
                group1.Description = string.Concat(group1.Description, options);
            }
        }
    }
}