using System.Linq;
using FluentScheduler;
using SmartTrack.Web.Configuration;
using SmartTrack.Web.Utils.Extensions;

namespace SmartTrack.Web.ScheduledTasks
{
    public class RefreshDatabase : ITask
    {
        private readonly IAppSettings appSettings;
        private readonly IPersistenceConfiguration persistenceConfiguration;

        public RefreshDatabase() { }
        public RefreshDatabase(IAppSettings appSettings, IPersistenceConfiguration persistenceConfiguration)
        {
            this.appSettings = appSettings;
            this.persistenceConfiguration = persistenceConfiguration;
        }

        public void Execute()
        {
            var refreshOn = new[]
            {
                BuildConfigurations.Debug.Description(),
                BuildConfigurations.TestsUnit.Description()
            };
            if (!refreshOn.Contains(appSettings.Configuration))
                return;

            persistenceConfiguration.RecreateDatabase();
        }
    }
}