using Models.DTO.DTOModels;
using Models.ImplementationModels.Enums;
using System.Threading.Tasks;

namespace Implemantation.IServices
{
    public interface IAccountService
    {
        public AccountModel[] GetAllAccounts();
        public Task<AccountModel> GetAccountByNumberAsync(string number);
        public Task DeactivateAccountAsync(AccountModel account, int status = (int)AccountStatus.Inactive);
    }
}
