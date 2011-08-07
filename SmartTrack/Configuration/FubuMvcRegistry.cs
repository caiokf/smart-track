using System.Net;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.WebForms;
using SmartTrack.Web.Controllers;
using SmartTrack.Web.Controllers.Login;
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

            Routes
                .HomeIs<MeasuresController>(x => x.AllMeasures())
                .IgnoreControllerNamespaceEntirely()
                .IgnoreClassSuffix("Controller")
                .IgnoreMethodSuffix("Html")
                .IgnoreMethodSuffix("Post")
                .IgnoreMethodSuffix("Get")
                .ConstrainToHttpMethod(action => action.Method.Name.EndsWith("Command"), "POST")
                .ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Query"), "GET");
            
            Views.TryToAttachWithDefaultConventions();

            Output.ToJson.WhenTheOutputModelIs<JsonResponse>();
        }
    }
}