using System;
using NHibernate;
using SmartTrack.Model.Measures;
using Newtonsoft.Json;

namespace SmartTrack.Model.Repositories
{
    public class EventRepository
    {
        private readonly ISession session;

        public EventRepository(ISession session)
        {
            this.session = session;
        }

        public void SaveEvent<T>(T addedEvent, User user) where T : IDomainEvent
        {
            var eventType = addedEvent.GetType().ToString();
            var eventJson = JsonConvert.SerializeObject(addedEvent);

            if (!addedEvent.IsValid())
                throw new ArgumentException(string.Format("Trying to save invalid event '{0}' with value: \r\n {1}", eventType, eventJson));

            var e = new DomainEvent
            {
                DateTime = DateTime.Now,
                UserId = user.Id,
                EventType = eventType,
                Event = eventJson
            };

            session.Save(e);
            session.Flush();
        }
    }
}