using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public interface ICompanyTripRepository
    {
        Task<string> GetCompanyByAccount();
        Task<CreateUpdateCompanyTripResDto> CreateCompanyTripByCompnayIdAsync(CreateUpdateCompanyTripResDto createCompanyTrip);
        Task<CreateUpdateCompanyTripResDto> UpdateCompanyTripByCompnayIdAsync(CreateUpdateCompanyTripResDto updateCompanyTrip,int id);
        Task<List<ReadCompanyTripResDto>> GetAllCompanyTripByCompanyIdAsync();
    }
}
