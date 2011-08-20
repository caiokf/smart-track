using System;
using NUnit.Framework;
using SmartTrack.Model;
using SmartTrack.Model.Measures;

namespace SmartTrack.Tests.Unit.Measures
{
    public class FakeEvent : IDomainEvent { public bool IsValid() { return true; } }

    [TestFixture]
    public class UserAggregateEvents
    {
        [Test]
        public void should_not_receive_events_not_subscribed_to()
        {
            var user = new User("any", "any", "any");

            Assert.Throws<ArgumentException>(() => user.Apply(new FakeEvent()));
        }
    }
}