using DBInfrastructure.DTOModels;
using ProGaudi.Tarantool.Client;

namespace TarantoolDB.Repositories
{
    public class AccountRepository : TRepository<AccountModel>
    {
        public AccountRepository(Box box) : base(box, vspace, viindex) { }

        private readonly static string vspace = "account";
        private readonly static string viindex = "primary_index";
    }
}
