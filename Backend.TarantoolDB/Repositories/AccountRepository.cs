using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;
using System.Collections.Generic;
using System.Linq;

namespace TarantoolDB.Repositories
{
    public class AccountRepository : TRepository<AccountModel>
    {
        public AccountRepository(Box box) : base(box, vspace, viindex, dict) { }

        private readonly static string vspace = "account";
        private readonly static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(AccountTFields))
              .Cast<AccountTFields>()
              .ToDictionary(t => (int)t, t => t.ToString());

        public enum AccountTFields
        {
            primary_index = 1,
            login = 2,
            password = 3,
            number = 4,
            cookie = 5,
            status = 6,
            bucket_id = 7
        }
    }
}
