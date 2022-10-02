using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
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

        private readonly IConfiguration _configuration;

        public AccountRepository(ApplicationDbContext db, IMapper mapper, ILogger<AccountRepository> logger, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
            _configuration = configuration;
        }        

        // Find Account By Id
        public async Task<Account> FindAccountByIdAsync(int accountId)
        {
            _logger.LogInformation($"Find Account By Id: {accountId}");            
            Account account = await _db.Account.FindAsync(accountId);
            if (account == null)
            {
                _logger.LogInformation($"Account Id: {accountId} not found at {DateTime.UtcNow.ToLongTimeString()}");
                throw new KeyNotFoundException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            return account;
        }


        // Get Account's Info by Id
        public async Task<GetUserInformationResDto> GetUserInformationByIdAsync(int accountId)
        {
            _logger.LogInformation($"Get User Information By Id: {accountId}");
            var query = (from pd in _db.Account
                         join pd2 in _db.Role on pd.RoleId equals pd2.Id
                         where pd2.Id == accountId
                         select new GetUserInformationResDto
                         {
                             Id = pd.Id,                             
                             phoneNumber = pd.Phone,
                             email = pd.Email,
                             name = pd.Name,
                             status = pd.Status,
                             roleName = pd2.Name,
                             roleAuthority = pd2.Authority,
                             passwordHash = pd.PasswordHash,
                             passwordSalt = pd.PasswordSalt
                         }).FirstOrDefaultAsync();
            GetUserInformationResDto acc = await query;
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
                             phoneNumber = pd.Phone,
                             email = pd.Email,
                             name = pd.Name,
                             status = pd.Status,
                             roleName = pd2.Name,
                             roleAuthority = pd2.Authority,
                             passwordHash = pd.PasswordHash,
                             passwordSalt = pd.PasswordSalt
                         }).ToListAsync();
            List<GetAllUserInformationResDto> listAcc = await query;
            return _mapper.Map<List<GetAllUserInformationResDto>>(listAcc);
        }

        // Create and Update Account
        public async Task<CreateUpdateUserResDto> CreateUpdateUserAsync(CreateUpdateUserResDto user, int accountId, string Password)
        {
            Account acc = new Account(user.Name, user.Phone, user.Email, user.Status, user.CompanyId, user.roleId, user.PasswordHash, user.PasswordSalt);
            acc.CompanyId = 0;
            acc.Status = true;
            acc.RoleId = 3;
            
            if (accountId == 0)
            {
                _logger.LogInformation($"Create new User.");

                CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
                acc.PasswordHash = Convert.ToBase64String(passwordHash);
                acc.PasswordSalt = Convert.ToBase64String(passwordSalt);
                              
                await _db.Account.AddAsync(acc);                
            }
            else
            {
                _logger.LogInformation($"Update User with id: {accountId}");
                Account accUpdate = await this.FindAccountByIdAsync(accountId);
                accUpdate.Name = user.Name;
                accUpdate.Phone = user.Phone;
                accUpdate.Email = user.Email;                                
                _db.Account.Update(accUpdate);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<CreateUpdateUserResDto>(acc);
        }
        
        //Delete Account
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

        public async Task<bool> ChangeStatusAccountByIdAsync(int accountId)
        {
            _logger.LogInformation($"Change status of account with id: {accountId}");
            Account account = await this.FindAccountByIdAsync(accountId);
            if (account != null)
            {
                if (account.Status == true)
                {
                    account.Status = false;
                }
                else
                {
                    account.Status = true;
                }
                _db.Account.Update(account);
            }
            else
            {
                _logger.LogError($"Account not found");
                throw new KeyNotFoundException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            await _db.SaveChangesAsync();
            return true;
        }

        private void CreatePasswordHash(String password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(String password, string passwordHash, string passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(passwordSalt)))
            {
                var computedHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) throw new UnauthorizedAccessException(ErrorCode.PASSWORD_INCORRECT);
                }
                return true;
            }
        }

        public async Task<string> LoginAndReturnTokenAsync(string email, string passwordHash)
        {
            _logger.LogInformation($"Login with email: {email}");
            Account acc = await _db.Account.FirstOrDefaultAsync(x => x.Email == email);            
            if (acc == null)
            {
                _logger.LogError($"Account not found");
                throw new KeyNotFoundException(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            if (acc.Status == false)
            {
                _logger.LogError($"Account is blocked");
                throw new KeyNotFoundException(ErrorCode.ACCOUNT_BLOCKED);
            }
            if (!VerifyPasswordHash(passwordHash, acc.PasswordHash, acc.PasswordSalt))
            {
                _logger.LogError($"Password incorrect");
                throw new UnauthorizedAccessException(ErrorCode.PASSWORD_INCORRECT);
            }
            String token = GenerateToken(acc);
            return token;
        }

        private string GenerateToken(Account acc)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, acc.Name),
                new Claim(ClaimTypes.Email, acc.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
