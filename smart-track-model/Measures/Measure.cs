using System;
using System.Collections.Generic;

namespace SmartTrack.Model.Measures
{
    public class Measure
    {
        public Measure(string name)
        {
            Name = name;
            values = new List<Measurement>();
        }

        private readonly List<Measurement> values; 
        
        public string Name { get; private set; }
        public IEnumerable<Measurement> Values { get { return values; } }

        public void AddMeasurement(DateTime date, string value, string unit)
        {
            values.Add(new Measurement(date, value, unit));
        }
    }

    public class Measurement
    {
        public string Value { get; private set; }
        public string Unit { get; private set; }
        public DateTime Date { get; private set; }

        public Measurement(DateTime date, string value, string unit)
        {
            Date = date;
            Value = value;
            Unit = unit;
        }
    }
}