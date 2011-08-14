using System.Linq;
using MigSharp;

namespace SmartTrack.Web.Migrations
{
    public static class MigrationExtensions
    {
        public static void DropTable(this IDatabase db, string tableName)
        {
            db.Tables[tableName].Drop();
        }

        public static void Clear(this SupportedProviders providers)
        {
            var names = providers.Names;
            names.ToList().ForEach(providers.Remove);
        }
    }
}