using System.Linq;
using FluentScheduler;
using NHibernate;
using NHibernate.Linq;
using SmartTrack.Model.Measures;

namespace SmartTrack.Web.ScheduledTasks
{
    public class CreateTestData : ITask
    {
        private readonly ISession session;
        
        public CreateTestData() { }
        public CreateTestData(ISession session)
        {
            this.session = session;
        }

        public void Execute()
        {
            var existing = session.Query<User>().Where(x => x.Name == "caiokf").FirstOrDefault();
            if (existing == null)
            {
                using (var tx = session.BeginTransaction())
                {
                    session.Save(new User("caiokf", "", "caiokf@gmail.com"));
                    tx.Commit();
                }
            }
        }
    }
}