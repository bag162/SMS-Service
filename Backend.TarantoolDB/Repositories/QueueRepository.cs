using Backend.Models.DB.Models;
using DBInfrastructure;
using ProGaudi.Tarantool.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TarantoolDB;

namespace Backend.TarantoolDB.Repositories
{
    public class QueueRepository : TRepository<QueueModel>
    {
        public QueueRepository(Box box) : base(box, vspace, viindex) { }

        private readonly static string vspace = "queue";
        private readonly static string viindex = "primary_index";
    }
}