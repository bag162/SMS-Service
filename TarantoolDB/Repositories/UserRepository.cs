using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;

namespace TarantoolDB.Repositories
{
    public class UserRepository : TRepository<UserModel>
    {
        public UserRepository(Box box) : base(box, vspace, viindex){}

        private readonly static string vspace = "users";
        private readonly static string viindex = "primary_index";
    }
}