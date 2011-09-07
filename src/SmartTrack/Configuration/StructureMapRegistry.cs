using FubuMVC.Validation;
using FubuValidation;
using NHibernate;
using SmartTrack.Model.Measures;
using SmartTrack.Model.Repositories;
using SmartTrack.Web.Behaviors.Validation;
using SmartTrack.Web.Http;
using SmartTrack.Web.Http.Behaviors.Transactions;
using StructureMap.Configuration.DSL;

namespace SmartTrack.Web.Configuration
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            IncludeRegistry<WebLayerRegistry>();
            IncludeRegistry<NhibernateRegistry>();
            IncludeRegistry<ValidationRegistry>();
        }
    }

    public class WebLayerRegistry : Registry
    {
        public WebLayerRegistry()
        {
            For<IAppSettings>().Use<AppSettingsWrapper>();
            For<IHttpSession>().Use<CurrentHttpContextSession>();
            For<User>().Use(x => x.GetInstance<UserRepository>().GetUser("caiokf"));
            //For<User>().Use(x => new User("caiokf", "", "email"));
        }
    }

    public class NhibernateRegistry : Registry
    {
        public NhibernateRegistry()
        {
            For<ISessionFactory>().Singleton().Use(c => c.GetInstance<NHibernateConfiguration>().BuildSessionFactory());
            //For<ISession>().Use(c => c.GetInstance<ITransactionBoundary>().Session);
            For<ISession>().HybridHttpOrThreadLocalScoped().Use(c => c.GetInstance<ISessionFactory>().OpenSession());
            For<ITransactionBoundary>().Use<NHibernateTransactionBoundary>();
        }
    }

    public class ValidationRegistry : Registry
    {
        public ValidationRegistry()
        {
            Scan(x =>
            {
                x.AssemblyContainingType<ConventionalValidator>();
                x.ConnectImplementationsToTypesClosing(typeof (IValidate<>));
            });
            For<IValidator>().Use<ConventionalValidator>();
            For<IValidationFailureHandler>().Use<ValidationFailureHandler>();
            For<IValidationFailurePolicy>().Add<JsonValidationFailurePolicy>();
        }
    }
}