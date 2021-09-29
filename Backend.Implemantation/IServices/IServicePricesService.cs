using Backend.Models.DB;
using Backend.Models.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Implemantation.IServices
{
    public interface IServicePricesService
    {
        public double GetServicePriceByServiceId(long serviceIndex, int bucket = 5000);
        public IEnumerable<ServiceModel> GetAllServices(int bucket = 5000);
        public JsonExpression GetAllExpressionByServiceId(long serviceId, int bucket = 5000);
        public ServiceModel GetServiceByServiceId(long serviceId, int bucket = 5000);

        public Task UpdateServiceAsync(ServiceModel service);

        public Task CreateServiceAsync(ServiceModel service);

        public Task DeleteServiceAsync(ServiceModel service);
    }
}