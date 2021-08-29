using DBInfrastructure.DTOModels;
using ProGaudi.Tarantool.Client;

namespace TarantoolDB.Repositories
{
    public class ProxyRepository : TRepository<ProxyModel>
    {
        public ProxyRepository(Box box) : base(box, vspace, viindex) { }

        private readonly static string vspace = "proxy";
        private readonly static string viindex = "primary_index";
    }
}
