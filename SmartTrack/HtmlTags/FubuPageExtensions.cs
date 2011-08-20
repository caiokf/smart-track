using System;
using System.Linq.Expressions;
using FubuCore.Reflection;
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
    }
}