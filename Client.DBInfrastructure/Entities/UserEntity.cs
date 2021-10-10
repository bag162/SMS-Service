using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.DataBase.Contexts
{
    public class UserEntity
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Telegram { get; set; }
        public long IdRole { get; set; }
    }
}