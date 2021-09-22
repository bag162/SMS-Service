using Backend.DBInfrastructure;
using Backend.DBInfrastructure.Models;
using Backend.Models.DB.Models;
using DBInfrastructure;
using ProGaudi.Tarantool.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TarantoolDB;

namespace Backend.TarantoolDB.Repositories
{
    public class QueueRepository : TRepository<QueueModel>
    {
        public QueueRepository(Box box) : base(box, vspace, viindex, dict) { }

        private readonly static string vspace = "queue";
        private readonly static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(QueueTFields))
               .Cast<QueueTFields>()
               .ToDictionary(t => (int)t, t => t.ToString());

        public enum QueueTFields
        {
            primary_index = 1,
            type = 2,
            data = 3,
            priority = 4,
            bucket_id = 5
        }
    
        public async Task<IEnumerable<QueueModel>> GetTasks(int count, int bucket = 1000)
        {
            var jsonData = new JsonRequestModel<QueueModel>()
            {
                count_tasks = count
            };
            var result = await CallReplicaArray(GenerateVshardData(JsonSerializer.Serialize(jsonData), OtherDBOperation.GetTask, bucket));
            return result.AsEnumerable();
        }
    }
}