using System.Collections.Generic;
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

        public virtual string Name { get; protected set; }
        public virtual string Email { get; protected set; }
        public virtual string Password { get; protected set; }

        private readonly List<Measure> measures;
        public virtual IEnumerable<Measure> Measures { get { return measures; } }

        private readonly List<Tag> tags;
        public virtual IEnumerable<Tag> Tags { get { return tags; } }

        private readonly List<Group> groups;
        public virtual IEnumerable<Group> Groups { get { return groups; } }

        protected virtual void Apply(MeasureAdded e)
        {
            var measure = Measures.WithName(e.Measure);
            if (measure == null)
            {
                measure = new Measure(e.Measure, e.Unit);
                measures.Add(measure);
            }
            measure.AddMeasurement(e.Date, e.Value);
        }

        protected virtual void Apply(MeasureCreated e)
        {
            var measure = Measures.WithName(e.Measure);
            if (measure == null)
            {
                measure = new Measure(e.Measure, e.Unit);
                measures.Add(measure);
            }
        }

        protected virtual void Apply(MeasureEdited e)
        {
            var measure = Measures.WithName(e.OldMeasure);
            if (measure == null)
                return;

            var emptyName = e.NewMeasure.IsNullOrEmpty();
            var existingName = Measures.WithName(e.NewMeasure) != null;

            if (!emptyName && !existingName) measure.ChangeNameTo(e.NewMeasure);
            if (!e.Unit.IsNullOrEmpty()) measure.ChangeUnitTo(e.Unit);
        }

        protected virtual void Apply(MeasureDeleted e)
        {
            measures.RemoveAll(x => x.Name.ToLower() == e.Measure.ToLower());
        }

        protected virtual void Apply(TagAdded e)
        {
            var tag = Tags.WithNameAndDate(e.Tag, e.StartDate);
            if (tag == null)
                tags.Add(new Tag(e.Tag, e.StartDate));
        }

        protected virtual void Apply(TagDeleted e)
        {
            tags.RemoveAll(x => x.Name.ToLower() == e.Tag.ToLower() && x.StartDate.Date == e.StartDate.Date);
        }

        protected virtual void Apply(GroupAdded e)
        {
            var group = groups.WithName(e.Group);
            if (group == null)
                groups.Add(new Group(e.Group));
        }

        protected virtual void Apply(GroupDeleted e)
        {
            groups.RemoveAll(x => x.Name == e.Group);
        }

        protected virtual void Apply(GroupEdited e)
        {
            var group = Groups.WithName(e.OldGroup);
            if (group == null)
                return;

            var emptyName = e.NewGroup.IsNullOrEmpty();
            var existingName = Groups.WithName(e.NewGroup) != null;

            if (!emptyName && !existingName) group.ChangeNameTo(e.NewGroup);
        }

        protected virtual void Apply(MeasureAddedToGroup e)
        {
            var measure = Measures.WithName(e.Measure);
            var group = Groups.WithName(e.Group);

            if (group != null && measure != null)
                group.AddMeasure(measure);
        }

        protected virtual void Apply(MeasureRemovedFromGroup e)
        {
            var measure = Measures.WithName(e.Measure);
            var group = Groups.WithName(e.Group);

            if (group != null && measure != null)
                group.RemoveMeasure(measure);
        }

        protected virtual void Apply(TagAddedToGroup e)
        {
            var tag = Tags.WithNameAndDate(e.Tag, e.TagStartDate);
            var group = Groups.WithName(e.Group);

            if (group != null && tag != null)
                group.AddTag(tag);
        }

        protected virtual void Apply(TagRemovedFromGroup e)
        {
            var tag = Tags.WithNameAndDate(e.Tag, e.TagStartDate);
            var group = Groups.WithName(e.Group);

            if (group != null && tag != null)
                group.RemoveTag(tag);
        }
	}
}
