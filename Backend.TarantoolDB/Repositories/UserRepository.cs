using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;
using System.Collections.Generic;
using System.Linq;

namespace TarantoolDB.Repositories
{
    public class UserRepository : TRepository<UserModel>
    {
        public UserRepository(Box box) : base(box, vspace, viindex, dict) {}

        private readonly static string vspace = "users";
        private readonly static string viindex = "primary_index";

        public static Dictionary<int, string> dict = System.Enum.GetValues(typeof(UserTFields))
               .Cast<UserTFields>()
               .ToDictionary(t => (int)t, t => t.ToString());

        public enum UserTFields 
        {
            primary_index = 1,
            login = 2,
            balance = 3,
            apikey = 4,
            bucket_id = 5
        }

    }
}