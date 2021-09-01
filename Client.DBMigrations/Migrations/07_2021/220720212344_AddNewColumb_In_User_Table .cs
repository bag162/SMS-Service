using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DBMigrations.Migrations._07_2021
{
    [Migration(220720212344)]
    public class AddNewColumb_In_User_Table : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Telegram").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Column("Telegram").FromTable("Users");
        }
    }
}
