using DBInfrastructure.DTOModels;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IProxyService
    {
        public ProxyModel GetRandomProxy();
        public ProxyModel[] GetAllProxy();
        public Task SetInvalidProxyAsync(ProxyModel proxy);
        public Task SetValidProxyAsync(ProxyModel proxy, string externalIp);
    }
}
