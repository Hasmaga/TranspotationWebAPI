using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TranspotationAPI.Enum;
using TranspotationWebAPI.Model.Dto;
using TranspotationWebAPI.Repositories;

namespace TranspotationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyTripController : ControllerBase
    {
        protected CommonResDto _response;
        private ICompanyTripRepository _companyTripRepository;
        private readonly ILogger _logger;

        public CompanyTripController(ICompanyTripRepository companyTripRepository, ILogger<CompanyTripController> logger)
        {
            _companyTripRepository = companyTripRepository;
            this._response = new CommonResDto();
            _logger = logger;
        }

        //Get Account Role 
        [HttpGet, Authorize]
        [Route("GetRoleAccount")]
        public async Task<ActionResult> GetRoleAccount()
        {
            _logger.LogInformation("Get Role Account");
            try
            {
                string role = await _companyTripRepository.GetCompanyByAccount();
                if (role == null)
                {                    
                    return NotFound(ErrorCode.GET_ROLE_ACCOUNT_FAIL);
                }
                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.GET_ROLE_ACCOUNT_FAIL);
            }
        }

        //Create Company Trip
        [HttpPost, Authorize]
        [Route("CreateCompanyTrip")]
        public async Task<ActionResult<CommonResDto>> CreateCompanyTripAsync([FromBody] CreateCompanyTripResDto trip)
        {
            _logger.LogInformation("Create Company Trip");
            try
            {
                CreateCompanyTripResDto newTrip = await _companyTripRepository.CreateCompanyTripByCompnayIdAsync(trip);
                _response.Result = newTrip;
                _response.IsSuccess = true;
                _response.DisplayMessage = "Create Company Trip Success";
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());                
                _response.IsSuccess = false;
                _response.DisplayMessage = "Create Company Trip Fail";
            }
            return Ok(_response);
        }
    }
}
