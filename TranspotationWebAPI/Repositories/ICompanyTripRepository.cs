using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public interface ICompanyTripRepository
    {
        Task<string> GetCompanyByAccount();
        Task<CreateUpdateCompanyTripResDto> CreateCompanyTripByCompnayIdAsync(CreateUpdateCompanyTripResDto createCompanyTrip);
        Task<CreateUpdateCompanyTripResDto> UpdateCompanyTripByCompnayIdAsync(CreateUpdateCompanyTripResDto updateCompanyTrip,int id);
        Task<List<ReadCompanyTripResDto>> GetAllCompanyTripByCompanyIdAsync();
        Task<bool> ChangeStatusCompanyTripByCompanyIdAsync(int id);
        Task<bool> DeleteCompanyTripByCompanyIdAsync(int id);
    }
}
