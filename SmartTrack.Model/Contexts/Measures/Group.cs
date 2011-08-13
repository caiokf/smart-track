using System.Collections.Generic;

namespace SmartTrack.Model.Measures
{
    public class Group
    {
        public string Name { get; set; }

        public Group(string name)
        {
            Name = name;
            measures = new List<Measure>();
        }

        private readonly List<Measure> measures; 

        public IEnumerable<Measure> Measures { get { return measures; } }

        public void AddMeasure(Measure measure)
        {
            measures.Add(measure);
        }

        public void RemoveMeasure(Measure measure)
        {
            measures.Remove(measure);
        }
    }
}