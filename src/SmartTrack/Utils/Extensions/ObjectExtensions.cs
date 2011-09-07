using HtmlTags;

namespace SmartTrack.Web.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object toSerialize)
        {
            return JsonUtil.ToJson(toSerialize);
        }

        public static bool IsEmpty(this object obj)
        {
            return (obj == null) || (string.IsNullOrWhiteSpace((string)obj));
        }
    }
}