using Models.DTO.DTOModels;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IOrderService
    {
        public OrderModel[] GetAllOrders(int bucket = 4000);

        public Task<OrderModel[]> GetAllOrdersByServiceAsync(long serviceId, int bucket = 4000);
        public Task<OrderModel> GetOrderByOrderIdAsync(int orderId, int bucket = 4000);
        public Task<OrderModel[]> GetActiveOrdersAsync(int bucket = 4000);
        public Task<OrderModel> GetNumberAsync(int serviceId, int bucket = 4000);
        public Task SetStatusAsync(OrderModel order, OrderStatus status);

        public Task InsertOrderAsync(OrderModel order);

        public Task DeleteOrderByIdAsync(string id, int bucket = 4000);

        public Task SetSMSAndSMSCodeAsync(OrderModel order, string sms, string smsCode);

        public Task UpdateOrderAsync(OrderModel order);
    }
}
