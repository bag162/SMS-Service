using Backend.TaskMonitor;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SMS_Service_Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            RecurringJob.AddOrUpdate<TaskMonitorWorker>(x => x.StartTaskMonitor(), Cron.Minutely);
            RecurringJob.AddOrUpdate<TaskCreatorWorker>(x => x.CheckTaskForCreateQueue(), Cron.Minutely);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}