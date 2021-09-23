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
using static TarantoolDB.Repositories.OrderRepository;

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

        public async Task<OrderModel> GetOrderByOrderIdAsync(int orderId, int bucket = 4000)
        {
            return orderRepository.Find(orderId, (int)OrderTFields.order_id, bucket).Result.FirstOrDefault();
        }
        public async Task<OrderModel[]> GetActiveOrdersAsync(int bucket = 4000)
        {
            return orderRepository.Find((int)OrderStatus.STATUS_WAIT_CODE, (int)OrderTFields.status, bucket).Result.ToArray();
        }
        public OrderModel[] GetAllOrders(int bucket = 4000)
        {
            return orderRepository.FindAll(bucket).Result.ToArray();
        }
        public async Task<OrderModel[]> GetAllOrdersByServiceAsync(long serviceId, int bucket = 4000)
        {
            return orderRepository.Find(serviceId, (int)OrderTFields.service, bucket).Result.ToArray();
        }

        public async Task SetStatusAsync(OrderModel order, OrderStatus status)
        {
            var updatedOrder = order;
            updatedOrder.Status = (int)status;
            orderRepository.Update(updatedOrder, (int)order.Bucket);
            return;
        }
        public async Task SetSMSAndSMSCodeAsync(OrderModel order, string sms, string smsCode)
        {
            var updatedOrder = order;
            updatedOrder.SMS = sms;
            updatedOrder.SMSCode = smsCode;
            orderRepository.Update(updatedOrder, (int)order.Bucket);
            return;
        }

        public async Task InsertOrderAsync(OrderModel order)
        {
            orderRepository.Create(order, (int)order.Bucket);
            return;
        }

        public async Task DeleteOrderByIdAsync(string id, int bucket = 4000)
        {
            orderRepository.Delete(new OrderModel { Id = id}, bucket);
            return;
        }

        public async Task UpdateOrderAsync(OrderModel order)
        {
            await orderRepository.Update(order, (int)order.Bucket);
            return;
        }

        public async Task<OrderModel> GetNumberAsync(int serviceId, int bucket = 4000)
        {
            return await orderRepository.GetNumber(serviceId, bucket);
        }
    }
}