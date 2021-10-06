using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DBMigrations.Migrations._08_2021
{
    [Migration(202108051343)]
    public class ChangeNullable_UserTable_UsernameColomhb : Migration
    {
        public override void Up()
        {
            Alter.Table("Users").AlterColumn("Username").AsString().Nullable();
        }

        public override void Down()
        {
        }
    }
}