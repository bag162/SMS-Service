using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DBMigrations.Migrations._07_2021
{
    [Migration(200720211340)]
    public class _200720211340_Add_User_Table : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Username").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("EmailAddress").AsString().Nullable()
                .WithColumn("ApiKey").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
