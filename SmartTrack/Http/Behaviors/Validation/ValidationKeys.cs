using FubuCore;
using FubuLocalization;
using FubuValidation;
using SmartTrack.Web.Http.Behaviors.Validation.Rules;

namespace SmartTrack.Web.Http.Behaviors.Validation
{
    public class ValidationKeys : StringToken
    {
        public static readonly StringToken CONFIRM_EQUALS = new ValidationKeys("CONFIRM_EQUALS", "Doesn't match {0}".ToFormat(ConfirmEqualsFieldRule.OTHER_FIELD.AsTemplateField()));
        
        public ValidationKeys(string key) : this(key, null) { }
        public ValidationKeys(string key, string default_EN_US_Text) : base(key, default_EN_US_Text) { }
    }
}