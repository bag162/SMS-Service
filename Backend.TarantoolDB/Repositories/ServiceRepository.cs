using Backend.Models.DB;
using ProGaudi.Tarantool.Client;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.TarantoolDB.Repositories
{
    public class ServiceRepository : TRepository<ServiceModel>
    {
        public ServiceRepository(Box box) : base(box, vspace, viindex, dict) { }

        public static string vspace = "service";
        public static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(ServiceTFields))
               .Cast<ServiceTFields>()
               .ToDictionary(t => (int)t, t => t.ToString());

        public enum ServiceTFields
        {
            primary_index = 1,
            price = 2,
            parse_regular_exp = 3,
            service_prefix = 4,
            bucket_id = 5
        }
    }
}