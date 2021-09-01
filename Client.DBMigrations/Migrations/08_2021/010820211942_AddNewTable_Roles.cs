using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DBMigrations.Migrations._08_2021
{
    [Migration(010820211942)]
    public class AddNewTable_Roles : Migration
    {
        public override void Up()
        {
            Create.Table("Roles")
                .WithColumn("id").AsInt64().NotNullable().PrimaryKey()
                .WithColumn("role").AsString().NotNullable();

            Insert.IntoTable("Roles")
                .Row(new { id = 1, role = "user" })
                .Row(new { id = 2, role = "admin" });
        }

        public override void Down()
        {
            Delete.Table("Roles");
        }
    }
}
