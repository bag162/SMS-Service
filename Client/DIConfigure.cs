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

namespace Client
{
    public class DIConfigure
    {
        public static void ConfigureService(IServiceCollection services)
        {
            // gRPC Services
            services.AddTransient<gRPCClient>();
            services.AddTransient<gRPCAccountService>();
            services.AddTransient<gRPCUserService>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();
            services.AddScoped<IRoleService, RoleService>();

            // Angular Configure
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/dist"; });

            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                   .AddCookie(options => { options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/login"); });
            services.AddHealthChecks();
        }

        public static void ConfigureDB(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Client")));
        }
    }
}
