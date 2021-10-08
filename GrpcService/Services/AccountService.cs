using AutoMapper;
using gRPCAccount;
using Grpc.Core;
using Backend.Implemantation.IServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class AccountService : Account.AccountBase
    {
        private IAccountService accountService { get; set; }
        private readonly ILogger<AccountService> logger;
        private readonly IMapper mapper;
        public AccountService(IAccountService accountService,
            ILogger<AccountService> logger,
            IMapper mapper)
        {
            this.accountService = accountService;
            this.logger = logger;
            this.mapper = mapper;
        }

        public override Task<AccountArray> GetAccounts(GetAccountsRequest requestData, ServerCallContext context)
        {
            var accounts = accountService.GetAllAccounts().OrderBy(x => x.Id).Take(requestData.Count);

            var accountModels = new List<AccountModel>();
            foreach (var account in accounts)
            {
                accountModels.Add(mapper.Map<AccountModel>(account));
            }

            var returnedAccounts = new AccountArray();
            returnedAccounts.Accounts.AddRange(accountModels);

            return Task.FromResult(returnedAccounts);
        }
    }
}