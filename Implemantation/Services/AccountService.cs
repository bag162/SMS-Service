using DBInfrastructure;
using DBInfrastructure.DTOModels;
using Implemantation.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace Implemantation.Services
{
    public class AccountService : IAccountService
    {
        private readonly TRepository<AccountModel> accountRepository;

        public AccountService(TRepository<AccountModel> accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task DeactivateAccountAsync(AccountModel account, int status)
        {
            await accountRepository.UpdateArgument(account.Id, 5, status);
            return;
        }

        public async Task<AccountModel> GetAccountByNumberAsync(string number)
        {
            return accountRepository.Find(await accountRepository.Space.GetIndex("secondary_number"), number).FirstOrDefault();
        }

        public AccountModel[] GetAllAccounts()
        {
            return accountRepository.FindAll().ToArray();
        }
    }
}