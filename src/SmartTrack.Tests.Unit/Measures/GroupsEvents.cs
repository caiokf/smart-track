using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using SmartTrack.Model.Measures;

namespace SmartTrack.Tests.Unit.Measures
{
    [TestFixture]
    public class GroupsEvents
    {
        [Test]
        public void create_new_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Name.Should().Be("Measures");
            user.Groups.First().Measures.Count().Should().Be(0);
        }

        [Test]
        public void create_two_groups()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupAdded { Group = "Chest" });

            user.Groups.Count().Should().Be(2);
            user.Groups.ToArray()[0].Name.Should().Be("Measures");
            user.Groups.ToArray()[1].Name.Should().Be("Chest");
        }

        [Test]
        public void create_new_group_with_name_of_existing_group_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupAdded { Group = "Measures" });

            user.Groups.Count().Should().Be(1);
        }

        [Test]
        public void delete_a_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupDeleted { Group = "Measures" });

            user.Groups.Count().Should().Be(0);
        }

        [Test]
        public void delete_a_non_existing_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupDeleted { Group = "NonExisting" });

            user.Groups.Count().Should().Be(1);
        }

        [Test]
        public void edit_an_existing_group()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupEdited { OldGroup = "Measures", NewGroup = "Edited Measures"});

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Name.Should().Be("Edited Measures");
        }

        [Test]
        public void edit_an_existing_group_to_an_empty_name()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupEdited { OldGroup = "Measures", NewGroup = "" });

            user.Groups.Count().Should().Be(1);
            user.Groups.First().Name.Should().Be("Measures");
        }

        [Test]
        public void edit_an_existing_group_to_an_existing_group_name_should_have_no_effect()
        {
            var user = MotherOf.Users.MrEmpty();

            user.Apply(new GroupAdded { Group = "Chest" });
            user.Apply(new GroupAdded { Group = "Measures" });
            user.Apply(new GroupEdited { OldGroup = "Measures", NewGroup = "Chest" });

            user.Groups.Count().Should().Be(2);
            user.Groups.ToArray()[0].Name.Should().Be("Chest");
            user.Groups.ToArray()[1].Name.Should().Be("Measures");
        }
    }
}