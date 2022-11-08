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

        //Get Company Trip 
        [HttpGet]
        [Route("GetAllCompanyTrip")]
        public async Task<ActionResult<List<GetAllCompanyTripResDto>>> GetAllCompanyTripAsync()
        {
            _logger.LogInformation("Get All Company Trip");
            try
            {
                List<GetAllCompanyTripResDto> companyTrip = await _companyTripRepository.GetAllCompanyTripAsync();
                return Ok(companyTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.GET_COMPANY_TRIP_FAIL);
            }
        }

        //Get Company Trip By Trip Id
        [HttpGet]
        [Route("GetCompanyTripByTripId")]
        public async Task<ActionResult<GetCompanyTripByTripIdResDto>> GetCompanyTripByTripIdAsync(int id)
        {
            _logger.LogInformation("Get Company Trip By Trip Id");
            try
            {
                List<GetCompanyTripByTripIdResDto> companyTrip = await _companyTripRepository.GetCompanyTripByTripIdAsync(id);
                return Ok(companyTrip);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.GET_COMPANY_TRIP_FAIL);
            }
        }

        //Get Company Trip By Company Id
        [HttpGet]
        [Route("GetTotalSeatByCompanyId")]
        public async Task<int> GetTotalSeatByCompanyIdAsync(int id)
        {
            _logger.LogInformation("Get Total Seat By Company Id");
            int totalSeat = await _companyTripRepository.GetTotalSeatByCompanyIdAsync(id);
            if (totalSeat == 0)
            {
                throw new Exception("Get Total Seat By Company Id Fail");
            }
            return totalSeat;
        }

        [HttpGet]
        [Route("GetCompanyTripByLoFromAndLoTo")]
        public async Task<List<GetAllCompanyTripResDto>> GetCompanyTripByLoFromAndLoToAsync(string loFrom, string loTo)
        {            
            _logger.LogInformation("Get Company Trip By Lo From And Lo To");
            List<GetAllCompanyTripResDto> companyTrip = await _companyTripRepository.GetCompanyTripFromLocationNameFromAndToAsync(loFrom, loTo);
            if (companyTrip == null)
            {
                throw new Exception("Get Company Trip By Lo From And Lo To Fail");
            }
            return companyTrip;            
        }

        [HttpGet]
        [Route("GetSeatListByCompanyId")]
        public async Task<List<string>> GetSeatListByCompanyIdAsync(int id)
        {
            _logger.LogInformation("Get Seat List By Company Id");
            List<string> seatList = await _companyTripRepository.CreateSeatListAsync(id);
            if (seatList == null)
            {
                throw new Exception("Get Seat List By Company Id Fail");
            }
            return seatList;
        }

        [HttpGet]
        [Route("GetLocationName")]        
        public async Task<List<string>> GetLocationNameAsync()
        {
            _logger.LogInformation("Get Location Name");
            List<string> locationName = await _companyTripRepository.GetLocationAsync();
            if (locationName == null)
            {
                throw new Exception("Get Location Name Fail");
            }
            return locationName;
        }
    }
}
