using System;
using System.Collections.Generic;
using System.Linq;
using SmartTrack.Model.Measures;

namespace SmartTrack.Model
{
	public class User
	{
        protected User() { }
        public User(string name)
        {
            Name = name;
            Measures = new List<Measure>();
            Tags = new List<Tag>();
            Groups = new List<Group>();
        }

	    public virtual Guid Id { get; protected set; }
        public virtual string Name { get; private set; }
        public virtual List<Measure> Measures { get; set; }
        public virtual List<Tag> Tags { get; set; }
        public virtual List<Group> Groups { get; set; }
        
        public void Apply(MeasureAdded e)
        {
            var measure = Measures.FirstOrDefault(x => x.Name.ToLower() == e.Measure.ToLower());
            if (measure == null)
            {
                measure = new Measure(e.Measure);
                Measures.Add(measure);
            }
            measure.AddMeasurement(e.Date, e.Value, e.Unit);
        }

        public void Apply(MeasureDeleted e)
        {
            Measures.RemoveAll(x => x.Name.ToLower() == e.Measure.ToLower());
        }

        public void Apply(TagAdded e)
        {
            Tags.Add(new Tag(e.Date, e.Tag));
        }

        public void Apply(TagDeleted e)
        {
            Tags.RemoveAll(x => x.Name.ToLower() == e.Tag.ToLower() && x.Date.Date == e.Date.Date);
        }

        public void Apply(GroupAdded e)
        {
            Groups.Add(new Group(e.Group));
        }

        public void Apply(GroupDeleted e)
        {
            Groups.RemoveAll(x => x.Name == e.Group);
        }

        public void Apply(MeasureAddedToGroup e)
        {
            var measure = Measures.Where(x => x.Name.ToLower() == e.Measure.ToLower()).Single();
            var group = Groups.Where(x => x.Name.ToLower() == e.Group.ToLower()).Single();
            group.AddMeasure(measure);
        }

        public void Apply(MeasureRemovedFromGroup e)
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
