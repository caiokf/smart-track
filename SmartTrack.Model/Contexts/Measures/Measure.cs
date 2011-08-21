using System;
using System.Collections.Generic;
using SmartTrack.Model.Extensions;

namespace SmartTrack.Model.Measures
{
    public class Measure
    {
        public Measure(string name, string unit)
        {
            Name = name;
            Unit = unit;
            values = new List<Measurement>();
        }

        private readonly List<Measurement> values; 
        
        public string Name { get; private set; }
        public string Unit { get; private set; }

        public IEnumerable<Measurement> Values { get { return values; } }

        public void AddMeasurement(DateTime date, string value)
        {
            values.Add(new Measurement(date, value));
        }

        public void ChangeUnitTo(string unit)
        {
            if (!unit.IsNullOrEmpty()) Unit = unit;
        }

        public void ChangeNameTo(string name)
        {
            if (!name.IsNullOrEmpty()) Name = name;
        }
    }

    public class Measurement
    {
        public string Value { get; private set; }
        public DateTime Date { get; private set; }

        public Measurement(DateTime date, string value)
        {
            Date = date;
            Value = value;
        }
    }
}