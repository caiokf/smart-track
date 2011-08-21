using System.Collections.Generic;
using SmartTrack.Model.Extensions;

namespace SmartTrack.Model.Measures
{
    public class Group
    {
        public string Name { get; set; }

        public Group(string name)
        {
            Name = name;
            measures = new List<Measure>();
            tags = new List<Tag>();
        }

        private readonly List<Measure> measures; 
        public IEnumerable<Measure> Measures { get { return measures; } }

        private readonly List<Tag> tags;
        public IEnumerable<Tag> Tags { get { return tags; } }

        public void AddMeasure(Measure measure)
        {
            measures.Add(measure);
        }

        public void RemoveMeasure(Measure measure)
        {
            measures.Remove(measure);
        }

        public void ChangeNameTo(string name)
        {
            if (!name.IsNullOrEmpty()) Name = name;
        }

        public void AddTag(Tag tag)
        {
            tags.Add(tag);
        }

        public void RemoveTag(Tag tag)
        {
            tags.Remove(tag);
        }
    }
}