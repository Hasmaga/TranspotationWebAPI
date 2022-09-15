using TranspotationAPI.Model;
using TranspotationAPI.Model.Dto;


namespace TranspotationAPI.Repositories
{
    //For add interface for AccountRepository
    public interface IAccountRepository
    {
        Task<GetUserInformationResDto> GetUserInformationByIdAsync(int accountId);
        
        Task<List<Account>> GetAllUserInformationAsync();
    }
}
