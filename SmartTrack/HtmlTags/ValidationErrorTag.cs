using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public class ValidationErrorTag : HtmlTag
    {
        public ValidationErrorTag(string validationForId) : base("div")
        {
            this.Id("validation-for-" + validationForId)
                .AddClass("ui-widget")
                .AddClass("ui-error")
                .AddClass("ui-error-field")
                .Hide()
                .Append("ul");

        }
    }
}