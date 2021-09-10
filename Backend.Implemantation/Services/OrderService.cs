using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using TarantoolDB.Repositories;
using Models.ImplementationModels;
using Models.ImplementationModels.Enums;
using System.Collections.Generic;
using ProGaudi.Tarantool.Client;

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

        public async Task<OrderModel[]> GetAllOrdersByServiceAsync(long serviceId)
        {
            return orderRepository.Find(await orderRepository.Space.GetIndex("secondary_service"), serviceId);
        }

        public async Task UpdateOrderAsync(OrderModel order)
        {
            await orderRepository.Update(order);
        }
    }
}