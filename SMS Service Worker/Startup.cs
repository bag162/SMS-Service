using Backend.TaskMonitor;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            RecurringJob.AddOrUpdate<TaskMonitorWorker>(x => x.StartTaskMonitor(), Cron.Minutely);
            RecurringJob.AddOrUpdate<TaskCreatorWorker>(x => x.CheckTaskForCreateQueue(), Cron.Minutely);
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