using FluentNHibernate;
using FubuMVC.Validation;
using FubuValidation;
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

            For<IValidator>().Use<Validator>();
            For<IValidationFailureHandler>().Use<ValidationFailureHandler>();
            For<IValidationFailurePolicy>().Add<JsonValidationFailurePolicy>();
        }

        private void setupNHibernate()
        {
            For<ISessionFactory>().Singleton().Use(NHibernateConfiguration.BuildSessionFactory);
            
            For<ISession>().HybridHttpOrThreadLocalScoped().Use(c =>
            {
                return c.GetInstance<ISessionFactory>().OpenSession();
            });

            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();
        }
    }
}