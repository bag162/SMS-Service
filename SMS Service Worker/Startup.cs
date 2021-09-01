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
            DependencyInjectionConf.ConfigureTarantool(services, Configuration);
            DependencyInjectionConf.Configure(services, Configuration);
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