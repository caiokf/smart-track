using FubuMVC.Core;
using FubuMVC.Validation;
using FubuMVC.WebForms;
using FubuValidation;
using SmartTrack.Web.Behaviors.Validation;
using SmartTrack.Web.Controllers.Measures.Measures;
using SmartTrack.Web.Http.Behaviors.ActionlessViews;
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

            Actions.IncludeClassesSuffixedWithController().FindWith<JsonActionSource>();

            this.Validation();
            
            Policies
                //.WrapBehaviorChainsWith<TransactionBehavior>()
                .Add<ValidationPolicy>();

            Services(x => x.ReplaceService<IValidator, ConventionalValidator>());

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
}