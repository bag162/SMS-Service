using Backend.Models.DB;
using Backend.Implemantation.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.TarantoolDB.Repositories;
using Backend.Models.Implementation.Enums;

namespace Backend.Implemantation.Services
{
    public class ProxyService : IProxyService
    {
        private readonly ProxyRepository proxyRepository;
        private readonly Random randomizer = new();
        public ProxyService(ProxyRepository proxyRepository)
        {
            this.proxyRepository = proxyRepository;
        }

        public async Task<ProxyModel[]> GetAllProxyAsync(int bucket = 5500)
        {
            var allProxy = await proxyRepository.FindAll(bucket);
            return allProxy.ToArray();
        }
        public async Task<ProxyModel> GetRandomProxyAsync(int bucket = 5500)
        {
            var proxyList = await proxyRepository.FindAll(bucket);
            if (proxyList.Count() == 0)
            {
                return null;
            }
            return proxyList.ToArray()[randomizer.Next(proxyList.Count())];
        }

        public async Task SetInvalidProxyAsync(ProxyModel proxy)
        {
            var updatedProxy = proxy;
            updatedProxy.Status = (int)ProxyStatus.InActive;
            await proxyRepository.Update(updatedProxy, (int)proxy.Bucket);
            return;
        }
        public async Task SetValidProxyAsync(ProxyModel proxy, string externalIp)
        {
            ProxyModel newProxy = proxy;
            if (proxy.Status == (int)ProxyStatus.InActive)
            {
                newProxy.Status = (int)ProxyStatus.Active;
            }
            newProxy.LasteTimeActive = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            newProxy.ExternalIp = externalIp;
            await proxyRepository.Update(newProxy, (int)proxy.Bucket);
            return;
        }
    }
}