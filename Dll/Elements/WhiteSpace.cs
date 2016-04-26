using System.Text.RegularExpressions;

namespace Elements
{
    public class WhiteSpace : Element
    {
        public static readonly Regex FindWhiteSpace;

        public static readonly Regex FindEcmaWhiteSpace;

        static WhiteSpace()
        {
            FindWhiteSpace = new Regex(RegularExpressionStrings.WhiteSpace.FindWhiteSpace, RegexOptions.Compiled);
            FindEcmaWhiteSpace = new Regex(RegularExpressionStrings.WhiteSpace.FindEcmaWhiteSpace, RegexOptions.Compiled | RegexOptions.ECMAScript);
        }

        public WhiteSpace(int originalIndex, string text)
        {
            //this.Image = ImageType.WhiteSpace;
            Start = originalIndex;
            End = originalIndex + text.Length;
            Literal = text;
            Description = Resources.WhiteSpace.Description;
        }
    }
}
