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

    [Migration(20190928)]
    public class BirthdayMigration : Migration
    {
        public override void Up()
        {
            Alter.Table("Customer")
                .AddColumn("BirthDay").AsInt16().Nullable()
                .AddColumn("BirthMonth").AsInt16().Nullable();
        }

        public override void Down()
        {
        }
    }

}
