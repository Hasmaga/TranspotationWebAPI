using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using System.Security.Permissions;
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
            CompanyTrip newTrip = new CompanyTrip(trip.TripId, trip.CarId, companyId, true, trip.StartDateTime,  trip.Price, trip.CarTypeId, trip.StationId, trip.TotalSeat);
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
            updateTrip.StartDateTime = trip.StartDateTime;
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

        public async Task<List<GetAllCompanyTripResDto>> GetAllCompanyTripAsync()
        {
            _logger.LogInformation($"Get All CompanyTrip");
            var query = (from companyTrip in _db.CompanyTrip
                        join trip in _db.Trip on companyTrip.TripId equals trip.Id
                        join car in _db.Car on companyTrip.CarId equals car.Id
                        join carType in _db.CarType on companyTrip.CarTypeId equals carType.Id
                        join station in _db.Station on companyTrip.StationId equals station.Id
                        join company in _db.Company on companyTrip.CompanyId equals company.Id
                        select new GetAllCompanyTripResDto
                        {
                            Id = companyTrip.Id,
                            LocationFrom = trip.From.Name,
                            LoactionTo = trip.To.Name,
                            CarName = car.Name,
                            CompanyName = company.Name,
                            StartDateTime = companyTrip.StartDateTime,
                            Price = companyTrip.Price,
                            CarTypeName = carType.Name,
                            StationName = station.Name
                        }).ToListAsync();
            List<GetAllCompanyTripResDto> list = await query;
            if (list == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            return _mapper.Map<List<GetAllCompanyTripResDto>>(list);
        }

        public async Task<List<GetCompanyTripByTripIdResDto>> GetCompanyTripByTripIdAsync(int id)
        {
            _logger.LogInformation($"Get CompanyTrip with TripId: {id}");
            var query = (from companyTrip in _db.CompanyTrip
                         join trip in _db.Trip on companyTrip.TripId equals trip.Id
                         join car in _db.Car on companyTrip.CarId equals car.Id
                         join carType in _db.CarType on companyTrip.CarTypeId equals carType.Id
                         join station in _db.Station on companyTrip.StationId equals station.Id
                         join company in _db.Company on companyTrip.CompanyId equals company.Id
                         where companyTrip.TripId == id
                         select new GetCompanyTripByTripIdResDto
                         {
                             CompanyName = company.Name,
                             StartDateTime = companyTrip.StartDateTime,
                             Price = companyTrip.Price,
                             CarTypeName = carType.Name,
                             StationName = station.Name
                         }).ToListAsync();
            List<GetCompanyTripByTripIdResDto> list = await query;
            if (list == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            return _mapper.Map<List<GetCompanyTripByTripIdResDto>>(list);
        }

        public async Task<CompanyTrip>FindCompanyTripByIdAsync(int id)
        {
            _logger.LogInformation($"Find CompanyTrip with Id: {id}");
            CompanyTrip trip = await _db.CompanyTrip.FindAsync(id);
            if (trip == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            return trip;
        }   
        
        public async Task<int> GetTotalSeatByCompanyIdAsync(int id)
        {
            _logger.LogInformation($"Get Total Seat with CompanyId: {id}");
            CompanyTrip companyTrip = await this.FindCompanyTripByIdAsync(id);
            if (companyTrip == null)
            {
                throw new Exception(ErrorCode.NOT_FOUND);
            }
            int totalSeat = (int)companyTrip.TotalSeat;
            return totalSeat;
        }

        public async Task<List<GetAllCompanyTripResDto>> GetCompanyTripFromLocationNameFromAndToAsync(string loFrom, string loTo)
        {
            _logger.LogInformation($"");
            var query = from r in _db.CompanyTrip
                        join t in _db.Trip on r.TripId equals t.Id
                        join f in _db.Location on t.From_Id equals f.Id
                        join l in _db.Location on t.To_Id equals l.Id
                        where f.Name == loFrom && l.Name == loTo
                        select new GetAllCompanyTripResDto
                        {
                            Id = r.Id,
                            LocationFrom = f.Name,
                            LoactionTo = l.Name,
                            StartDateTime = r.StartDateTime,
                            Price = r.Price,
                            CarTypeName = r.CarType.Name,
                            StationName = r.Station.Name,
                            CompanyName = r.Company.Name
                        };       
            var list = await query.ToListAsync();
            return list;
        }

        public async Task<List<string>> CreateSeatListAsync(int companyTripId)
        {
            if (companyTripId == 0)
            {
                throw new Exception(ErrorCode.COMPANY_TRIP_NOT_FOUND);
            }
            CompanyTrip companyTrip = await this.FindCompanyTripByIdAsync(companyTripId);
            _logger.LogInformation($"Create Seat List");
            List<string> list = new List<string>();
            for (int i = 1; i <= (companyTrip.TotalSeat) / 2; i++)            
                list.Add("A" + i.ToString());            
            for (int i = 1; i <= (companyTrip.TotalSeat) / 2; i++)           
                list.Add("B" + i.ToString());            
            return list;
        }

        public async Task<List<string>> GetLocationAsync()
        {            
            _logger.LogInformation($"Get Location");
            var query = from r in _db.Location
                      select r.Name;
            var list = await query.ToListAsync();
            return list;            
        }
        
    }
}
