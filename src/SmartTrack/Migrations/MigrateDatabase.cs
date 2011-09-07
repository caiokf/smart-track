using System.Configuration;
using MigSharp;
using SmartTrack.Web.Controllers.Measures;
using SmartTrack.Web.Controllers.Measures.Measures;

namespace SmartTrack.Web.Migrations
{
    public class MigrateDatabase
    {
        public void Execute()
        {
            var connection = ConfigurationManager.ConnectionStrings["database"];
            var options = new MigrationOptions();
            options.SupportedProviders.Clear();
            options.SupportedProviders.Add(ProviderNames.SQLite);
            options.SupportedProviders.Add(ProviderNames.SqlServer2005);
            options.SupportedProviders.Add(ProviderNames.SqlServer2008);

            var m = new Migrator(connection.ConnectionString, connection.ProviderName, options);

            m.MigrateAll(typeof(MeasuresController).Assembly);
        }
    }
}