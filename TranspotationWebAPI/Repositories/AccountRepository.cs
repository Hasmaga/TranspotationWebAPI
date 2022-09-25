using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TranspotationAPI.DbContexts;
using TranspotationAPI.Enum;
using TranspotationWebAPI.Model;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationAPI.Repositories
{
    //For controller to use
    public class AccountRepository : IAccountRepository
    {
        // Add ApplicationDbContext
        private readonly ApplicationDbContext _db;
        
        // Add Automapper to map object to dto and vice versa
        private IMapper _mapper;
        
        // Add Logger to log every event to the console for tracking purpose
        private readonly ILogger _logger;

        public AccountRepository(ApplicationDbContext db, IMapper mapper, ILogger<AccountRepository> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }   

        // Find Account By Id
        public async Task<Account> FindAccountByIdAsync(int accountId)
        {
            _logger.LogInformation($"Find Account By Id: {accountId}");

            var account = await _db.Account.FindAsync(accountId);
            if (account == null)
            {
                _logger.LogError($"Account not found with id:{accountId} at {DateTime.UtcNow.ToLongTimeString}");
                throw new KeyNotFoundException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            return account;
        }


        // Get Account's Info by Id
        public async Task<GetUserInformationResDto> GetUserInformationByIdAsync(int accountId)
        {
            _logger.LogInformation($"Get User Information By Id: {accountId}");
            Account acc = await this.FindAccountByIdAsync(accountId);           
            return _mapper.Map<GetUserInformationResDto>(acc);         
        }
        // Get All Account's Info
        public async Task<List<GetAllUserInformationResDto>> GetAllUserInformationAsync()
        {
            _logger.LogInformation($"Get all User.");
            List<Account> listAcc = await _db.Account.ToListAsync();
            return _mapper.Map<List<GetAllUserInformationResDto>>(listAcc);
        }
    }
}
