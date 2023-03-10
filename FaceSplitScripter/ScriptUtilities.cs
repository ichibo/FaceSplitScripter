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
    }
}
