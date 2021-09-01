using Implemantation.IServices;

namespace GrpcService.Services
{
    public class AccountService : Greeter.GreeterBase
    {
        private readonly IAccountService accountService { get; set; }

        public AccountService(IAccountService accountService)
        {
            this.accountService = accountService;
        }
    }
}
