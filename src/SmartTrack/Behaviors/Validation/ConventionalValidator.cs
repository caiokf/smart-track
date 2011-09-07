using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FubuCore;
using FubuValidation;
using StructureMap;
using IValidator = FubuValidation.IValidator;
using ValidationContext = FubuValidation.ValidationContext;

namespace SmartTrack.Web.Behaviors.Validation
{
    public class ConventionalValidator : IValidator
    {
        private readonly IContainer container;
        private readonly ITypeResolver typeResolver;

        public ConventionalValidator(IContainer container, ITypeResolver typeResolver)
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
            var context = new ValidationContext(this, notification, target) 
            {
                TargetType = validatedType,
                Resolver = typeResolver
            };
            
            var validatorType = typeof (IValidator<>).MakeGenericType(validatedType);
            var validators = container.GetAllInstances(validatorType).Cast<dynamic>();
            var notificationMessages = validators.SelectMany<dynamic, NotificationMessage>(x => x.Validate((dynamic)target));
            notificationMessages.Each(x => context.Notification.RegisterMessage(x));
        }
    }
}