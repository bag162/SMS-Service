
using Grpc.Core;
using Implemantation.IServices;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class AccountService : Greeter.GreeterBase
    {
        private IAccountService accountService { get; set; }

        public AccountService(
            IAccountService accountService,
            ILogger<GreeterService> ILogger) : base(ILogger)
        {
            this.accountService = accountService;
        }

        public override Task<AccountList> GetAccounts(GetAccountsRequest accounts, ServerCallContext context)
        {
            var returnedAcceounts = new AccountList();
            returnedAcceounts.Accounts.Add(new AccountModel() { Id = "asd", Cookie = "test", Login = "test", Number = "test", Password = "test", Status = Status.Active });
            return Task.FromResult(returnedAcceounts);
        }
    }
}
