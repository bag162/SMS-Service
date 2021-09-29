using Backend.Models.DB;
using Backend.Implemantation.IServices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.TarantoolDB.Repositories;
using Backend.Models.Implementation;
using System.Linq;

namespace Backend.Implemantation.Services
{
    public class ServicePricesService : IServicePricesService
    {
        public ServicePricesService(ServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }
        private readonly ServiceRepository serviceRepository;

        public double GetServicePriceByServiceId(long serviceId, int bucket = 5000)
        {
            return serviceRepository.Find(serviceId, 1, bucket).Result.FirstOrDefault().Price;
        }
        public IEnumerable<ServiceModel> GetAllServices(int bucket = 5000)
        {
            return serviceRepository.FindAll(bucket).Result;
        }
        public ServiceModel GetServiceByServiceId(long serviceId, int bucket = 5000)
        {
            return serviceRepository.Find(serviceId, 1, bucket).Result.FirstOrDefault();
        }
        public JsonExpression GetAllExpressionByServiceId(long serviceId, int bucket = 5000)
        {
            return JsonConvert.DeserializeObject<JsonExpression>(GetServiceByServiceId(serviceId, bucket).RegularExpressions);
        }

        public async Task UpdateServiceAsync(ServiceModel service)
        {
            serviceRepository.Update(service, (int)service.Bucket);
            return;
        }

        public async Task CreateServiceAsync(ServiceModel service)
        {
            serviceRepository.Create(service, (int)service.Bucket);
            return;
        }

        public async Task DeleteServiceAsync(ServiceModel service)
        {
            serviceRepository.Delete(service, (int)service.Bucket);
            return;
        }
    }

    public class Root
    {
        public string RegularExpression { get; set; }
    }
}
