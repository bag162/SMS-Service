using Models.DTO.DTOModels;
using Models.ImplementationModels;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IOrderService
    {
        public OrderModel[] GetAllOrders();
        public Task<GetNumberModel> GetNumberAsync(GetNumberModel user);
        public Task<OrderModel> GetOrderByOrderIdAsync(int orderId);
        public Task SetStatusAsync(OrderModel order, int status);
        public Task InsertOrderAsync(OrderModel order);
        public Task DeleteOrderByIdAsync(string id);
        public Task SetSMSAndSMSCodeAsync(OrderModel order, string sms, string smsCode);
    }
}
