using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;
using System.Threading.Tasks;

namespace TarantoolDB.Repositories
{
    public class ServiceRepository : TRepository<ServiceModel>
    {
        public ServiceRepository(Box box) : base(box, vspace, viindex) { }

        public readonly static string vspace = "service";
        public readonly static string viindex = "primary_index";
    }
}