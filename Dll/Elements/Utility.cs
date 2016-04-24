using System;

namespace Elements
{
    public class Utility
    {

        public static void ExpressoError(string msg)
        {
            // TODO: Rename this method to remove Expresso name and meaning
            throw new Exception(msg);
            //MessageBox.Show(msg, "Expresso Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static void ParseError(string message, CharacterBuffer buffer)
        {
            string str = string.Concat(
                "Cannot parse the regular expression\n\n", 
                message, 
                "\n\n", 
                buffer.Snapshot());
            ExpressoError(str);
        }
    }
}