using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using System.Linq;
using System.Threading.Tasks;
using TarantoolDB.Repositories;
using static TarantoolDB.Repositories.AccountRepository;
using Models.ImplementationModels.Enums;

namespace Implemantation.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task DeactivateAccountAsync(AccountModel account, AccountStatus status)
        {
            var updatedAccount = account;
            updatedAccount.Status = (int)status;
            accountRepository.Update(updatedAccount, (int)updatedAccount.Bucket);
            return;
        }

        public async Task<AccountModel> GetAccountByNumberAsync(string number, int bucket = 3000)
        {
            return accountRepository.Find(number, (int)AccountTFields.number, bucket).Result.FirstOrDefault();
        }
        public AccountModel[] GetAllAccounts(int bucket = 3000)
        {
            return accountRepository.FindAll(bucket).Result.ToArray();
        }
        public AccountModel[] GetAllActiveAccounts(int bucket = 3000)
        {
            return accountRepository.Find((int)AccountStatus.Active, (int)AccountTFields.status, bucket).Result.ToArray();
        }
    }
}