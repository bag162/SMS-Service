using AutoMapper;
using Models.DTO.DTOModels;
using System.Collections.Generic;

namespace Client.gRPC.Services
{
    public class AccountService
    {
        private readonly gRPCClient Client;
        private readonly IMapper mapper;
        private readonly GrpcServiceAccount.Account.AccountClient accountClient;

        public AccountService(gRPCClient client,
            IMapper mapper)
        {
            this.Client = client;
            this.mapper = mapper;
            this.accountClient = new GrpcServiceAccount.Account.AccountClient(Client.channel);
        }

        public IEnumerable<AccountModel> GetAccounts(int count)
        {
            var response = accountClient.GetAccounts(new GrpcServiceAccount.GetAccountsRequest() { Count = count} );
            var accountsList = new List<AccountModel>();

            foreach (var account in response.Accounts)
            {
                accountsList.Add(mapper.Map<AccountModel>(account));
            }
            return accountsList;
        }
    }
}
