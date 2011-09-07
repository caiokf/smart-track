namespace SmartTrack.Model.Extensions
{
    public static class StringExtensions
    {
         public static bool IsNullOrEmpty(this string s)
         {
             return string.IsNullOrWhiteSpace(s);
         }
    }
}