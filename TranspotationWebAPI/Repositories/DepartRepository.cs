using AutoMapper;
using TranspotationAPI.DbContexts;
using TranspotationAPI.Repositories;

namespace TranspotationWebAPI.Repositories
{
    public class DepartRepository : IDepartRepository 
    {
        // Add ApplicationDbContext
        private readonly ApplicationDbContext _db;

        // Add Automapper to map object to dto and vice versa
        private IMapper _mapper;

        // Add Logger to log every event to the console for tracking purpose
        private readonly ILogger _logger;

        public DepartRepository(ApplicationDbContext db, IMapper mapper, ILogger<AccountRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }
    }
}
