using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Model.Measures;

namespace SmartTrack.Tests.Unit.Measures
{
    [TestFixture]
    public class TagsEvents
    {
        [Test]
        public void create_new_tag()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.Today });

            user.Tags.Count().Should().Be(1);
            user.Tags.First().Name.Should().Be("New Trainning Started");
            user.Tags.First().StartDate.Should().Be(DateTime.Today);
        }

        [Test]
        public void create_new_tag_with_empty_name_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "", StartDate = DateTime.Today });

            user.Tags.Count().Should().Be(0);
        }

        [Test]
        public void create_new_tag_with_whitespace_name_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "  ", StartDate = DateTime.Today });

            user.Tags.Count().Should().Be(0);
        }

        [Test]
        public void create_new_tag_null_start_date_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.MinValue });

            user.Tags.Count().Should().Be(0);
        }

        [Test]
        public void create_new_tag_with_same_name_and_start_date_as_existing_tag_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.Today });
            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.Today });

            user.Tags.Count().Should().Be(1);
        }

        [Test]
        public void existing_tag_deleted()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.Today });
            user.Apply(new TagDeleted { Tag = "New Trainning Started", StartDate = DateTime.Today });

            user.Tags.Count().Should().Be(0);
        }

        [Test]
        public void non_existing_tag_deleted_with_different_name()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.Today });
            user.Apply(new TagDeleted { Tag = "Non-Existing", StartDate = DateTime.Today });

            user.Tags.Count().Should().Be(1);
        }

        [Test]
        public void non_existing_tag_deleted_with_different_date()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "New Trainning Started", StartDate = DateTime.Today });
            user.Apply(new TagDeleted { Tag = "New Trainning Started", StartDate = DateTime.Today.AddDays(1) });

            user.Tags.Count().Should().Be(1);
        }

        [Test]
        public void add_an_existing_tag_to_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new TagAdded { Tag = "Tag", StartDate = DateTime.Today });
            user.Apply(new TagAddedToGroup { Group = "Chest", Tag = "Tag", TagStartDate = DateTime.Today });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Tags.Count().Should().Be(1);
            user.Groups.First().Tags.First().Name.Should().Be("Tag");
        }

        [Test]
        public void add_an_existing_tag_to_a_non_existing_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAdded { Tag = "Tag", StartDate = DateTime.Today });
            user.Apply(new TagAddedToGroup { Group = "Chest", Tag = "Tag", TagStartDate = DateTime.Today });

            user.Groups.Count().Should().Be(0);
        }

        [Test]
        public void add_a_non_existing_tag_to_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new TagAddedToGroup { Group = "Chest", Tag = "Tag", TagStartDate = DateTime.Today });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Tags.Count().Should().Be(0);
        }

        [Test]
        public void add_a_non_existing_tag_to_a_non_existing_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new TagAddedToGroup { Group = "Chest", Tag = "Tag", TagStartDate = DateTime.Today });

            user.Groups.Count().Should().Be(0);
        }

        [Test]
        public void remove_a_tag_from_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new MeasureCreated { Measure = "Pecs", Unit = "cm" });
            user.Apply(new TagAddedToGroup { Group = "Chest", Tag = "Tag", TagStartDate = DateTime.Today });

            user.Apply(new TagRemovedFromGroup { Group = "Chest", Tag = "Tag", TagStartDate = DateTime.Today });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Tags.Count().Should().Be(0);
        }
    }
}