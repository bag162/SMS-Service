using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProGaudi.Tarantool.Client;
using ProGaudi.Tarantool.Client.Model;
using ProGaudi.Tarantool.Client.Model.Enums;
using SMS_Service_Worker.Data.Context;

namespace SMS_Service_Worker.API.PrivateWEB.GetNumber.Controllers
{
    public class GetNumberController : Controller
    {
        [Obsolete]
        [Route("test")]
        [HttpGet]
        public async Task<JsonResult> testAsync()
        {
            using (var box = await Box.Connect(
                "vlad:111vlaD111@localhost:3301"))
            {
                var schema = box.GetSchema();

                var space = await schema.GetSpace("users");
                var primaryIndex = await space.GetIndex("primary_id");

                var data = await primaryIndex.Select<TarantoolTuple<string>,
                    TarantoolTuple<string, string, string, string, long>>(
                    TarantoolTuple.Create(String.Empty), new SelectOptions
                    {
                        Iterator = Iterator.All
                    });

                foreach (var item in data.Data)
                {
                    Console.WriteLine(item);
                }
            }
            return null;
        }
    }
}
