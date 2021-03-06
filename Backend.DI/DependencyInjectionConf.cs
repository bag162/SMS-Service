using Backend.Models.DB;
using Hangfire;
using Hangfire.SqlServer;
using Backend.Implemantation.IServices;
using Backend.Implemantation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProGaudi.MsgPack.Light;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using Backend.Configuration;
using SMS_Service_Worker.Workers.CheckerDBWorker;
using SMS_Service_Worker.Workers.CheckProxyValid;
using SMS_Service_Worker.Workers.SMSWorker;
using System;
using Backend.TarantoolDB.Repositories;
using Backend.Models.DB.Models;
using Backend.DBInfrastructure.Models;
using DalSoft.Hosting.BackgroundQueue.DependencyInjection;
using Backend.Backend.Implemantation.IServices;
using Backend.Backend.Implemantation.Services;
using AutoMapper;

namespace Backend.DI
{
    public class DependencyInjectionConf
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<ConfigurationClass>(Configuration);
            services.AddBackgroundQueue(onException: exception => {});

            services.AddTransient<OrderRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<HistoryRepository>();
            services.AddTransient<ServiceRepository>();
            services.AddTransient<AccountRepository>();
            services.AddTransient<ProxyRepository>();
            services.AddTransient<QueueRepository>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServicePricesService, ServicePricesService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProxyService, ProxyService>();
            services.AddTransient<IHandlerConveyor, HandlerConveyor>();
            services.AddTransient<IQueueService, QueueService>();

            services.AddTransient<SMSWorker>();
            services.AddTransient<CheckAccountValidWorker>();
            services.AddTransient<CheckProxyValidWorker>();
            services.AddTransient<CheckDBWorker>();
        }

        public static void ConfigureTarantool(IServiceCollection services, IConfiguration Configuration)
        {
            var msgPackContext = new MsgPackContext();
            msgPackContext.GenerateAndRegisterArrayConverter<JsonRequestOneField>();
            msgPackContext.GenerateAndRegisterArrayConverter<QueueModel>();
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
        }

        public static void ConfigureHangFire(IServiceCollection services, IConfiguration Configuration)
        {
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
            services.AddHangfireServer(options => options.WorkerCount = 2);
        }
    }
}