using System;

namespace SmartTrack.Model.Measures
{
    public class MeasureCreated : IDomainEvent
    {
        public string Measure { get; set; }
        public string Unit { get; set; }

        public bool IsValid() { return (Measure != null && Unit != null); }
    }

    public class MeasureEdited : IDomainEvent
    {
        public string OldMeasure { get; set; }
        public string NewMeasure { get; set; }
        public string Unit { get; set; }

        public bool IsValid() { return (NewMeasure != null && OldMeasure != null && Unit != null); }
    }

    public class MeasureDeleted : IDomainEvent
    {
        public string Measure { get; set; }

        public bool IsValid() { return (Measure != null); }
    }

    public class MeasureAdded : IDomainEvent
    {
        public string Measure { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }

        public bool IsValid() { return (Measure != null && Unit != null && Value != null); }
    }

    public class TagAdded : IDomainEvent
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }

        public bool IsValid() { return (Tag != null); }
    }

    public class TagDeleted : IDomainEvent
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }

        public bool IsValid() { return (Tag != null); }
    }

    public class GroupAdded : IDomainEvent
    {
        public string Group { get; set; }

        public bool IsValid() { return (Group != null); }
    }

    public class GroupDeleted : IDomainEvent
    {
        public string Group { get; set; }

        public bool IsValid() { return (Group != null); }
    }

    public class MeasureAddedToGroup : IDomainEvent
    {
        public string Group { get; set; }
        public string Measure { get; set; }

        public bool IsValid() { return (Group != null && Measure != null); }
    }

    public class MeasureRemovedFromGroup : IDomainEvent
    {
        public string Group { get; set; }
        public string Measure { get; set; }

        public bool IsValid() { return (Group != null && Measure != null); }
    }
}