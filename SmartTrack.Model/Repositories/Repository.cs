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
         
    }

    public class UserRepository
    {
        private ISession session;

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