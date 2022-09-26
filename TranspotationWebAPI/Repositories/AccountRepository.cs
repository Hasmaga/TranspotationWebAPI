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
            try
            {
                Account account = await _db.Account.FindAsync(accountId);
                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }           
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
            var query =  (from pd in _db.Account
                         join pd2 in _db.Role on pd.RoleId equals pd2.Id                         
                         select new GetAllUserInformationResDto
                         {
                             Id = pd.Id,
                             password = pd.Password,
                             phoneNumber = pd.Phone,
                             email = pd.Email,
                             name = pd.Name,
                             status = pd.Status,
                             roleName = pd2.Name,
                             roleAuthority = pd2.Authority                             
                         }).ToListAsync();
            List<GetAllUserInformationResDto> listAcc = await query;
            return _mapper.Map<List<GetAllUserInformationResDto>>(listAcc);
        }

        public async Task<CreateUpdateUserResDto> CreateUpdateUserAsync(CreateUpdateUserResDto user, int accountId)
        {
            Account acc = new Account(user.Phone, user.Email, user.Name, user.Password, user.Status, user.CompanyId, user.roleId);
            acc.CompanyId = 0;
            acc.Status = true;
            acc.RoleId = 3;
            
            if (accountId == 0)
            {
                _logger.LogInformation($"Create new User.");
                _db.Account.Add(acc);
            }
            else
            {
                _logger.LogInformation($"Update User with id: {accountId}");
                Account accUpdate = await this.FindAccountByIdAsync(accountId);
                accUpdate.Name = user.Name;
                accUpdate.Phone = user.Phone;
                accUpdate.Email = user.Email;
                accUpdate.Password = user.Password;
                accUpdate.Status = user.Status;
                accUpdate.CompanyId = user.CompanyId;
                accUpdate.RoleId = user.roleId;
                _db.Account.Update(accUpdate);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<CreateUpdateUserResDto>(acc);
        }

        public async Task DeleteAccountByIdAsync(int accountId)
        {
            _logger.LogInformation($"Delete link by id: {accountId}");
            Account account = await this.FindAccountByIdAsync(accountId);
            if (account != null)
            {
                _db.Account.Remove(account);
            }
            else
            {
                _logger.LogError($"Account not found");
                throw new KeyNotFoundException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            await _db.SaveChangesAsync();
        }
    }
}
