using Models.DTO.DTOModels;
using Implemantation.IServices;
using Implemantation.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProGaudi.MsgPack.Light;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using SMS_Service_Worker.Common.Services.Configuration;
using TarantoolDB.Repositories;
using SMS_Service_Worker.Workers.SMSWorker;
using SMS_Service_Worker.Workers.CheckProxyValid;
using SMS_Service_Worker.Workers.CheckerDBWorker;
using AutoMapper;
using GrpcService;

namespace SMS_Service_Worker
{
    public class DependencyInjectionConf
    {
        public static void Configure(IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<ConfigurationClass>(Configuration);
            services.AddControllersWithViews();
            // reg reposities
            services.AddTransient<OrderRepository>();
            services.AddTransient<UserRepository>();
            services.AddTransient<HistoryRepository>();
            services.AddTransient<ServiceRepository>();
            services.AddTransient<AccountRepository>();
            services.AddTransient<ProxyRepository>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IServicePricesService, ServicePricesService>();
            services.AddTransient<IHistoryService, HistoryService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IProxyService, ProxyService>();
            services.AddTransient<IHandlerConveyor, HandlerConveyor>();

            services.AddTransient<SMSWorker>();
            services.AddTransient<CheckAccountValidWorker>();
            services.AddTransient<CheckProxyValidWorker>();
            services.AddTransient<CheckDBWorker>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void ConfigureTarantool(IServiceCollection services, IConfiguration Configuration)
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
        }
    }
}
