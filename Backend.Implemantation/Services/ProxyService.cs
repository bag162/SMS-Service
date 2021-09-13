using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using TarantoolDB.Repositories;
using Models.ImplementationModels.Enums;

namespace Implemantation.Services
{
    public class ProxyService : IProxyService
    {
        private readonly ProxyRepository proxyRepository;
        private readonly Random randomizer = new();
        public ProxyService(ProxyRepository proxyRepository)
        {
            this.proxyRepository = proxyRepository;
        }

        public ProxyModel[] GetAllProxy()
        {
            return proxyRepository.FindAll().Result.ToArray();
        } // int

        public ProxyModel GetRandomProxy()
        {
            var proxyList = proxyRepository.FindAll();
            return proxyList.Result.ToArray()[randomizer.Next(proxyList.Result.Count())];
        }

        public async Task SetInvalidProxyAsync(ProxyModel proxy)
        {
            var updatedProxy = proxy;
            updatedProxy.Status = (int)ProxyStatus.InActive;
            await proxyRepository.Update(updatedProxy);
            return;
        }

        public async Task SetValidProxyAsync(ProxyModel proxy, string externalIp)
        {
            ProxyModel newProxy = proxy;
            if (proxy.Status == (int)ProxyStatus.InActive)
            {
                newProxy.Status = (int)ProxyStatus.Active;
            }
            newProxy.LasteTimeActive = DateTime.Now.ToString();
            newProxy.ExternalIp = externalIp;
            await proxyRepository.Update(newProxy);
            return;
        }
    }
}
