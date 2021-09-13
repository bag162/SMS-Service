using DBInfrastructure;
using Models.DTO.DTOModels;
using Implemantation.IServices;
using System.Linq;
using System.Threading.Tasks;
using TarantoolDB.Repositories;

namespace Implemantation.Services
{
    public class AccountService : IAccountService
    {
        private readonly AccountRepository accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public async Task DeactivateAccountAsync(AccountModel account, int status)
        {
            var updatedAccount = account;
            updatedAccount.Status = status;
            await accountRepository.Update(updatedAccount);
            return;
        }

        public async Task<AccountModel> GetAccountByNumberAsync(string number)
        {
            /*return accountRepository.Find(await accountRepository.Space.GetIndex("secondary_number"), number).FirstOrDefault();*/ // TODO
            return null;
        }

        public AccountModel[] GetAllAccounts()
        {
            return accountRepository.FindAll().Result.ToArray();
        }
    }
}