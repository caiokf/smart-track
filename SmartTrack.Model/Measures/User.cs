using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartTrack.Model.Measures
{
    public class User : AggregateRoot
    {
        protected User()
        {
            measures = new List<Measure>();
            tags = new List<Tag>();
            groups = new List<Group>();
        }

        public User(string name) : this()
        {
            Name = name;
        }
        
        public virtual string Name { get; private set; }

        private readonly List<Measure> measures;
        public virtual IEnumerable<Measure> Measures { get { return measures; } }

        private readonly List<Tag> tags;
        public virtual IEnumerable<Tag> Tags { get { return tags; } }

        private readonly List<Group> groups;
        public virtual IEnumerable<Group> Groups { get { return groups; } }

        protected void Apply(MeasureAdded e)
        {
            var measure = Measures.FirstOrDefault(x => x.Name.ToLower() == e.Measure.ToLower());
            if (measure == null)
            {
                measure = new Measure(e.Measure);
                measures.Add(measure);
            }
            measure.AddMeasurement(e.Date, e.Value, e.Unit);
        }

        protected void Apply(MeasureDeleted e)
        {
            measures.RemoveAll(x => x.Name.ToLower() == e.Measure.ToLower());
        }

        protected void Apply(TagAdded e)
        {
            tags.Add(new Tag(e.Date, e.Tag));
        }

        protected void Apply(TagDeleted e)
        {
            tags.RemoveAll(x => x.Name.ToLower() == e.Tag.ToLower() && x.Date.Date == e.Date.Date);
        }

        protected void Apply(GroupAdded e)
        {
            groups.Add(new Group(e.Group));
        }

        protected void Apply(GroupDeleted e)
        {
            groups.RemoveAll(x => x.Name == e.Group);
        }

        protected void Apply(MeasureAddedToGroup e)
        {
            var measure = Measures.Where(x => x.Name.ToLower() == e.Measure.ToLower()).Single();
            var group = Groups.Where(x => x.Name.ToLower() == e.Group.ToLower()).Single();
            group.AddMeasure(measure);
        }

        protected void Apply(MeasureRemovedFromGroup e)
        {
            var measure = Measures.Where(x => x.Name.ToLower() == e.Measure.ToLower()).Single();
            var group = Groups.Where(x => x.Name.ToLower() == e.Group.ToLower()).Single();
            group.RemoveMeasure(measure);
        }
	}

    public class MeasureAdded : IDomainEvent
    {
        public string Measure { get; set; }
        public DateTime Date { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }

    public class MeasureDeleted : IDomainEvent
    {
        public string Measure { get; set; }
    }

    public class TagAdded : IDomainEvent
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }
    }

    public class TagDeleted : IDomainEvent
    {
        public string Tag { get; set; }
        public DateTime Date { get; set; }
    }

    public class GroupAdded : IDomainEvent
    {
        public string Group { get; set; }
    }

    public class GroupDeleted : IDomainEvent
    {
        public string Group { get; set; }
    }

    public class MeasureAddedToGroup : IDomainEvent
    {
        public string Group { get; set; }
        public string Measure { get; set; }
    }

    public class MeasureRemovedFromGroup : IDomainEvent
    {
        public string Group { get; set; }
        public string Measure { get; set; }
    }
}
