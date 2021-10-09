using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Models.DTO
{
    public class UserInfoDTO
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Telegram { get; set; }
        public double Balance { get; set; }
        public string ApiKey { get; set; }
    }
}