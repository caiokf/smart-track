using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuMVC.Core.UI;
using FubuMVC.Core.View;
using HtmlTags;

namespace SmartTrack.Web.HtmlTags
{
    public static class FubuPageExtensions
    {
        public static SubmitButtonTag SubmitButton(this IFubuPage page)
        {
            return new SubmitButtonTag(); 
        }

        public static CancelButtonTag CancelButton(this IFubuPage page) 
        {
            return new CancelButtonTag();
        }

        public static ButtonTag Button(this IFubuPage page) 
        {
            return new ButtonTag();
        }

        public static TextboxTag Textbox(this IFubuPage page)
        {
            return new TextboxTag();
        }

        public static HtmlTag ValidationFor(this IFubuPage page, string id)
        {
            return new ValidationErrorTag(id);
        }

        public static ValidationErrorSummaryTag ErrorSummary(this IFubuPage page)
        {
            return new ValidationErrorSummaryTag();
        }

        public static TextboxTag Password(this IFubuPage page)
        {
            return new TextboxTag().Attr("type", "password") as TextboxTag;
        }

        public static TextboxTag TextBoxFor<T>(this IFubuPage page, T model, Expression<Func<T, object>> expression)
            where T : class
        {
            var value = model.ValueOrDefault(expression);
            return new TextboxTag("", (value == null) ? "" : value.ToString());
        }

        public static HtmlTag LinkWithConfirmationTo(this IFubuPage page, object inputModel, string confirmationMessage)
        {
            var link = page.LinkTo(inputModel);
            var href = link.Attr("href");
            link.Attr("href", " ");

            var onClick = "javascript:linkConfirmation('" + confirmationMessage + "', '" + href + "'); return false;";
            
            link.Attr("onclick", onClick);

            return link;
        }
    }
}