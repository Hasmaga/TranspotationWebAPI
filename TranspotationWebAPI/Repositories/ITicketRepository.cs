using TranspotationWebAPI.Model;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public interface ITicketRepository
    {
        Task<List<GetTicketByAccountResDto>> GetAllTicketByAccount();
        Task<string> GetAccountEmailByToken();
        Task<bool> UpdateTicketByTokenAsync(UpdateTicketByTokenResDto updateTicket, int id);
        Task DeleteTicketByTokenAsync(int id);
        Task<Ticket> FindTicketByIdAsync(int id);
        Task<List<GetAllTicketByAccountWithStatusResDto>> GetAllTicketByAccountWithStatus(bool status);
    }
}
