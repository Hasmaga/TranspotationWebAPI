using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

        public Task<string> GetCompanyByAccount()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GroupSid);
            }
            return Task.FromResult(result);
        }

        public async Task<CreateUpdateCompanyTripResDto> CreateCompanyTripByCompnayIdAsync(CreateUpdateCompanyTripResDto trip)
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
            return _mapper.Map<CreateUpdateCompanyTripResDto>(newTrip);
        }

        public async Task<CreateUpdateCompanyTripResDto> UpdateCompanyTripByCompnayIdAsync(CreateUpdateCompanyTripResDto trip, int companyTripId)
        {
            int companyId = int.Parse(await GetCompanyByAccount());
            if (companyId == 0)
            {
                throw new UnauthorizedAccessException(ErrorCode.THIS_ACCOUNT_IS_NOT_AUTH);
            }
            _logger.LogInformation($"Update CompanyTrip with CompanyId: {companyId}");
            CompanyTrip updateTrip = await _db.CompanyTrip.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.Id == companyTripId);
            if (companyId != updateTrip.CompanyId)
            {
                throw new UnauthorizedAccessException(ErrorCode.THIS_ACCOUNT_IS_NOT_AUTH);
            }
            if (updateTrip == null)
            {
                throw new Exception(ErrorCode.COMPANY_TRIP_NOT_FOUND);
            }
            updateTrip.TripId = trip.TripId;
            updateTrip.CarId = trip.CarId;
            updateTrip.StartTime = trip.StartTime;
            updateTrip.Price = trip.Price;
            updateTrip.CarTypeId = trip.CarTypeId;
            updateTrip.StationId = trip.StationId;
            await _db.SaveChangesAsync();
            return _mapper.Map<CreateUpdateCompanyTripResDto>(updateTrip);
        }

        public async Task<List<ReadCompanyTripResDto>> GetAllCompanyTripByCompanyIdAsync()
        {
            int CompanyId = int.Parse(await GetCompanyByAccount());
            if (CompanyId == 0)
            {
                throw new UnauthorizedAccessException(ErrorCode.THIS_ACCOUNT_IS_NOT_AUTH);
            }
            _logger.LogInformation($"Get All CompanyTrip with CompanyId: {CompanyId}");
            List<CompanyTrip> companyTrips = await _db.CompanyTrip.Where(x => x.CompanyId == CompanyId).ToListAsync();
            if (companyTrips == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            return _mapper.Map<List<ReadCompanyTripResDto>>(companyTrips);
        }

        public async Task<bool> ChangeStatusCompanyTripByCompanyIdAsync(int id)
        {
            int CompanyId = int.Parse(await GetCompanyByAccount());
            if (CompanyId == 0)
            {
                throw new UnauthorizedAccessException(ErrorCode.THIS_ACCOUNT_IS_NOT_AUTH);
            }
            _logger.LogInformation($"Change Status CompanyTrip with CompanyId: {CompanyId}");
            CompanyTrip companyTrip = await _db.CompanyTrip.Where(x => x.CompanyId == CompanyId && x.Id == id).FirstOrDefaultAsync();
            if (companyTrip == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            if (companyTrip.Status == true)
            {
                companyTrip.Status = false;
            }
            else
            {
                companyTrip.Status = true;
            }
            _db.CompanyTrip.Update(companyTrip);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCompanyTripByCompanyIdAsync(int id)
        {
            int CompanyId = int.Parse(await GetCompanyByAccount());
            if (CompanyId == 0)
            {
                throw new UnauthorizedAccessException(ErrorCode.THIS_ACCOUNT_IS_NOT_AUTH);
            }
            _logger.LogInformation($"Delete CompanyTrip with CompanyId: {CompanyId}");
            CompanyTrip companyTrip = await _db.CompanyTrip.Where(x => x.CompanyId == CompanyId && x.Id == id).FirstOrDefaultAsync();
            if (companyTrip == null)
            {
                throw new Exception(ErrorCode.REPOSITORY_ERROR);
            }
            _db.CompanyTrip.Remove(companyTrip);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
