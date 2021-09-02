using Models.DTO.DTOModels;
using Implemantation.Models;
using Models.DTO.DTOModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IServicePricesService
    {
        public double GetServicePriceByServiceId(long serviceIndex);
        public IEnumerable<ServiceModel> GetAllServices();
        public JsonExpression GetAllExpressionByServiceId(long serviceId);
        public ServiceModel GetServiceByServiceId(long serviceId);
        public Task UpdateServiceAsync(ServiceModel service);
        public Task CreateServiceAsync(ServiceModel service);
        public Task DeleteServiceAsync(ServiceModel service);
    }
}