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

        [Test]
        public void edit_measure_name_to_be_empty_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureCreated { Measure = "Thighs", Unit = "cm" });
            user.Apply(new MeasureEdited { OldMeasure = "Thighs", NewMeasure = "", Unit = "inches" });

            user.Measures.Count().Should().Be(1);
            user.Measures.First().Name.Should().Be("Thighs");
            user.Measures.First().Unit.Should().Be("cm");
        }

        [Test]
        public void edit_measure_name_to_be_same_as_other_existing_measure_should_have_no_effect_on_name()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureCreated { Measure = "Calves", Unit = "cm" });
            user.Apply(new MeasureCreated { Measure = "Thighs", Unit = "cm" });
            user.Apply(new MeasureEdited { OldMeasure = "Thighs", NewMeasure = "Calves", Unit = "inches" });

            user.Measures.Count().Should().Be(2);
            user.Measures.ToArray()[0].Name.Should().Be("Calves");
            user.Measures.ToArray()[0].Unit.Should().Be("cm");
            user.Measures.ToArray()[1].Name.Should().Be("Thighs");
            user.Measures.ToArray()[1].Unit.Should().Be("inches");
        }

        [Test]
        public void add_an_existing_measure_to_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new MeasureCreated { Measure = "Pecs", Unit = "cm" });
            user.Apply(new MeasureAddedToGroup { Group = "Chest", Measure = "Pecs" });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Measures.Count().Should().Be(1);
            user.Groups.First().Measures.First().Name.Should().Be("Pecs");
        }

        [Test]
        public void add_an_existing_measure_to_a_non_existing_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureCreated { Measure = "Pecs", Unit = "cm" });
            user.Apply(new MeasureAddedToGroup { Group = "Chest", Measure = "Pecs" });

            user.Groups.Count().Should().Be(0);
        }

        [Test]
        public void add_a_non_existing_measure_to_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new MeasureAddedToGroup { Group = "Chest", Measure = "Pecs" });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Measures.Count().Should().Be(0);
        }

        [Test]
        public void add_a_non_existing_measure_to_a_non_existing_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new MeasureAddedToGroup { Group = "Chest", Measure = "Pecs" });

            user.Groups.Count().Should().Be(0);
        }

        [Test]
        public void remove_a_measure_from_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new MeasureCreated { Measure = "Pecs", Unit = "cm" });
            user.Apply(new MeasureAddedToGroup { Group = "Chest", Measure = "Pecs" });

            user.Apply(new MeasureRemovedFromGroup { Group = "Chest", Measure = "Pecs" });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Measures.Count().Should().Be(0);
        }
    }
}