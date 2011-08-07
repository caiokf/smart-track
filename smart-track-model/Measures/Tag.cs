using System;

namespace SmartTrack.Model.Measures
{
    public class Tag
    {
        public Tag(DateTime date, string name)
        {
            Date = date;
            Name = name;
        }

        public string Name { get; private set; }
        public DateTime Date { get; private set; }
    }
}