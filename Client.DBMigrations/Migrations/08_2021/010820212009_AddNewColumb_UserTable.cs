using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DBMigrations.Migrations._08_2021
{
    [Migration(010820212009)]
    public class AddNewColumb_UserTable : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("IdRole").AsInt64().NotNullable().WithDefaultValue(1);
        }

        public override void Down()
        {
            Delete.Column("IdRole").FromTable("Users");
        }
    }
}
