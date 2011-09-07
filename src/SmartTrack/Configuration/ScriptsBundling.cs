using SquishIt.Framework.JavaScript;

namespace SquishIt.Framework
{
    public static class ScriptsBundling
    {
        public static IJavaScriptBundle Add_JQuery(this IJavaScriptBundle bundle)
        {
            bundle
                .Add("~/Content/scripts/jquery/jquery-1.6.2.min.js")
                .Add("~/Content/scripts/jquery/jquery.form.js")
                .Add("~/Content/scripts/jquery/jquery.validate.js")
                .Add("~/Content/scripts/jquery/jquery.alerts.js")
                .Add("~/Content/scripts/jquery/jquery.autocomplete.min.js")
                .Add("~/Content/scripts/jquery/jquery.cookie.js")
                .Add("~/Content/scripts/jquery/jquery.validate.js")
                .Add("~/Content/scripts/jquery/jquery.jgrowl_minimized.js")
                .Add("~/Content/scripts/jquery/jquery.linq.js")
                .Add("~/Content/scripts/jquery/jquery.loading.1.6.4.min.js")
                .Add("~/Content/scripts/jquery/jquery.loading.overflow.min.js")
                .Add("~/Content/scripts/jquery/jquery.templates-1.0.js")
                .Add("~/Content/scripts/jquery/jquery.tools.min.js");
            return bundle;
        }

        public static IJavaScriptBundle Add_JQueryUi(this IJavaScriptBundle bundle)
        {
            bundle.Add("~/Content/scripts/jquery-ui/jquery-ui-1.8.14.min.js");
            return bundle;
        }

        public static IJavaScriptBundle Add_LinqJs(this IJavaScriptBundle bundle)
        {
            bundle.Add("~/Content/scripts/linq-js/linq.min.js");
            return bundle;
        }

        public static IJavaScriptBundle Add_Underscore(this IJavaScriptBundle bundle)
        {
            bundle.Add("~/Content/scripts/underscore/underscore.js");
            return bundle;
        }

        public static IJavaScriptBundle Add_LessHandler(this IJavaScriptBundle bundle)
        {
            bundle.Add("~/Content/scripts/less-1.1.3.min.js");
            return bundle;
        }
    }
}