using NHibernate;
using NUnit.Framework;
using SmartTrack.Web.Configuration;

namespace SmartTrack.Tests.Unit.Persistance.InMemory
{
    public class InMemoryPersistenceTestsSetup
    {
        protected ISession session;

        [TestFixtureSetUp]
        public void setup()
        {
            var sessionFactory = NHibernateConfiguration.BuildSessionFactory();
            session = sessionFactory.OpenSession();
        }
	}
}
