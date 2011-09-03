using System;
using System.Linq;
using FubuMVC.Core.Runtime;
using FubuMVC.Validation;
using FubuValidation;
using SmartTrack.Web.Http.Output;

namespace SmartTrack.Web.Behaviors.Validation
{
    public class JsonValidationFailurePolicy : IValidationFailurePolicy
    {
        private readonly IFubuRequest _request;
        private readonly IPartialFactory _factory;

        public JsonValidationFailurePolicy(IFubuRequest request, IPartialFactory factory)
        {
            _request = request;
            _factory = factory;
        }

        public bool Matches(Type modelType)
        {
            return true;
        }

        public void Handle(Type modelType, Notification notification)
        {
            var jsonResponse = new JsonResponse();
            jsonResponse.RegisterErrors(notification
                .ToValidationErrors()
                .Where(x => !string.IsNullOrWhiteSpace(x.message)));

            _request.Set(jsonResponse);

            _factory
                .BuildPartial(jsonResponse.GetType())
                .InvokePartial();
        }
    }
}