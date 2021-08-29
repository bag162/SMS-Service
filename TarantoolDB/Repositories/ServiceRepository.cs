using DBInfrastructure.DTOModels;
using ProGaudi.Tarantool.Client;

namespace TarantoolDB.Repositories
{
    public class ServiceRepository : TRepository<ServiceModel>
    {
        public ServiceRepository(Box box) : base(box, vspace, viindex) { }

        private readonly static string vspace = "service";
        private readonly static string viindex = "primary_index";
    }
}