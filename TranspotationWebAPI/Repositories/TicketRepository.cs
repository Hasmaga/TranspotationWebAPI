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
    }
}
