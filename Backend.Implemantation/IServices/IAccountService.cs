using Backend.Models.DB;
using Backend.Models.Implementation.Enums;
using System.Threading.Tasks;

namespace Backend.Implemantation.IServices
{
    public interface IAccountService
    {
        public AccountModel[] GetAllActiveAccounts(int bucket = 3000);
        public AccountModel[] GetAllAccounts(int bucket = 3000);
        public Task<AccountModel> GetAccountByNumberAsync(string number, int bucket = 3000);
        public Task DeactivateAccountAsync(AccountModel account, AccountStatus status = AccountStatus.Inactive);
    }
}