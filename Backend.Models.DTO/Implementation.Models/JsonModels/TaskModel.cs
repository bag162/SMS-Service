using Backend.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Models.Implementation.JsonModels
{
    public class TaskModel
    {
        public OrderModel Order { get; set; }
        public UserModel User { get; set; }
        public AccountModel Account { get; set; }
        public ProxyModel Proxy { get; set; }
        public string FirstMessage { get; set; }
    }
}
