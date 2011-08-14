using System.Data;
using MigSharp;

namespace SmartTrack.Web.Migrations
{
    [MigrationExport]
    public class Migration_002 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.OnTable("User")
                .AddNotNullableColumn("Email", DbType.StringFixedLength).OfSize(255).HavingTemporaryDefault("")
                .AddNotNullableColumn("Password", DbType.String).HavingTemporaryDefault("");
        }

        public void Down(IDatabase db)
        {
            db.OnTable("User")
                .DropColumn("Email")
                .DropColumn("Password");
        }
    }
}