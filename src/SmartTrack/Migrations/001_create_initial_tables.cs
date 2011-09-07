using System.Data;
using MigSharp;

namespace SmartTrack.Web.Migrations
{
    [MigrationExport]
    public class Migration_001 : IReversibleMigration
    {
        public void Up(IDatabase db)
        {
            db.CreateTable("DomainEvent")
                .WithPrimaryKeyColumn("Id", DbType.Guid)
                .WithNotNullableColumn("UserId", DbType.Guid)
                .WithNotNullableColumn("DateTime", DbType.DateTime)
                .WithNotNullableColumn("Event", DbType.String);

            db.CreateTable("User")
                .WithPrimaryKeyColumn("Id", DbType.Guid)
                .WithNotNullableColumn("Name", DbType.StringFixedLength).OfSize(255);
        }
        
        public void Down(IDatabase db)
        {
            db.DropTable("User");
            db.DropTable("DomainEvent");
        }
    }
}