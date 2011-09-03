using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FubuCore.Reflection;
using FubuLocalization;
using FubuValidation;

namespace SmartTrack.Web.Behaviors.Validation
{
    public class NotificationMessageErrors<T>
    {
        private readonly List<NotificationMessage> notificationMessages = new List<NotificationMessage>();

        public NotificationMessage Add(Expression<Func<T, object>> property, StringToken message)
        {
            var notification = new NotificationMessage(message);
            notification.AddAccessor(property.ToAccessor());
            notificationMessages.Add(notification);
            return notification;
        }

        public List<NotificationMessage> ToList()
        {
            return notificationMessages;
        }
    }
}