namespace PSPlus.Core.Text
{
    public static class StringExtensions
    {
        private static char[] s_wildcardChars = new char[] { '?', '*' };

        public static bool ContainsWildcardPattern(this string s)
        {
            return s.IndexOfAny(s_wildcardChars) >= 0;
        }
    }
}
