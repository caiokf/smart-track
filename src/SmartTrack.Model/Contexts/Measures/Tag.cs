using System;

namespace SmartTrack.Model.Measures
{
    public class Tag
    {
        public Tag(string name, DateTime startDate)
        {
            StartDate = startDate;
            Name = name;
        }

        public Tag(string name, DateTime startDate, DateTime endDate) : this(name, startDate)
        {
            EndDate = endDate;
        }

        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
    }
}