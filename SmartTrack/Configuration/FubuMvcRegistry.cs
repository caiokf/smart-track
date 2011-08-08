using FubuMVC.Core;
using FubuMVC.WebForms;
using SmartTrack.Web.Controllers.Measures;
using SmartTrack.Web.Http.Behaviors.Transactions;
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

            Policies.WrapBehaviorChainsWith<TransactionBehavior>();
            
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