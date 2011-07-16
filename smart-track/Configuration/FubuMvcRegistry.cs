using FubuMVC.Core;
using SmartTrack.Web.Controllers;
using SmartTrack.Web.Http.Output;
using SmartTrack.Web.Utils.Extensions;

namespace SmartTrack.Web.Configuration
{
    public class FubuMvcRegistry : FubuRegistry
    {
        public FubuMvcRegistry()
        {
            IncludeDiagnostics(true);
            
            Applies.ToThisAssembly();

            Actions.IncludeClassesSuffixedWithController();

            Routes
                .HomeIs<LoginInputModel>()
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html");
                //.ConstrainToHttpMethod(action => action.Method.Name.EndsWith("Command"), "POST")
                //.ConstrainToHttpMethod(action => action.Method.Name.StartsWith("Query"), "GET")
                //.RootAtAssemblyNamespace();

            // Match views to action methods by matching
            // on model type, view name, and namespace
            this.UseSpark();

            Views
                .TryToAttach(x =>
                {
                    x.by_ViewModel_and_Namespace_and_MethodName();
                    x.by_ViewModel_and_Namespace();
                    x.by_ViewModel();
                })
                .TryToAttachWithDefaultConventions();

            Output.ToJson.WhenTheOutputModelIs<JsonResponse>();

            /*
            
            Views
                .TryToAttach(x=>
                {
                    x.to_spark_view_by_action_namespace_and_name(GetType().Namespace);
                    x.by_ViewModel_and_Namespace_and_MethodName();
                    x.by_ViewModel_and_Namespace();
                    x.by_ViewModel();
                });
                         
             */
        }
    }
}