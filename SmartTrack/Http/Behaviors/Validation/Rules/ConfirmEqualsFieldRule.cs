using System;
using System.Collections.Generic;
using System.Reflection;
using FubuCore.Reflection;
using FubuValidation;
using FubuValidation.Fields;

namespace SmartTrack.Web.Http.Behaviors.Validation.Rules
{
    public class ConfirmEqualsToAttribute : FieldValidationAttribute
    {
        private readonly string _otherField;

        public ConfirmEqualsToAttribute(string otherField)
        {
            _otherField = otherField;
        }

        public override IEnumerable<IFieldValidationRule> RulesFor(PropertyInfo property)
        {
            return new [] {new ConfirmEqualsFieldRule(_otherField)};
        }
    }
    public class ConfirmEqualsFieldRule : IFieldValidationRule
    {
        private readonly string _otherField;
        public static readonly string OTHER_FIELD = "otherfield";

        public ConfirmEqualsFieldRule(string otherField)
        {
            _otherField = otherField;
        }

        public void Validate(Accessor accessor, ValidationContext context)
        {
            var original = accessor.GetValue(context.Target);
            var confirmation = context.TargetType.GetProperty(OtherField).GetValue(context.Target, null);

            if (!original.Equals(confirmation))
            {
                context.Notification.RegisterMessage(accessor, ValidationKeys.CONFIRM_EQUALS).AddSubstitution(OTHER_FIELD, _otherField);
            }
        }

        public string OtherField
        {
            get { return _otherField; }
        }
    }
}