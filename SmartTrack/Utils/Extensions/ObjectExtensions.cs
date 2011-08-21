using HtmlTags;

namespace SmartTrack.Web.Utils.Extensions
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object toSerialize)
        {
            return JsonUtil.ToJson(toSerialize);
        }
    }
}