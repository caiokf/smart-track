using FubuMVC.Core;
using FubuMVC.WebForms;
using SmartTrack.Web.Controllers.Login;
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
                .HomeIs<LoginController>(x => x.Index())
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