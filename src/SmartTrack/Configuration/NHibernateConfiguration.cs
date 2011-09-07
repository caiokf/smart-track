using System.Configuration;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SmartTrack.Model;

namespace SmartTrack.Web.Configuration
{
    public interface IPersistenceConfiguration
    {
        ISessionFactory BuildSessionFactory();
        void RecreateDatabase();
    }

    public class NHibernateConfiguration : IPersistenceConfiguration
    {
        private readonly IAppSettings appSettings;
        private NHibernate.Cfg.Configuration configuration;
        private bool configurationCreated;

        public NHibernateConfiguration(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public ISessionFactory BuildSessionFactory()
        {
            var automap = AutoMap
                .AssemblyOf<IEntity>()
                .AddMappingsFromAssemblyOf<UserMap>()
                .UseOverridesFromAssemblyOf<UserMap>()
                .Where(x => typeof (IEntity).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);

            var sessionFactory = Fluently.Configure()
                .Mappings(x => x.AutoMappings.Add(automap))
                .ConfigureDatabase(appSettings)
                .Diagnostics(x => x.OutputToConsole())
                .ExposeConfiguration(x => configuration = x)
                .BuildSessionFactory();

            configurationCreated = true;
            return sessionFactory;
        }

        public void RecreateDatabase()
        {
            if (!configurationCreated)
                BuildSessionFactory();

            new SchemaUpdate(configuration).Execute(false, true);
        }
    }

    public static class DatabaseConfigurations
    {
        public static FluentConfiguration ConfigureDatabase(this FluentConfiguration config, IAppSettings appSettings)
        {
            #if DEBUG
                return config.DebugDatabase();
            #endif
                return config.ProductionDatabase();
        }

        public static FluentConfiguration ProductionDatabase(this FluentConfiguration config)
        {
            return config.Database(MySQLConfiguration.Standard.ConnectionString(x => x
                .Server("db001.appharbor.net")
                .Database("db3362")));
        }

        public static FluentConfiguration DebugDatabase(this FluentConfiguration config)
        {
            return config.Database(SQLiteConfiguration.Standard.ConnectionString(ConfigurationManager.ConnectionStrings["database"].ConnectionString).ShowSql());
        }
    }
}