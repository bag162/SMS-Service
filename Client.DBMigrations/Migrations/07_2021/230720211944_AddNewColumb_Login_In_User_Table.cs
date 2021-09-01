using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DBMigrations.Migrations._07_2021
{
    [Migration(230720211944)]
    public class AddNewColumb_Login_In_User_Table : Migration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .AddColumn("Login").AsString().WithDefaultValue("undefined").NotNullable();
        }

        public override void Down()
        {
            Delete.Column("Login").FromTable("Users");
        }
    }
}
