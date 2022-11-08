using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TranspotationAPI.DbContexts;
using TranspotationAPI.Enum;
using TranspotationWebAPI.Model;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _mdb;
        private IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketRepository(ApplicationDbContext _db, IMapper mapper, ILogger<TicketRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _mdb = _db;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<string> GetAccountEmailByToken()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
            return Task.FromResult(result);
        }

        public Task<string> GetAccountRoleByToken()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return Task.FromResult(result);
        }

        public Task<string> GetAccountIdByToken()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            }
            return Task.FromResult(result);
        }    

        public async Task<List<GetTicketByAccountResDto>> GetAllTicketByAccount()
        {
            string accEmail = await GetAccountEmailByToken();
            if (accEmail == null)
            {
                throw new UnauthorizedAccessException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            _logger.LogInformation($"Get Ticket with AccountEmail: {accEmail}");
            var query = from r in _mdb.Ticket
                        join c in _mdb.Account on r.AccountId equals c.Id
                        where c.Email == accEmail
                        select new GetTicketByAccountResDto
                        {
                            Status = r.Status,
                            Total = r.Total,
                            CompanyTripId = r.CompanyTripId,
                            SeatName = r.SeatName,
                            Description = r.Description
                        };
            var list = await query.ToListAsync();
            if (list == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            return list;
        }

        public async Task<bool> UpdateTicketByTokenAsync(UpdateTicketByTokenResDto updateTicket, int id)
        {
            var role = await GetAccountRoleByToken();
            if (role != "1")
            {
                throw new Exception(ErrorCode.ACCOUNT_BLOCKED);
            }
            _logger.LogInformation($"Update Ticket by admin");
            var ticket = await _mdb.Ticket.FirstOrDefaultAsync(x => x.Id == id);
            if (ticket == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            ticket.Status = updateTicket.Status;
            ticket.Total = updateTicket.Total;            
            ticket.SeatName = updateTicket.SeatName;
            ticket.Description = updateTicket.Description;
            _mdb.Ticket.Update(ticket);
            await _mdb.SaveChangesAsync();
            return true;
        }

        public async Task<Ticket> FindTicketByIdAsync(int id)
        {
            _logger.LogInformation($"Find Ticket By Id: {id}");
            Ticket ticket = await _mdb.Ticket.FindAsync(id);
            if(ticket == null) 
            {
                _logger.LogInformation($"Ticket Not Found");
                throw new KeyNotFoundException(ErrorCode.NOT_FOUND);
            }
            return ticket;
        }

        public async Task DeleteTicketByTokenAsync(int id)
        {
            var role = await GetAccountRoleByToken();
            if (role != "1")
            {
                throw new Exception(ErrorCode.ACCOUNT_BLOCKED);
            }
            Ticket ticket = await this.FindTicketByIdAsync(id);
            if (ticket!= null)
            {
                _mdb.Ticket.Remove(ticket);
            }
            else
            {
                _logger.LogError($"Account not found");
                throw new KeyNotFoundException(ErrorCode.NOT_FOUND);
            }
            await _mdb.SaveChangesAsync();
        }

        public async Task<List<GetAllTicketByAccountWithStatusResDto>> GetAllTicketByAccountWithStatus(bool status)
        {
            string accEmail = await GetAccountEmailByToken();
            if (accEmail == null)
            {
                throw new UnauthorizedAccessException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            _logger.LogInformation($"Get Ticket with AccountEmail: {accEmail}");
            var query = from r in _mdb.Ticket
                        join c in _mdb.Account on r.AccountId equals c.Id
                        join t in _mdb.CompanyTrip on r.CompanyTripId equals t.Id
                        join dt in _mdb.Company on t.CompanyId equals dt.Id
                        join ct in _mdb.CarType on t.CarTypeId equals ct.Id
                        join mt in _mdb.Trip on t.TripId equals mt.Id                        
                        where c.Email == accEmail && r.Status == status
                        select new GetAllTicketByAccountWithStatusResDto
                        {
                            Status = r.Status,
                            Total = r.Total,
                            CompanyName = dt.Name,
                            CarTypeName = ct.Name,
                            StartDateTime = t.StartDateTime,
                            FromLocation = mt.From.Name,
                            ToLocation = mt.To.Name,
                            SeatName = r.SeatName,
                            Description = r.Description
                        };
            var list = await query.ToListAsync();
            if (list == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            return list;
        }

        public async Task<bool> CreateTicketByAccount(CreateTicketByAccountResDto createTicket)
        {
            int accountId = int.Parse(await GetAccountIdByToken());
            
            if (accountId == 0)
            {
                throw new UnauthorizedAccessException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            _logger.LogInformation($"Create Ticket with AccountEmail: {accountId}");
            Ticket ticket = new Ticket
            (
                true,
                _mdb.CompanyTrip.Where(x => x.Id == createTicket.CompanyTripId).Select(x => x.Price).FirstOrDefault(),
                accountId,
                createTicket.CompanyTripId,
                createTicket.SeatName,
                createTicket.Description                
            );
            _mdb.Ticket.Add(ticket);
            await _mdb.SaveChangesAsync();
            return true;
        }

        public async Task<List<string>> GetSeatSelectedByCompanyTripId(int companyTripId)
        {
            _logger.LogInformation($"Get Seat Selected By Company Trip Id: {companyTripId}");
            var query = from r in _mdb.Ticket
                        where r.CompanyTripId == companyTripId
                        select r.SeatName;
            var list = await query.ToListAsync();            
            return list;
        }
    }
}
