using System;
using FubuMVC.Core;
using FubuMVC.Core.Registration.DSL;
using FubuMVC.Core.Registration.Nodes;
using FubuMVC.Core.Registration.Routes;
using FubuMVC.Core.Urls;
using FubuMVC.Core.View;
using FubuMVC.Validation;
using FubuMVC.WebForms;
using SmartTrack.Web.Controllers.Measures;
using SmartTrack.Web.Http.Behaviors.ActionlessViews;
using SmartTrack.Web.Http.Behaviors.Validation;
using SmartTrack.Web.Http.Output;
using FubuMVC.Core.Registration;

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
            
            //Policies.WrapBehaviorChainsWith<TransactionBehavior>();
            Policies.Add<ValidationPolicy>();

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