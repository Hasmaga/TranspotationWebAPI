using TranspotationAPI.Model.Dto;

namespace TranspotationAPI.Repositories
{
    public interface IAccountRepository
    {
        Task<GetUserInformationResDto> GetUserInformationByIdAsync(int accountId);
    }
}
