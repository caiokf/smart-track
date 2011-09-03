using System.Collections.Generic;
using FubuValidation;

namespace SmartTrack.Web.Behaviors.Validation
{
    public interface IValidate<in T> where T : class
    {
        IEnumerable<NotificationMessage> Validate(T objectToValidate);
    }
}