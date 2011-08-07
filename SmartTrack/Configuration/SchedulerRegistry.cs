using FluentScheduler;
using NHibernate;
using SmartTrack.Web.ScheduledTasks;
using StructureMap;

namespace SmartTrack.Web.Configuration
{
    public class SchedulerRegistry : Registry
    {
        private ISession session;

        public SchedulerRegistry()
        {
            Schedule<CreateTestData>().ToRunNow();
        }

        public override ITask GetTaskInstance<T>()
        {
            if (session == null)
                session = ObjectFactory.GetInstance<ISessionFactory>().OpenSession();

            return ObjectFactory
                .With(session)
                .GetInstance<T>();
        }
    }
}