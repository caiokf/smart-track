using System;

namespace SmartTrack.Model
{
    public class DomainEvent
    {
        public virtual Guid Id { get; set; }
        public virtual Guid UserId { get; set; }
        public virtual Guid AggregateId { get; set; }
        public virtual string AggreagateType { get; set; }
        public virtual string Event { get; set; }
    }
}