using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Model.Measures;

namespace SmartTrack.Tests.Unit.Measures
{
    [TestFixture]
    public class MeasuresEvents
    {
        [Test]
        public void add_one_measure()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            
            user.Measures.Count().Should().Be(1);
            user.Measures.Select(x => x.Name).Should().Have.SameValuesAs(new[] { "Biceps" });
            user.Measures.Single(x => x.Name == "Biceps").Values.Count().Should().Be(1);
        }

        [Test]
        public void add_existing_measure()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            user.Apply(new MeasureAdded { Date = DateTime.Today, Measure = "Biceps", Value = "40", Unit = "cm" });

            user.Measures.Count().Should().Be(1);
            user.Measures.Select(x => x.Name).Should().Have.SameValuesAs(new[] { "Biceps" });
            user.Measures.Single(x => x.Name == "Biceps").Values.Count().Should().Be(2);
        }

        [Test]
        public void add_more_than_one_measure()
        {
            var user = MotherOf.Users.MrEmpty();

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
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureAdded { Date = DateTime.Today.AddDays(-7), Measure = "Biceps", Value = "39", Unit = "cm" });
            user.Apply(new MeasureDeleted { Measure = "Biceps" });
            
            user.Measures.Count().Should().Be(0);
        }

        [Test]
        public void remove_non_existing_measures()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureDeleted { Measure = "Biceps" });
            
            user.Measures.Count().Should().Be(0);
        }
        
        [Test]
        public void create_new_measure()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureCreated { Measure = "Thighs", Unit = "cm"});

            user.Measures.Count().Should().Be(1);
            user.Measures.First().Name.Should().Be("Thighs");
            user.Measures.First().Values.Count().Should().Be(0);
        }

        [Test]
        public void create_new_measure_with_same_name_should_not_create_new_one()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureCreated { Measure = "Thighs", Unit = "cm" });
            user.Apply(new MeasureCreated { Measure = "Thighs", Unit = "cm" });

            user.Measures.Count().Should().Be(1);
            user.Measures.First().Name.Should().Be("Thighs");
            user.Measures.First().Unit.Should().Be("cm");
            user.Measures.First().Values.Count().Should().Be(0);
        }

        [Test]
        public void edit_existing_measure()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureCreated { Measure = "Thighs", Unit = "cm" }); 
            user.Apply(new MeasureEdited { OldMeasure = "Thighs", NewMeasure = "ThighsInches", Unit = "inches" });
            
            user.Measures.Count().Should().Be(1);
            user.Measures.First().Name.Should().Be("ThighsInches");
            user.Measures.First().Unit.Should().Be("inches");
            user.Measures.First().Values.Count().Should().Be(0);
        }

        [Test]
        public void edit_non_existing_measure_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureEdited { OldMeasure = "Thighs", NewMeasure = "ThighsInches", Unit = "inches" });
            
            user.Measures.Count().Should().Be(0);
        }
    }
}