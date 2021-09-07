using Models.DTO.DTOModels;
using ProGaudi.Tarantool.Client;

namespace TarantoolDB.Repositories
{
    public class OrderRepository : TRepository<OrderModel>
    {
        public OrderRepository(Box box) : base(box, vspace, viindex) { }

        private readonly static string vspace = "orders";
        private readonly static string viindex = "primary_index";
    }
}
