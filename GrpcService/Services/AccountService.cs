using AutoMapper;
using Grpc.Core;
using GrpcAccount;
using Implemantation.IServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Services
{
    public class AccountService : Account.AccountBase
    {
        private IAccountService accountService { get; set; }
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper mapper;
        public AccountService(IAccountService accountService,
            ILogger<AccountService> logger,
            IMapper mapper)
        {
            this.accountService = accountService;
            this._logger = logger;
            this.mapper = mapper;
        }

        public override Task<AccountList> GetAccounts(GetAccountsRequest requestData, ServerCallContext context)
        {
            var accounts = accountService.GetAllAccounts().OrderBy(x => x.Id).Take(requestData.Count);

            var accountModels = new List<AccountModel>();
            foreach (var account in accounts)
            {
                accountModels.Add(mapper.Map<AccountModel>(account));
            }

            var returnedAccounts = new AccountList();
            returnedAccounts.Accounts.AddRange(accountModels);

            return Task.FromResult(returnedAccounts);
        }
    }
}