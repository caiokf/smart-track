using System;
using System.Collections.Generic;
using System.Linq;
using SmartTrack.Model.Extensions;

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

        public User(string name, string password, string email) : this()
        {
            Password = password;
            Email = email;
            Name = name;
        }

        public virtual string Name { get; private set; }
        public virtual string Email { get; private set; }
        public virtual string Password { get; private set; }

        private readonly List<Measure> measures;
        public virtual IEnumerable<Measure> Measures { get { return measures; } }

        private readonly List<Tag> tags;
        public virtual IEnumerable<Tag> Tags { get { return tags; } }

        private readonly List<Group> groups;
        public virtual IEnumerable<Group> Groups { get { return groups; } }

        protected void Apply(MeasureAdded e)
        {
            var measure = Measures.WithName(e.Measure);
            if (measure == null)
            {
                measure = new Measure(e.Measure, e.Unit);
                measures.Add(measure);
            }
            measure.AddMeasurement(e.Date, e.Value);
        }

        protected void Apply(MeasureCreated e)
        {
            var measure = Measures.WithName(e.Measure);
            if (measure == null)
            {
                measure = new Measure(e.Measure, e.Unit);
                measures.Add(measure);
            }
        }

        protected void Apply(MeasureEdited e)
        {
            var measure = Measures.WithName(e.OldMeasure);
            if (measure == null)
                return;
            measure.ChangeNameTo(e.NewMeasure);
            measure.ChangeUnitTo(e.Unit);
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
            var group = groups.WithName(e.Group);
            if (group == null)
                groups.Add(new Group(e.Group));
        }

        protected void Apply(GroupDeleted e)
        {
            groups.RemoveAll(x => x.Name == e.Group);
        }

        protected void Apply(MeasureAddedToGroup e)
        {
            var measure = Measures.WithName(e.Measure);
            var group = Groups.WithName(e.Group);
            group.AddMeasure(measure);
        }

        protected void Apply(MeasureRemovedFromGroup e)
        {
            var measure = Measures.WithName(e.Measure);
            var group = Groups.WithName(e.Group);
            group.RemoveMeasure(measure);
        }
	}

}
