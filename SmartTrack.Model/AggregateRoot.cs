using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SmartTrack.Model
{
    public abstract class AggregateRoot : IEntity
    {
        public virtual Guid Id { get; protected set; }

        public virtual void Hydrate(IEnumerable<IDomainEvent> events)
        {
            events.ToList().ForEach(Apply);
        }

        public virtual void Apply(IDomainEvent e)
        {
            var eventType = e.GetType();
            var castedEvent = Convert.ChangeType(e, eventType);

            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            var methods = GetType().GetMethods(bindingFlags)
                .Where(x => x.GetParameters().Count() > 0)
                .Where(x => x.Name.Contains("Apply"))
                .Where(x => x.GetParameters()[0].ParameterType == eventType)
                .ToList();

            if (methods.Count() <= 0)
                throw new ArgumentException(string.Format("Event '{0}' cannot be processed by this aggregate", e.GetType()));

            methods.First().Invoke(this, bindingFlags, null, new[] {castedEvent}, null);
        } 
    }
}