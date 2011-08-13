using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Model;
using SmartTrack.Model.Measures;

namespace SmartTrack.Tests.Unit.Persistance.Measures
{
    public class FakeEvent : IDomainEvent { public bool IsValid() { return true; } }

    [TestFixture]
    public class MeasurementEvents
    {
        [Test]
        public void should_not_receive_events_not_subscribed_to()
        {
            var user = new User("any");

            Assert.Throws<ArgumentException>(() => user.Apply(new FakeEvent()));
        }

        [Test]
        public void add_one_measure()
        {
            var user = new User("any");

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            
            user.Measures.Count().Should().Be(1);
            user.Measures.Select(x => x.Name).Should().Have.SameValuesAs(new[] { "Biceps" });
            user.Measures.Single(x => x.Name == "Biceps").Values.Count().Should().Be(1);
        }

        [Test]
        public void add_existing_measure()
        {
            var user = new User("any");

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            user.Apply(new MeasureAdded { Date = DateTime.Today, Measure = "Biceps", Value = "40", Unit = "cm" });

            user.Measures.Count().Should().Be(1);
            user.Measures.Select(x => x.Name).Should().Have.SameValuesAs(new[] { "Biceps" });
            user.Measures.Single(x => x.Name == "Biceps").Values.Count().Should().Be(2);
        }

        [Test]
        public void add_more_than_one_measure()
        {
            var user = new User("any");

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            user.Apply(new MeasureAdded { Date = DateTime.Today, Measure = "Calf", Value = "38", Unit = "cm" });
            user.Apply(new MeasureAdded { Date = DateTime.Today, Measure = "Biceps", Value = "41", Unit = "cm" });

            user.Measures.Count().Should().Be(2);
            user.Measures.Select(x => x.Name).Should().Have.SameValuesAs(new [] {"Biceps", "Calf"});
            user.Measures.Single(x => x.Name == "Biceps").Values.Count().Should().Be(2);
        }

        [Test]
        public void remove_existing_measures()
        {
            var user = new User("any");

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            user.Apply(new MeasureDeleted { Measure = "Biceps" });
            
            user.Measures.Count().Should().Be(0);
        }

        [Test]
        public void remove_non_existing_measures()
        {
            var user = new User("any");

            user.Apply(new MeasureDeleted { Measure = "Biceps" });
            
            user.Measures.Count().Should().Be(0);
        }
    }
}