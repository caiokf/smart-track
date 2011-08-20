using System;
using System.Configuration;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SmartTrack.Model;

namespace SmartTrack.Web.Configuration
{
    public class NHibernateConfiguration
    {
        public static ISessionFactory BuildSessionFactory()
        {
            var automap = AutoMap
                .AssemblyOf<IEntity>()
                .AddMappingsFromAssemblyOf<UserMap>()
                .UseOverridesFromAssemblyOf<UserMap>()
                .Where(x => typeof (IEntity).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract);

            return Fluently.Configure()
                .Mappings(x => x.AutoMappings.Add(automap))
                .ConfigureDatabase()
                .Diagnostics(x => x.OutputToConsole())
                .BuildSessionFactory();
        }
    }

    public static class DatabaseConfigurations
    {
        public static FluentConfiguration ConfigureDatabase(this FluentConfiguration config)
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
            return config.Database(SQLiteConfiguration.Standard.ConnectionString(ConfigurationManager.ConnectionStrings["database"].ConnectionString).ShowSql())
                .ExposeConfiguration(x => new SchemaUpdate(x).Execute(false, true));
        }
    }
}