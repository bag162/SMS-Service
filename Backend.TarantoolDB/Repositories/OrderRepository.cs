using Backend.DBInfrastructure;
using Backend.DBInfrastructure.Models;
using Backend.Models.DB;
using ProGaudi.Tarantool.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Backend.TarantoolDB.Repositories
{
    public class OrderRepository : TRepository<OrderModel>
    {
        public OrderRepository(Box box) : base(box, vspace, viindex, dict) { }

        private readonly static string vspace = "orders";
        private readonly static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(OrderTFields))
               .Cast<OrderTFields>()
               .ToDictionary(t => (int)t, t => t.ToString());

        public enum OrderTFields
        {
            primary_index = 1,
            status = 2,
            number = 3,
            user_id = 4,
            order_id = 5,
            service = 6,
            sms = 7,
            sms_code = 8,
            start_time = 9,
            last_check_time = 10,
            json_data = 11,
            bucket_id = 12
        }

        public async Task<OrderModel> GetNumber(int serviceId, int bucket)
        {
            var jsonData = new JsonRequestModel<OrderModel>()
            {
                service_id = serviceId
            };
            var result = await CallReplicaOne(GenerateVshardData(JsonSerializer.Serialize(jsonData), OtherDBOperation.GetNumber, bucket));
            return result;
        }
    }
}
