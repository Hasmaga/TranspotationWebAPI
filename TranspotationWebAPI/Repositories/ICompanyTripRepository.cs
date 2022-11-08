using TranspotationWebAPI.Model;
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
        Task<List<GetAllCompanyTripResDto>> GetAllCompanyTripAsync();
        Task<List<GetCompanyTripByTripIdResDto>> GetCompanyTripByTripIdAsync(int id);
        Task<int> GetTotalSeatByCompanyIdAsync(int id);
        Task<List<GetAllCompanyTripResDto>> GetCompanyTripFromLocationNameFromAndToAsync(string from, string to);
        Task<List<string>> CreateSeatListAsync(int totalSeat);
        Task<List<string>> GetLocationAsync();
    }
}
