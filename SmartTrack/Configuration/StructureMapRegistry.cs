using FluentNHibernate;
using NHibernate;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Http;
using SmartTrack.Web.Http.Behaviors;
using StructureMap.Configuration.DSL;

namespace SmartTrack.Web.Configuration
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            setupNHibernate();
            
            For<IHttpSession>().Use<CurrentHttpContextSession>();
            
            For<User>().Use(x => x.GetInstance<UserRepository>().GetUser("caiokf"));
        }

        private void setupNHibernate()
        {
            For<ISessionFactory>().Singleton().Use(NHibernateConfiguration.BuildSessionFactory);
            
            For<ISession>().Use(c =>
            {
                var transaction = c.GetInstance<ITransactionBoundary>();
                return transaction.Session;
            });

            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();
        }
    }
}