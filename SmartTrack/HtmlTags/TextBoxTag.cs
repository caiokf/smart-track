using HtmlTags;
using HtmlTags.Extended.Attributes;

namespace SmartTrack.Web.HtmlTags
{
    public static class TextboxTagExtensions
    {
        public static string Watermark(this HtmlTag textbox, string watermark)
        {
            return textbox
                .Attr("onfocus", "javascript:Watermark.focusInput($(this));")
                .Attr("onblur", "javascript:Watermark.blurInput($(this));")
                .WrapWith(new HtmlTag("div"))
                    .Style("position", "relative")
                    .Style("display", "inline")
                .Append(new HtmlTag("span")
                    .AddClass("watermark")
                    .Style("position", "absolute")
                    .Style("left", "15px")
                    .Style("top", "2px")
                    .Text(" " + watermark + " ")
                    .Attr("onclick", "javascript:Watermark.focusSpan($(this));")
                    .Attr("onfocus", "javascript:Watermark.focusSpan($(this));")
                    .Attr("onblur", "javascript:Watermark.blurSpan($(this));")
                ).ToString();
        }
    }
}