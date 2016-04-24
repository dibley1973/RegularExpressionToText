using System.Text.RegularExpressions;

namespace Elements
{
    public class WhiteSpace : Element
    {
        public static Regex FindWhiteSpace;

        public static Regex FindECMAWhiteSpace;

        static WhiteSpace()
        {
            FindWhiteSpace = new Regex(RegularExpressions.WhiteSpace.FindWhiteSpace, RegexOptions.Compiled);
            FindECMAWhiteSpace = new Regex(RegularExpressions.WhiteSpace.FindECMAWhiteSpace, RegexOptions.Compiled | RegexOptions.ECMAScript);
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
