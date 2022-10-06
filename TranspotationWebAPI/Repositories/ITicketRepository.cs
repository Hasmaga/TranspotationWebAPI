using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public interface ITicketRepository
    {
        Task<List<GetTicketByAccountResDto>> GetAllTicketByAccount();
        Task<string> GetAccountEmailByToken();
    }
}
