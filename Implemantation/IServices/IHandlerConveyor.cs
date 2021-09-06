using Models.DTO.DTOModels;
using Models.ImplementationModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace Implemantation.Services
{
    public interface IHandlerConveyor
    {
        public Task<HandlerConveerModel> GetHandler(AccountModel account, bool setProxy = true, bool setcookie = true);
        public HttpClientHandler Setcookie(HttpClientHandler handler, string cookie);
        public HttpClientHandler SetProxy(HttpClientHandler handler, ProxyModel proxy);
    }
}
