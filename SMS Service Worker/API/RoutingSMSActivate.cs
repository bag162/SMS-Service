using Models.DTO.DTOModels;
using Implemantation.IServices;
using Microsoft.AspNetCore.Mvc;

namespace SMS_Service_Worker.API.PrivateWEB
{
    public class RoutingSMSActivate : Controller
    {
        private readonly IUserService userService;
        public RoutingSMSActivate(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet]
        [Route("stubs/handler_api.php")]
        public RedirectToRouteResult RoutingController(string api_key, [FromQuery] string action, string service, int id, int status)
        {
            UserModel user = userService.GetUserByApiKey(api_key);
            if (user == null)
            {
                return RedirectToRoute(new { controller = "ErrorResponse", action = "BadKey" });
            }

            switch (action)
            {
                case "getNumber":
                    return RedirectToRoute(new { controller = "GetNumber", action = "GetNumber", api_key = api_key, service = service });

                case "getBalance":
                    return RedirectToRoute(new { controller = "GetBalance", action = "GetBalance", api_key = api_key });

                case "getStatus":
                    return RedirectToRoute(new { controller = "GetStatus", action = "GetStatus", api_key = api_key, id = id });

                case "setStatus":
                    return RedirectToRoute(new { controller = "SetStatus", action = "SetStatus", api_key = api_key, id = id, status = status });

                default:
                    return RedirectToRoute(new { controller = "ErrorResponse", action = "BadAction" } ); // если такого экшена нет, то редирект на bad action response
            }
        }
    }
}