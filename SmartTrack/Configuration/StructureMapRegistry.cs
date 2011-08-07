using NHibernate;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;
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

            For<User>().Use(x => x.GetInstance<UserRepository>().GetUser("caiokf"));
        }
    }
}