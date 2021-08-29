using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using Implemantation.Models;
using Implemantation.Models.Enums;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using static Implemantation.Models.JsonModels.Cookie;

namespace Implemantation.Services
{
    public class HandlerConveyor : IHandlerConveyor
    {
        private readonly IHistoryService historyService;
        private readonly IProxyService proxyService;

        public HandlerConveyor(
            IHistoryService historyService,
            IProxyService proxyService)
        {
            this.historyService = historyService;
            this.proxyService = proxyService;
        }

        public async Task<HandlerConveerModel> GetHandler(AccountModel account, bool setProxy = true, bool setcookie = true)
        {
            HttpClientHandler handler = new();

            if (setProxy)
            {
                ProxyModel proxy = proxyService.GetRandomProxy();
                if (proxy == null)
                {
                    await historyService.InputNewHistoryAsync("0", (int)TypeRequests.NoProxy);
                    return new HandlerConveerModel { status = HandlerConveerStatus.NoProxy };
                }
                handler = SetProxy(handler, proxy);
            }

            if (setcookie)
            {
                if (account.Cookie == null)
                {
                    await historyService.InputNewHistoryAsync("0", (int)TypeRequests.NoCookie, "Cookie invalid");
                    return new HandlerConveerModel { status = HandlerConveerStatus.InvalidCookie };
                }
                handler = Setcookie(handler, account.Cookie);
            }

            return new HandlerConveerModel { handler = handler, status = HandlerConveerStatus.Success };
        }

        public HttpClientHandler Setcookie(HttpClientHandler handler, string cookie)
        {
            CookieContainer cookieContainer = new();
            CookieRootModel deserializedcookie = JsonConvert.DeserializeObject<CookieRootModel>(cookie);

            foreach (var cookieItem in deserializedcookie.cookies)
            {
                if (cookieItem.domain == "www.textnow.com")
                {
                    var baseAddress = new Uri("http://www.textnow.com");
                    cookieContainer.Add(baseAddress, new Cookie(cookieItem.name, cookieItem.value));
                }

            }
            handler.CookieContainer = cookieContainer;
            return handler;
        }

        public HttpClientHandler SetProxy(HttpClientHandler handler, ProxyModel proxy)
        {
            WebProxy requestProxy = new()
            {
                Address = new Uri($"http://{proxy.Ip}:{proxy.Port}"),
                BypassProxyOnLocal = false,
                UseDefaultCredentials = false,

                Credentials = new NetworkCredential(
                    userName: proxy.Login,
                    password: proxy.Password)
            };
            handler.Proxy = requestProxy;
            return handler;
        }
    }
}