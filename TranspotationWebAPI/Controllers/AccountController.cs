using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TranspotationAPI.Enum;
using TranspotationAPI.Repositories;
using TranspotationWebAPI.Model.Dto;

namespace TranspotationAPI.Controllers
{
    // To create AccountAPI for front end to use
    [Route("AccountAPI/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        protected CommonResDto _resonse;
        
        private IAccountRepository _accountRepository;

        private readonly ILogger _logger;              

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            this._resonse = new CommonResDto();
            _logger = logger;            
        }        
        
        // Get user information by Id
        [HttpGet, Authorize(Roles = "1")]
        [Route("GetAccountById/{accountId}")]
        public async Task<ActionResult<GetUserInformationResDto>> GetAccountInformationByIdAsync(int accountId)
        {
            _logger.LogInformation($"Get User's Information By Id: {accountId} by API.");
            try
            {
                GetUserInformationResDto account = await _accountRepository.GetUserInformationByIdAsync(accountId);
                if (account == null)
                {
                    return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
                }
                return Ok(account);
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.GET_ACCOUNT_INFO_FAIL);
            }
        }

        // Get all user information
        [HttpGet, Authorize(Roles = "1")]
        [Route("GetAllUser")]
        public async Task<ActionResult<List<GetAllUserInformationResDto>>> GetAllUserInformationAsync()
        {
            _logger.LogInformation($"Get All User Information API");
            try
            {
                List<GetAllUserInformationResDto> listAcc = await _accountRepository.GetAllUserInformationAsync();
                return Ok(listAcc);
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
            }
        }

        // Add new user
        [HttpPost, AllowAnonymous]
        [Route("AddAccount")]
        public async Task<ActionResult<CommonResDto>> CreateNewAccountAsync([FromBody] CreateAccountResDto newAcc)
        {
            _logger.LogInformation($"Create New Account API");
            try
            {
                await _accountRepository.CreateAccountAsync(newAcc);
                _resonse.Result = newAcc;
                _resonse.DisplayMessage = "Create new user successfully";
                _resonse.IsSuccess = true;
                return Ok(_resonse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
            }
        }
        
               

        //Delete user
        [HttpDelete, Authorize(Roles = "1")]
        [Route("DeleteUserById/{id}")]
        public async Task<ActionResult<CommonResDto>> DeleteAccountByIdAsync(int id)
        {
            _logger.LogInformation($"Delete Account By Id API");
            try
            {
                await _accountRepository.DeleteAccountByIdAsync(id);
                _resonse.Result = id;
                _resonse.IsSuccess = true;
                _resonse.DisplayMessage= "Delete user successfully";                
            }
            catch (Exception ex)
            {
                _resonse.IsSuccess = false;
                _resonse.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return Ok(_resonse);
        }

        //Change user status
        [HttpPatch, Authorize(Roles = "1")]
        [Route("ChangeStatusUserById/{id}")]
        public async Task<ActionResult<CommonResDto>> ChangeUserStatusById(int id)
        {
            _logger.LogInformation($"Change user status API");
            try
            {
                bool result = await _accountRepository.ChangeStatusAccountByIdAsync(id);
                _resonse.Result = result;
                _resonse.DisplayMessage = "Change user status successfully";
            } 
            catch (Exception ex)
            {                
                _resonse.IsSuccess = false;
                _resonse.ErrorMessage
                    = new List<string> { ex.ToString() };            
            }
            return Ok(_resonse);
        }


        [HttpPost, AllowAnonymous]
        [Route("Login")]
        public async Task<ActionResult<CommonResDto>> LoginAsync(string email, string password)
        {
            _logger.LogInformation($"Login API");
            try
            {
                string token = await _accountRepository.LoginAndReturnTokenAsync(email, password);
                _resonse.Result = token;
                _resonse.DisplayMessage = "Login successfully";               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            return Ok(_resonse);
        }

        // Registation User
        [HttpPost, AllowAnonymous]
        [Route("Register")]
        public async Task<ActionResult<CommonResDto>> RegisterAsync([FromBody] RegistrationUserResDto user)
        {
            _logger.LogInformation($"Register API");
            try
            {
                await _accountRepository.RegistrationUserAsync(user);
                _resonse.Result = user;
                _resonse.DisplayMessage = "Register successfully";
                _resonse.IsSuccess = true;               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REGISTRATION_FAILED);
            }
            return Ok(_resonse);
        }

        //Update user
        [HttpPut, Authorize]
        [Route("UpdateUser")]
        public async Task<ActionResult<CommonResDto>> UpdateUserAsync([FromBody] UpdateInfoUserResDto accUpdate)
        {
            _logger.LogInformation($"Update user API");
            try
            {
                UpdateInfoUserResDto acc = await _accountRepository.UpdateUserInfoAsync(accUpdate);
                _resonse.Result = acc;
                _resonse.IsSuccess = true;
                _resonse.DisplayMessage = "Update user successfully";
            }
            catch (Exception ex)
            {
                _resonse.IsSuccess = false;
                _resonse.ErrorMessage
                    = new List<string> { ex.ToString() };
            }
            return Ok(_resonse);
        }

        //Get User Info 
        [HttpGet, Authorize]
        [Route("GetUserInfo")]
        public async Task<ActionResult<GetUserInfoByIdResDto>> GetUserInfoByIdAsync()
        {
            _logger.LogInformation($"Get user by id API");
            try
            {
                GetUserInfoByIdResDto acc = await _accountRepository.GetUserInfoByIdAsync();
                return Ok(acc);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
            }
        }
    }
}
