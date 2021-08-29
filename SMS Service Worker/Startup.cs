using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProGaudi.MsgPack.Light;
using SMS_Service_Worker.Workers.CheckerDBWorker;
using SMS_Service_Worker.Workers.CheckProxyValid;
using SMS_Service_Worker.Workers.CheckValidWorker;
using DBInfrastructure.DTOModels;
using Hangfire.SqlServer;
using Implemantation.IServices;
using Implemantation.Services;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using SMS_Service_Worker.Common.Services.Configuration;
using SMS_Service_Worker.Workers.SMSWorker;
using System;
using TarantoolDB;
using TarantoolDB.Repositories;

namespace SMS_Service_Worker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var msgPackContext = new MsgPackContext();
            msgPackContext.GenerateAndRegisterArrayConverter<UserModel>();
            msgPackContext.GenerateAndRegisterArrayConverter<OrderModel>();
            msgPackContext.GenerateAndRegisterArrayConverter<HistoryModel>();
            msgPackContext.GenerateAndRegisterArrayConverter<ServiceModel>();
            msgPackContext.GenerateAndRegisterArrayConverter<AccountModel>();
            msgPackContext.GenerateAndRegisterArrayConverter<ProxyModel>();
            var clientOptions = new ClientOptions(Configuration.GetSection("Tarantool:ConnectionCredential").Value, context: msgPackContext);
            var box = new Box(clientOptions);
            box.Connect().ConfigureAwait(false).GetAwaiter().GetResult();
            services.AddSingleton(box);

            services.Configure<ConfigurationClass>(Configuration);
            services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
            services.AddHangfireServer();
            services.AddControllersWithViews();
            // reg reposities
            services.AddTransient<OrderRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<HistoryRepository>();
            services.AddTransient<ServiceRepository>();
            services.AddTransient<AccountRepository>();
            services.AddTransient<ProxyRepository>();

            services.AddTransient<IOrderService, OrderService>();
            /*services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServicePricesService, ServicePricesService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProxyService, ProxyService>();
            services.AddTransient<IHandlerConveyor, HandlerConveyor>();

            services.AddTransient<SMSWorker>();
            services.AddTransient<CheckAccountValidWorker>();
            services.AddTransient<CheckProxyValidWorker>();
            services.AddTransient<CheckDBWorker>();*/
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseHangfireDashboard();
            app.UseRouting();

            app.UseAuthorization();

            RecurringJob.AddOrUpdate<CheckDBWorker>(x => x.CheckDBInsert(), Configuration.GetSection("WorkersTimes:CheckDBInsertCron").Value);
            RecurringJob.AddOrUpdate<CheckDBWorker>(x => x.CheckDBDelete(), Configuration.GetSection("WorkersTimes:CheckDBDeleteCron").Value);
            RecurringJob.AddOrUpdate<CheckAccountValidWorker>(x => x.CheckAccountsOnValid(), Configuration.GetSection("WorkersTimes:CheckAccountsOnValidCron").Value);
            RecurringJob.AddOrUpdate<CheckProxyValidWorker>(x => x.CheckProxyValid(), Configuration.GetSection("WorkersTimes:CheckProxyValid").Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }
    }
}