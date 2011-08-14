using HtmlTags;
using HtmlTags.Extended.Attributes;

namespace SmartTrack.Web.HtmlTags
{
    public static class TextboxTagExtensions
    {
        public static HtmlTag Watermark(this TextboxTag textbox, string watermark)
        {
            return textbox
                .Attr("onfocus", "javascript:Watermark.focus($(this),'" + watermark + "');")
                .Attr("onblur", "javascript:Watermark.blur($(this),'" + watermark + "');")
                .AddClass("watermark");
        }
    }
}