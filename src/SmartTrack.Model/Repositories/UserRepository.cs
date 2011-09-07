using System;
using System.Linq;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;
using SmartTrack.Model.Measures;

namespace SmartTrack.Model.Repositories
{
    public class UserRepository
    {
        private readonly ISession session;

        public UserRepository(ISession session)
        {
            this.session = session;
        }

        public virtual IQueryable<User> Users { get { return session.Query<User>(); } }

        public virtual User GetUser(string name)
        {
            var user = session.Query<User>()
                .Where(x => x.Name == name).FirstOrDefault();
            return user == null ? null : GetUser(user.Id);
        }

        public virtual User GetUser(Guid id)
        {
            var user = session.Load<User>(id);
            var events = session.Query<DomainEvent>()
                .Where(x => x.UserId == id).ToList()
                .Select(x => JsonConvert.DeserializeObject(x.Event, Type.GetType(x.EventType)))
                .Cast<IDomainEvent>();
            
            user.Hydrate(events);
            return user;
        }

        public virtual void Save(User user)
        {
            session.SaveOrUpdate(user);
        }
    }

    public static class UserQueries
    {
        public static IQueryable<User> Named(this IQueryable<User> users, string name)
        {
            return users.Where(x => x.Name == name);
        }

        public static IQueryable<User> WithEmail(this IQueryable<User> users, string email)
        {
            return users.Where(x => x.Email.Replace(".","") == email.Replace(".", ""));
        }
    }
}