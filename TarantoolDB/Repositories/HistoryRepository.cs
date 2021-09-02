using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;

namespace TarantoolDB.Repositories
{
    public class HistoryRepository : TRepository<HistoryModel>
    {
        public HistoryRepository(Box box) : base(box, vspace, viindex) { }

        private readonly static string vspace = "history";
        private readonly static string viindex = "primary_index";
    }
}
