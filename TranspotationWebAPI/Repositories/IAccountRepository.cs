using TranspotationWebAPI.Model;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationAPI.Repositories
{
    //For add interface for AccountRepository
    public interface IAccountRepository
    {
        Task<GetUserInformationResDto> GetUserInformationByIdAsync(int accountId);        
        Task<List<GetAllUserInformationResDto>> GetAllUserInformationAsync();
        Task<CreateAccountResDto> CreateAccountAsync(CreateAccountResDto acc);
        Task DeleteAccountByIdAsync(int accountId);
        Task<bool> ChangeStatusAccountByIdAsync(int accountId);
        Task<String> LoginAndReturnTokenAsync(string email, string passwordHash);
        Task<RegistrationUserResDto> RegistrationUserAsync(RegistrationUserResDto user);
        Task CheckEmailExistAsync(string email);
        Task<UpdateInfoUserResDto> UpdateUserInfoAsync(UpdateInfoUserResDto user, int id);
        Task<GetUserInfoByIdResDto> GetUserInfoByIdAsync(int id);
    }
}
