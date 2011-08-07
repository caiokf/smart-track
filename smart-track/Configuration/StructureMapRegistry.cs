using NHibernate;
using SmartTrack.Web.Http;
using StructureMap.Configuration.DSL;

namespace SmartTrack.Web.Configuration
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<IHttpSession>().Use<CurrentHttpContextSession>();

            For<ISessionFactory>().Singleton().Use(NHibernateConfiguration.BuildSessionFactory);
            
            For<ISession>().HybridHttpOrThreadLocalScoped().Use(c => c.GetInstance<ISessionFactory>().OpenSession());
        }
    }
}