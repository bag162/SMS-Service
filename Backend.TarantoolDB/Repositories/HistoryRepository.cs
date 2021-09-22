using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;
using System.Collections.Generic;
using System.Linq;

namespace TarantoolDB.Repositories
{
    public class HistoryRepository : TRepository<HistoryModel>
    {
        public HistoryRepository(Box box) : base(box, vspace, viindex, dict) { }

        private readonly static string vspace = "history";
        private readonly static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(HistoryTFields))
               .Cast<HistoryTFields>()
               .ToDictionary(t => (int)t, t => t.ToString());

        public enum HistoryTFields
        {
            primary_index = 1,
            type_request = 2,
            user_id = 3,
            date_time = 4,
            message = 5,
            bucket_id = 6
        }
    }
}
