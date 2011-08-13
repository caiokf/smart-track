using System;
using SmartTrack.Model.Extensions;

namespace SmartTrack.Model.Measures
{
    public class MeasureCreated : IDomainEvent
    {
        public string Measure { get; set; }
        public string Unit { get; set; }

        public bool IsValid() { return (!Measure.IsNullOrEmpty() && !Unit.IsNullOrEmpty()); }
    }

    public class MeasureEdited : IDomainEvent
    {
        public string OldMeasure { get; set; }
        public string NewMeasure { get; set; }
        public string Unit { get; set; }

        public bool IsValid() { return (!NewMeasure.IsNullOrEmpty() && !OldMeasure.IsNullOrEmpty() && !Unit.IsNullOrEmpty()); }
    }

    public class MeasureDeleted : IDomainEvent
    {
        public string Measure { get; set; }

        public bool IsValid() { return (!Measure.IsNullOrEmpty()); }
    }

    public class MeasureAdded : IDomainEvent
    {
        public string Measure { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }

        public bool IsValid() { return (!Measure.IsNullOrEmpty() && !Unit.IsNullOrEmpty() && !Value.IsNullOrEmpty()); }
    }

    public class TagAdded : IDomainEvent
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }

        public bool IsValid() { return (!Tag.IsNullOrEmpty()); }
    }

    public class TagDeleted : IDomainEvent
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }

        public bool IsValid() { return (!Tag.IsNullOrEmpty()); }
    }

    public class GroupAdded : IDomainEvent
    {
        public string Group { get; set; }

        public bool IsValid() { return (!Group.IsNullOrEmpty()); }
    }

    public class GroupDeleted : IDomainEvent
    {
        public string Group { get; set; }

        public bool IsValid() { return (!Group.IsNullOrEmpty()); }
    }

    public class MeasureAddedToGroup : IDomainEvent
    {
        public string Group { get; set; }
        public string Measure { get; set; }

        public bool IsValid() { return (!Group.IsNullOrEmpty() && !Measure.IsNullOrEmpty()); }
    }

    public class MeasureRemovedFromGroup : IDomainEvent
    {
        public string Group { get; set; }
        public string Measure { get; set; }

        public bool IsValid() { return (!Group.IsNullOrEmpty() && !Measure.IsNullOrEmpty()); }
    }
}