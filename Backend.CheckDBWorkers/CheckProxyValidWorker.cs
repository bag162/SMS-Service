using Backend.Models.DB;
using Backend.Implemantation.IServices;
using Backend.Implemantation.Services;
using Microsoft.Extensions.Options;
using Backend.Configuration;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace SMS_Service_Worker.Workers.CheckProxyValid
{
    public class CheckProxyValidWorker
    {
        private readonly IProxyService proxyService;
        private readonly IHandlerConveyor handlerConveyor;
        private readonly IOptions<ConfigurationClass> Config;

        public CheckProxyValidWorker(
            IProxyService proxyService,
            IOptions<ConfigurationClass> Config,
            IHandlerConveyor handlerConveyor) 
        {
            this.proxyService = proxyService;
            this.Config = Config;
            this.handlerConveyor = handlerConveyor;
        }

        public async Task CheckProxyValid()
        {
            ProxyModel[] allProxy = await proxyService.GetAllProxyAsync();
            foreach (var proxy in allProxy)
            {
                HttpClientHandler handler = handlerConveyor.SetProxy(new HttpClientHandler(), proxy);
                HttpClient client = new(handler) { Timeout = new TimeSpan(0, 0, 20), BaseAddress = new Uri(Config.Value.OtherRoutes.CheckIpURI) };
                HttpContent accountProfileRequest;
                try
                {
                    
                    accountProfileRequest = client.GetAsync(Config.Value.OtherRoutes.CheckIpURI).Result.Content;
                }
                catch
                {
                    proxyService.SetInvalidProxyAsync(proxy);
                    continue;
                }
                proxyService.SetValidProxyAsync(proxy, accountProfileRequest.ReadAsStringAsync().Result);
            }
            return;
        }
    }
}
