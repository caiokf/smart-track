using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public class SubmitButtonTag : HtmlTag
    {
        public SubmitButtonTag() : base("input")
        {
            this.Attr("type", "submit");
        }
    }
}