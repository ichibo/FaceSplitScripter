using System;

namespace FaceSplitScripter
{
    internal static class ScriptUtilities
    {
        public static int GetQuantityFromText(string text)
        {
            int quantityStartIndex = text.IndexOf('(') + 1;
            int quantityEndIndex = text.IndexOf(')');
            int quantityLength = quantityEndIndex - quantityStartIndex;

            string quantityText = text.Substring(quantityStartIndex, quantityLength);

            try
            {
                int quantity = Int32.Parse(quantityText);
                return quantity;
            }
            catch
            {
                return 1;
            }
        }

        public static string RemoveQuantityFromText(string text)
        {
            // Presume that only one set of (xx) exists in the string.
            string[] separatedString = text.Split('(');

            return separatedString[0];
        }
    }
}
