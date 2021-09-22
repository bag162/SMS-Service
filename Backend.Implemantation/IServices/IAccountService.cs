using Models.DTO.DTOModels;
using Models.ImplementationModels.Enums;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IAccountService
    {
        public AccountModel[] GetAllActiveAccounts(int bucket = 3000);
        public AccountModel[] GetAllAccounts(int bucket = 3000);
        public Task<AccountModel> GetAccountByNumberAsync(string number, int bucket = 3000);
        public Task DeactivateAccountAsync(AccountModel account, AccountStatus status = AccountStatus.Inactive);
    }
}