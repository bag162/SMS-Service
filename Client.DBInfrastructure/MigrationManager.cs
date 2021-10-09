﻿using Client.DataBase.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.DBInfrastructure
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var appContext = scope.ServiceProvider.GetRequiredService<UserContext>();
                try
                {
                    appContext.Database.Migrate();
                }
                catch (Exception)
                {
                    throw new Exception("Fail start migratorManager");
                }
            }

            return host;
        }
    }
}
