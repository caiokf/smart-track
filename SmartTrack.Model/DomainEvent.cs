using System;

namespace SmartTrack.Model
{
    public class DomainEvent
    {
        public virtual Guid Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual DateTime DateTime { get; set; }
        public virtual string Event { get; set; }
        public virtual string EventType { get; set; }
    }
}