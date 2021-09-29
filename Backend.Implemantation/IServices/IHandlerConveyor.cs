using Backend.Models.DB;
using Backend.Models.Implementation;
using System.Net.Http;
using System.Threading.Tasks;

namespace Backend.Implemantation.Services
{
    public interface IHandlerConveyor
    {
        public Task<HandlerConveerModel> GetHandlerAsync(AccountModel account, ProxyModel proxy = null, bool setcookie = true);
        public HttpClientHandler SetCookie(HttpClientHandler handler, string cookie);
        public HttpClientHandler SetProxy(HttpClientHandler handler, ProxyModel proxy);
    }
}
