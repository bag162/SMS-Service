using Models.DTO.DTOModels;
using Models.ImplementationModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Implemantation.Services
{
    public interface IHandlerConveyor
    {
        public Task<HandlerConveerModel> GetHandlerAsync(AccountModel account, ProxyModel proxy = null, bool setcookie = true);
        public HttpClientHandler SetCookie(HttpClientHandler handler, string cookie);
        public HttpClientHandler SetProxy(HttpClientHandler handler, ProxyModel proxy);
    }
}
