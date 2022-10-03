using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public interface ICompanyTripRepository
    {
        Task<string> GetCompanyByAccount();

        Task<CreateCompanyTripResDto> CreateCompanyTripByCompnayIdAsync(CreateCompanyTripResDto createCompanyTrip);
    }
}
