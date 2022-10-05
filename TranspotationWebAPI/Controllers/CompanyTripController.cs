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
        public async Task<ActionResult<CommonResDto>> CreateCompanyTripAsync([FromBody] CreateUpdateCompanyTripResDto trip)
        {
            _logger.LogInformation("Create Company Trip");
            try
            {
                CreateUpdateCompanyTripResDto newTrip = await _companyTripRepository.CreateCompanyTripByCompnayIdAsync(trip);
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

        //Update Company Trip
        [HttpPut, Authorize]
        [Route("UpdateCompanyTrip")]
        public async Task<ActionResult<CommonResDto>> UpdateCompanyTripAsync([FromBody] CreateUpdateCompanyTripResDto trip, int id)
        {
            _logger.LogInformation("Update Company Trip");
            try
            {
                CreateUpdateCompanyTripResDto newTrip = await _companyTripRepository.UpdateCompanyTripByCompnayIdAsync(trip, id);
                _response.Result = newTrip;
                _response.IsSuccess = true;
                _response.DisplayMessage = "Update Company Trip Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.IsSuccess = false;
                _response.DisplayMessage = "Update Company Trip Fail";
            }
            return Ok(_response);
        }

        //Get All CompanyTrip By CompanyId
        [HttpGet, Authorize]
        [Route("GetCompanyTrip")]
        public async Task<ActionResult<List<ReadCompanyTripResDto>>> GetAllCompanyTripByCompanyIdAsync()
        {
            _logger.LogInformation("Get All Company Trip By CompanyId");
            try
            {
                List<ReadCompanyTripResDto> companyTrip = await _companyTripRepository.GetAllCompanyTripByCompanyIdAsync();
                return Ok(companyTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.GET_COMPANY_TRIP_FAIL);
            }            
        }

        //Change status Company Trip 
        [HttpPut, Authorize]
        [Route("ChangeStatusCompanyTrip")]
        public async Task<ActionResult<CommonResDto>> ChangeStatusCompanyTripByCompanyIdAsync(int id)
        {
            _logger.LogInformation("Change Status Company Trip");
            try
            {
                bool status = await _companyTripRepository.ChangeStatusCompanyTripByCompanyIdAsync(id);
                _response.IsSuccess = status;
                _response.DisplayMessage = "Change Status Company Trip Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.IsSuccess = false;
                _response.DisplayMessage = "Change Status Company Trip Fail";
            }
            return Ok(_response);
        }

        //Delete Company Trip
        [HttpDelete, Authorize]
        [Route("DeleteCompanyTrip")]
        public async Task<ActionResult<CommonResDto>> DeleteCompanyTripByCompanyIdAsync(int id)
        {
            _logger.LogInformation("Delete Company Trip");
            try
            {
                bool status = await _companyTripRepository.DeleteCompanyTripByCompanyIdAsync(id);
                _response.IsSuccess = status;
                _response.DisplayMessage = "Delete Company Trip Success";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                _response.IsSuccess = false;
                _response.DisplayMessage = "Delete Company Trip Fail";
            }
            return Ok(_response);
        }
    }
}
