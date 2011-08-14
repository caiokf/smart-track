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

        public static IExistingTable OnTable(this IDatabase db, string tableName)
        {
            return db.Tables[tableName];
        }

        public static IExistingTable DropColumn(this IExistingTable table, string columnName)
        {
            table.Columns[columnName].Drop();
            return table;
        }

        public static void Clear(this SupportedProviders providers)
        {
            var names = providers.Names;
            names.ToList().ForEach(providers.Remove);
        }
    }
}