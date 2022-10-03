using AutoMapper;
using System.Security.Claims;
using TranspotationAPI.DbContexts;
using TranspotationAPI.Enum;
using TranspotationWebAPI.Model;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationWebAPI.Repositories
{
    public class CompanyTripRepository : ICompanyTripRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompanyTripRepository(ApplicationDbContext db, IMapper mapper, ILogger<CompanyTripRepository> logger, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
              


        //public async Task<CreateCompanyTrip> CreateCompanyTripAsync(CreateCompanyTrip createCompanyTrip)
        //{
        //    _logger.LogInformation("Create Company Trip");
        //}       

        public Task<string> GetCompanyByAccount()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GroupSid);
            }
            return Task.FromResult(result);
        }

        public async Task<CreateCompanyTripResDto> CreateCompanyTripByCompnayIdAsync(CreateCompanyTripResDto trip)
        {
            int companyId = int.Parse(await GetCompanyByAccount());
            if (companyId == 0)
            {
                throw new UnauthorizedAccessException(ErrorCode.THIS_ACCOUNT_IS_NOT_AUTH);
            }
            _logger.LogInformation($"Create CompanyTrip with CompanyId: {companyId}");
            CompanyTrip newTrip = new CompanyTrip(trip.TripId, trip.CarId, companyId, true, trip.StartTime, trip.Price, trip.CarTypeId, trip.StationId);
            if (newTrip == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            await _db.CompanyTrip.AddAsync(newTrip);
            await _db.SaveChangesAsync();
            return _mapper.Map<CreateCompanyTripResDto>(newTrip);
        }
    }
}
