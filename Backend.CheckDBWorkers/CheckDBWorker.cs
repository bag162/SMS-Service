using Backend.Models.DB;
using Backend.Implemantation.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models.Implementation.Enums;

namespace SMS_Service_Worker.Workers.CheckerDBWorker
{
    public class CheckDBWorker
    {
        private readonly IAccountService accountService;
        private readonly IOrderService orderService;
        private readonly IServicePricesService serviceEntity;

        public CheckDBWorker(IAccountService accountService, 
            IOrderService orderService, 
            IServicePricesService serviceEntity)
        {
            this.accountService = accountService;
            this.orderService = orderService;
            this.serviceEntity = serviceEntity;
        }
        
        public async Task CheckDBInsert()
        {
            var allAccounts = accountService.GetAllAccounts();
            var allOrders = orderService.GetAllOrders();
            var allServices = serviceEntity.GetAllServices();
            int countServices = allServices.Count();

            foreach (var account in allAccounts)
            {
                var OrdersByAccNumber = allOrders
                    .AsQueryable()
                    .Where(x => x.Number == account.Number)
                    .Select(x => x.Service); // полчение id сервисов которые уже зарегестрированы для данного аккаунта

                if (OrdersByAccNumber.Count() != countServices) // если кол-во сервисов не совпадает с текущим кол-во сервисов то идём дальше
                {
                    foreach (var service in allServices) // проходимся по циклу всех текущих сервисов
                    {
                        if (!OrdersByAccNumber.Contains((int)service.Id))
                        {
                            orderService.InsertOrderAsync(new OrderModel() 
                            { 
                                Id = Guid.NewGuid().ToString(), 
                                Number = account.Number, 
                                OrderId = 0, 
                                Service = (int)service.Id,
                                Status = (int)OrderStatus.STATUS_FREE,
                                Bucket = 4000});
                        }
                    }
                }
            }
            return;
        }

        public async Task CheckDBDelete()
        {
            var allAccounts = accountService.GetAllAccounts();
            var allOrders = orderService.GetAllOrders();


            var inactiveAccounts = allAccounts
                    .AsQueryable()
                    .Where(x => x.Status == (int)AccountStatus.Inactive);

            foreach (var account in inactiveAccounts) // проверям есть ли в базе ордеров, номера аккаунтов у которых статус (int)AccountStatus.Inactive
            {
                var deletedOrders = allOrders
                    .AsQueryable()
                    .Where(x => x.Number == account.Number);

                if (deletedOrders.Any())
                {
                    foreach (var deletedOrder in deletedOrders)
                    {
                        orderService.SetStatusAsync(deletedOrder, OrderStatus.RESERVE);
                    }
                }
            }

            foreach (var order in allOrders) // проверяем есть ли в базе аккаунт, с таким же номером как в оредере. Если такого аккаунта нет, то ордер не может быть обслужен, и его необходимо удалить.
            {
                var ordersInAccBase = allAccounts
                    .AsQueryable()
                    .Where(x => x.Number == order.Number);

                if (!ordersInAccBase.Any())
                {
                    orderService.DeleteOrderByIdAsync(order.Id);
                }
            }

            return;
        }
    }
}