using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;
using System.Collections.Generic;
using System.Linq;

namespace TarantoolDB.Repositories
{
    public class ProxyRepository : TRepository<ProxyModel>
    {
        public ProxyRepository(Box box) : base(box, vspace, viindex, dict) { }

        private readonly static string vspace = "proxy";
        private readonly static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(ProxyTFields))
               .Cast<ProxyTFields>()
               .ToDictionary(t => (int)t, t => t.ToString());

        public enum ProxyTFields 
        {
            primary_index = 1,
            ip = 2,
            port = 3,
            login = 4,
            password = 5,
            status = 6,
            last_date_time_active = 7,
            external_ip = 8,
            bucket_id = 9
        }
    
    }
}
