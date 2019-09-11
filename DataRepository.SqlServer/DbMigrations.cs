using FluentMigrator;

namespace DataRepository.SqlServer
{
    [Migration(20190911)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Customer")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("FirstName").AsString(200).NotNullable()
                .WithColumn("LastName").AsString(200).NotNullable()
                .WithColumn("EmailAddress").AsString(200).NotNullable();
        }

        public override void Down()
        {
            Delete.Table("Customer");
        }
    }
}
