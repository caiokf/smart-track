using System.Text.RegularExpressions;

namespace SmartTrack.Web.Validation.Rules
{
    public static class StringExtensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(s);
        } 
    }
}