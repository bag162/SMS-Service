using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using TarantoolDB.Repositories;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;

namespace Implemantation.Services
{
    public class OrderService : IOrderService
    {
        public readonly Random Randomizer = new();
        private readonly OrderRepository orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<GetNumberModel> GetNumberAsync(GetNumberModel model)
        {
            var orderList = orderRepository.Find((await orderRepository.Space.GetIndex("secondary_service")), model.Service); // получение ордеров с необходимым сервисом
            OrderModel order = orderList
                .AsQueryable()
                .Where(x => x.Status == (int)OrderStatuses.STATUS_FREE)
                .FirstOrDefault(); // получение 1 ордера из выборки со статусом free

            if (order == null) 
            {
                return new GetNumberModel { FailMessage = "NO_NUMBER", StatusCode = 401, Success = false };
            } // если таких ордеров нет, то выдать ответ "номеров нет"

            int orderId = Randomizer.Next(10000000, 99999999); // генерация id ордера
            string dateTime = DateTime.Now.ToString();

            OrderModel updatedOrder = new() { 
                Id = order.Id, Status = (int)OrderStatuses.STATUS_WAIT_CODE, 
                Number = order.Number, 
                OrderId = orderId, 
                Service = order.Service, 
                StartDateTime = dateTime, 
                UserId = model.User.Id };

            await orderRepository.Update(updatedOrder);
            return new GetNumberModel { Success = true, Number = order.Number, OrderId = orderId, Service = model.Service, User = model.User };
        }

        public async Task<OrderModel> GetOrderByOrderIdAsync(int orderId)
        {
            return orderRepository.Find(await orderRepository.Space.GetIndex("secondary_orderid"), orderId).FirstOrDefault();
        }

        public async Task SetStatusAsync(OrderModel order, int status)
        {
            await orderRepository.UpdateArgument(order.Id, 1, status);
            return;
        }
         
        public OrderModel[] GetAllOrders()
        {
            return orderRepository.FindAll().ToArray();
        }

        public async Task InsertOrderAsync(OrderModel order)
        {
            await orderRepository.Create(order);
            return;
        }

        public async Task DeleteOrderByIdAsync(string id)
        {
            await orderRepository.Delete(new OrderModel { Id = id});
            return;
        }

        public async Task SetSMSAndSMSCodeAsync(OrderModel order, string sms, string smsCode)
        {
            await orderRepository.UpdateArgument(order.Id, 6, sms);
            await orderRepository.UpdateArgument(order.Id, 7, smsCode);
            return;
        }
    }
}