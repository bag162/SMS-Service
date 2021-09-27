using Models.DTO.DTOModels;
using Implemantation.IServices;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using static Models.ImplementationModels.JsonModels.Cookie;
using System.Text.Json;

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

        public async Task<HandlerConveerModel> GetHandlerAsync(AccountModel account, ProxyModel proxy = null, bool setcookie = true)
        {
            HttpClientHandler handler = new();
            var usedProxy = proxy;

            if (proxy == null)
                usedProxy = proxyService.GetRandomProxyAsync().Result;

            if (usedProxy == null)
                return new HandlerConveerModel { status = HandlerConveerStatus.NoProxy };

            handler = SetProxy(handler, usedProxy);

            if (setcookie)
            {
                if (account.Cookie == null)
                    return new HandlerConveerModel { status = HandlerConveerStatus.NoCookie };

                handler = SetCookie(handler, account.Cookie);

                if (handler == null)
                    return new HandlerConveerModel { status = HandlerConveerStatus.IncorrectCookie };
            }

            return new HandlerConveerModel { handler = handler, status = HandlerConveerStatus.Success };
        }

        public HttpClientHandler SetCookie(HttpClientHandler handler, string cookie)
        {
            CoockieRootModel deserializedcookie;
            CookieContainer cookieContainer = new();
            try
            {
                deserializedcookie = JsonSerializer.Deserialize<CoockieRootModel>(cookie);
            }
            catch
            {
                return null;
            }

            foreach (var cookieItem in deserializedcookie.Cookies)
            {
                if (cookieItem.Domain == "www.textnow.com")
                {
                    var baseAddress = new Uri("http://www.textnow.com");
                    cookieContainer.Add(baseAddress, new Cookie(cookieItem.Name, cookieItem.Value));
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