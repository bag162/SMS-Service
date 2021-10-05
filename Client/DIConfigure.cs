using AutoMapper;
using Backend.Configuration;
using Client.Database.Data.Repository;
using Client.Database.Data.Services;
using Client.DataBase.Data.Contexts;
using Client.DataBase.Data.IServices;
using Client.DataBase.Data.Services;
using Client.gRPC;
using Client.gRPC.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
{
    public class DIConfigure
    {
        public static void ConfigureService(IServiceCollection services, IConfiguration Configuration)
        {
            // gRPC Services
            services.AddTransient<gRPCClient>();
            services.AddTransient<AccountService>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IRoleService, RoleService>();

            // Angular Configure
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login"); });
            services.AddMvc();
        }

        public static void ConfigureDB(IServiceCollection services, IConfiguration Configuration, string connectionString)
        {
            // DB Configure
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<RoleContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
