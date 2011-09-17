using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public static class TextboxTagExtensions
    {
        public static HtmlTag Watermark(this HtmlTag textbox, string watermark)
        {
            return textbox
                .Attr("onfocus", "javascript:SmartTrack.Scripts.Watermark.inputFocus($(this));")
                .Attr("onblur", "javascript:SmartTrack.Scripts.Watermark.inputBlur($(this));")
                .WrapWith(new HtmlTag("div"))
                    .Style("position", "relative")
                    .Style("display", "inline")
                .Append(new HtmlTag("span")
                    .AddClass("watermark")
                    .Style("position", "absolute")
                    .Style("left", "15px")
                    .Style("top", "2px")
                    .Text(" " + watermark + " ")
                    .Attr("onclick", "javascript:SmartTrack.Scripts.Watermark.spanFocus($(this));")
                    .Attr("onfocus", "javascript:SmartTrack.Scripts.Watermark.spanFocus($(this));")
                    .Attr("onblur", "javascript:SmartTrack.Scripts.Watermark.spanBlur($(this));")
                );
        }
    }
}