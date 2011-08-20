using HtmlTags;
using HtmlTags.Extended.Attributes;

namespace SmartTrack.Web.HtmlTags
{
    public class CancelButtonTag : HtmlTag
    {
        public CancelButtonTag() : base("input")
        {
            this.Attr("type", "submit")
                .Value("Cancel")
                .AddClass("cancel-button")
                .Attr("onclick", "javascript:history.back();");
        } 
    }
}