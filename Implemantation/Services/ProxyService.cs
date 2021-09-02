using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using Implemantation.Models;
using Implemantation.Models.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using TarantoolDB.Repositories;

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
            return proxyRepository.FindAll().ToArray();
        } // int

        public ProxyModel GetRandomProxy()
        {
            var proxyList = proxyRepository.FindAll();
            return proxyList.ToArray()[randomizer.Next(proxyList.Count())];
        }

        public async Task SetInvalidProxyAsync(ProxyModel proxy)
        {
            await proxyRepository.UpdateArgument(proxy.Id, 5, (int)ProxyStatus.InActive);
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
