using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using SmartTrack.Model.Measures;
using Newtonsoft.Json;

namespace SmartTrack.Model.Repositories
{
    public class Repository
    {
        private readonly ISession session;

        public Repository(ISession session)
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

    public class UserRepository
    {
        private readonly ISession session;

        public UserRepository(ISession session)
        {
            this.session = session;
        }

        public User GetUser(string name)
        {
            var user = session.Query<User>()
                .Where(x => x.Name == name).FirstOrDefault();
            return user == null ? null : GetUser(user.Id);
        }

        public User GetUser(Guid id)
        {
            var user = session.Load<User>(id);
            var events = session.Query<DomainEvent>()
                .Where(x => x.UserId == id).ToList()
                .Select(x => JsonConvert.DeserializeObject(x.Event, Type.GetType(x.EventType)))
                .Cast<IDomainEvent>();
            
            user.Hydrate(events);
            return user;
        }
    }
}