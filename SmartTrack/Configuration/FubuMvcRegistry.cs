using System;
using FubuMVC.Core;
using FubuMVC.Core.Registration.DSL;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using FubuMVC.WebForms;
using SmartTrack.Web.Controllers.Measures;
using SmartTrack.Web.Http.Output;

namespace SmartTrack.Web.Configuration
{
    public class FubuMvcRegistry : FubuRegistry
    {
        public FubuMvcRegistry()
        {
            IncludeDiagnostics(true);

            Applies.ToThisAssembly();

            Import<WebFormsEngine>();

            Actions.IncludeClassesSuffixedWithController();

            //Policies.WrapBehaviorChainsWith<TransactionBehavior>();
            
            Routes
                .HomeIs<MeasuresController>(x => x.AllMeasures())
                .IgnoreControllerNamespaceEntirely()
                .IgnoreClassSuffix("Controller")
                .IgnoreMethodSuffix("Html")
                .ConstrainToHttpMethod(action => action.Method.Name.EndsWith("Command"), "POST")
                .ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Query"), "GET");

            Views.TryToAttachWithDefaultConventions()
                .RegisterWebFormsActionLessViews();

            Output.ToJson.WhenTheOutputModelIs<JsonResponse>();
        }
    }

    public interface IAmActionless { }

    public static class RegisterActionlessViewsConvention
    {
        public static ViewExpression RegisterWebFormsActionLessViews(this ViewExpression views)
        {
            Func<IViewToken,bool> filter = x => typeof (IAmActionless).IsAssignableFrom(x.ViewType);
            Func<BehaviorChain, string> routeFrom = x =>
            {
                var route = x.Top.ToString();
                route = route.Substring(route.IndexOf("/Controllers") + 13);
                route = route.Replace(".aspx", "");
                route = route.Replace(".ascx", "");
                route = route.Replace("'", "");
                route = route.ToLower();
                return route;
            };
            return views.RegisterActionLessViews(filter, chain => chain.Route = new RouteDefinition(routeFrom(chain)));
        }

        public static string UrlForActionless<T>(this IUrlRegistry urls) where T : IAmActionless
        {
            var type = typeof (T).FullName;
            var action = type.Replace("SmartTrack.Web.Controllers", "").Replace(".", "/").ToLower();
            return action;
        }
    }
}