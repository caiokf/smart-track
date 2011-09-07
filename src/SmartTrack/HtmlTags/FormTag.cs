using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public static class FormTagExtensions
    {
        public static FormTag WithAjaxValidation(this FormTag form)
        {
            return (FormTag)form.AddClass("form-ajax-validate-and-submit");
        }
    }
}