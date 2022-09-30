using TranspotationWebAPI.Model.Dto;

namespace TranspotationAPI.Repositories
{
    //For add interface for AccountRepository
    public interface IAccountRepository
    {
        Task<GetUserInformationResDto> GetUserInformationByIdAsync(int accountId);        
        Task<List<GetAllUserInformationResDto>> GetAllUserInformationAsync();
        Task<CreateUpdateUserResDto> CreateUpdateUserAsync(CreateUpdateUserResDto user, int accountId);
        Task DeleteAccountByIdAsync(int accountId);
        Task<bool> ChangeStatusAccountByIdAsync(int accountId);
    }
}
