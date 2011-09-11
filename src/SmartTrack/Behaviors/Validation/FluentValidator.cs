using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using FubuCore;
using FubuCore.Reflection;
using FubuLocalization;
using FubuValidation;
using StructureMap;
using IValidator = FubuValidation.IValidator;
using ValidationContext = FubuValidation.ValidationContext;

namespace SmartTrack.Web.Behaviors.Validation
{
    public class FluentValidator : IValidator
    {
        private readonly IContainer container;
        private readonly ITypeResolver typeResolver;

        public FluentValidator(IContainer container, ITypeResolver typeResolver)
        {
            this.container = container;
            this.typeResolver = typeResolver;
        }

        public Notification Validate(object target)
        {
            var validatedType = typeResolver.ResolveType(target);
            var notification = new Notification(validatedType);
            Validate(target, notification);
            return notification;
        }

        public void Validate(object target, Notification notification)
        {
            var validatedType = typeResolver.ResolveType(target);
            var validatorType = typeof (IValidator<>).MakeGenericType(validatedType);
            var validators = container.GetAllInstances(validatorType).Cast<dynamic>();
            validators.Select<dynamic, ValidationResult>(x => x.Validate((dynamic)target))
                .SelectMany(x => x.Errors)
                .Each(x =>
                {
                    var token = StringToken.FromKeyString("", x.ErrorMessage);
                    var property = validatedType.GetProperty(x.PropertyName);
                    if (property != null)
                        notification.RegisterMessage(property, token);
                    else
                        notification.RegisterMessage(token);
                });
        }
    }
}