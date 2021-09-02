using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using Implemantation.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TarantoolDB.Repositories;

namespace Implemantation.Services
{
    public class ServicePricesService : IServicePricesService
    {
        public ServicePricesService(ServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }
        private readonly ServiceRepository serviceRepository;

        public double GetServicePriceByServiceId(long serviceId)
        {
            return serviceRepository.FindById(serviceId).Price;
        }

        public IEnumerable<ServiceModel> GetAllServices()
        {
            return serviceRepository.FindAll();
        }

        public ServiceModel GetServiceByServiceId(long serviceId)
        {
            return serviceRepository.FindById(serviceId);
        }

        public JsonExpression GetAllExpressionByServiceId(long serviceId)
        {
            return JsonConvert.DeserializeObject<JsonExpression>(GetServiceByServiceId(serviceId).RegularExpressions);
        }

        public async Task UpdateServiceAsync(ServiceModel service)
        {
            await serviceRepository.Update(service);
            return;
        }

        public async Task CreateServiceAsync(ServiceModel service)
        {
            await serviceRepository.Create(service);
            return;
        }

        public async Task DeleteServiceAsync(ServiceModel service)
        {
            await serviceRepository.Delete(service);
            return;
        }
    }
    public class Root
    {
        public string RegularExpression { get; set; }
    }
}
