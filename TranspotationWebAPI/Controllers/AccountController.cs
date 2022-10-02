using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.OleDb;
using System.Security.Principal;
using TranspotationAPI.DbContexts;
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
        [HttpGet]
        [Route("GetUserById/{accountId}")]
        public async Task<ActionResult<GetUserInformationResDto>> GetUserInformationByIdAsync(int accountId)
        {
            _logger.LogInformation($"Get User's Information By Id: {accountId} by API.");
            try
            {
                GetUserInformationResDto account = await _accountRepository.GetUserInformationByIdAsync(accountId);
                return Ok(account);
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
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
        [Route("AddUser")]
        public async Task<ActionResult<CommonResDto>> CreateNewUserAsync([FromBody] CreateUpdateUserResDto user, string password)
        {
            _logger.LogInformation($"Create New User API");
            try
            {
                var accountId = 0;
                await _accountRepository.CreateUpdateUserAsync(user,accountId, password);
                _resonse.DisplayMessage = "Create new user successfully";
                return Ok(_resonse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.ACCOUNT_NOT_FOUND);
            }
        }
        

        //Update user
        [HttpPut]
        [Route("UpdateUserById/{id}")]
        public async Task<ActionResult<CommonResDto>> UpdateUserByIdAsync(int id, [FromBody] CreateUpdateUserResDto createUpdateUserResDto)
        {
            _logger.LogInformation($"Update user API");
            try
            {
                
                CreateUpdateUserResDto acc = await _accountRepository.CreateUpdateUserAsync(createUpdateUserResDto, id, null);
                _resonse.Result = acc;
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

        //Delete user
        [HttpDelete]
        [Route("DeleteUserById/{id}")]
        public async Task<ActionResult<CommonResDto>> DeleteAccountByIdAsync(int id)
        {
            _logger.LogInformation($"Delete Account By Id API");
            try
            {
                await _accountRepository.DeleteAccountByIdAsync(id);
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
        [HttpPatch]
        [Route("ChangeStatusUserById/{id}")]
        public async Task<ActionResult<CommonResDto>> ChangeUserStatusById(int id)
        {
            _logger.LogInformation($"Change user status API");
            try
            {
                bool result = await _accountRepository.ChangeStatusAccountByIdAsync(id);
                _resonse.Result = result;
                _resonse.DisplayMessage = "Change user status successfully";
            } catch (Exception ex)
            {                
                _resonse.IsSuccess = false;
                _resonse.ErrorMessage
                    = new List<string> { ex.ToString() };            
            }
            return Ok(_resonse);
        }


        [HttpPost]
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
    }
}
