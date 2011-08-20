using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public class ButtonTag : HtmlTag
    {
        public ButtonTag() : base("input")
        {
            this.Attr("type", "button");
        } 
    }
}