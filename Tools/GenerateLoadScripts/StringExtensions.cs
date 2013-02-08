namespace GenerateLoadScripts
{
    public static class StringExtensions
    {
        public static string Encode ( this string str )
        {
            string retStr = str.Replace ( "'", "''" );
            return retStr;
        }
    }
}