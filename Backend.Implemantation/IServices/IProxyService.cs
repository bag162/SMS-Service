using Backend.Models.DB;
using System.Threading.Tasks;

namespace Backend.Implemantation.IServices
{
    public interface IProxyService
    {
        public Task<ProxyModel> GetRandomProxyAsync(int bucket = 5500);
        public Task<ProxyModel[]> GetAllProxyAsync(int bucket = 5500);

        public Task SetInvalidProxyAsync(ProxyModel proxy);
        public Task SetValidProxyAsync(ProxyModel proxy, string externalIp);
    }
}
