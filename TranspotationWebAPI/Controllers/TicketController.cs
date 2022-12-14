using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranspotationAPI.Enum;
using TranspotationWebAPI.Model.Dto;
using TranspotationWebAPI.Repositories;

namespace TranspotationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger _logger;
        
        public TicketController(ITicketRepository ticketRepository, ILogger<TicketController> logger)
        {
            _ticketRepository = ticketRepository;
            _logger = logger;

        }


        [HttpGet, Authorize]
        [Route("GetAllTicketByAccount")]
        public async Task<ActionResult> GetAllTicketByAccount()
        {
            _logger.LogInformation("Get All Ticket By Account");
            try
            {
                List<GetTicketByAccountResDto> tickets = await _ticketRepository.GetAllTicketByAccount();                
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
        }

        [HttpPut, Authorize]
        [Route("UpdateTicketByToken")]
        public async Task<ActionResult> UpdateTicketByTokenAsync([FromBody] UpdateTicketByTokenResDto updateTicket, int id)
        {
            _logger.LogInformation("Update Ticket By Token");
            try
            {
                bool result = await _ticketRepository.UpdateTicketByTokenAsync(updateTicket, id);
                if (result)
                {
                    return Ok();
                }
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
        }

        // Get All Ticket By Account With Status
        [HttpGet, Authorize]
        [Route("GetAllTicketByAccountWithStatus")]
        public async Task<ActionResult> GetAllTicketByAccountWithStatus(bool status)
        {
            _logger.LogInformation("Get All Ticket By Account With Status");
            try
            {
                List<GetAllTicketByAccountWithStatusResDto> tickets = await _ticketRepository.GetAllTicketByAccountWithStatus(status);
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
        }

        // Create Ticket
        [HttpPost, Authorize]
        [Route("CreateTicket")]
        public async Task<ActionResult> CreateTicketAsync([FromBody] CreateTicketByAccountResDto ticket)
        {
            _logger.LogInformation("Create Ticket");
            try
            {
                bool result = await _ticketRepository.CreateTicketByAccount(ticket);
                if (result)
                {
                    return Ok();
                }
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
        }

        //Get Seat Selected By Company Trip Id
        [HttpGet]
        [Route("GetSeatSelectedByCompanyTripId")]
        public async Task<ActionResult> GetSeatSelectedByCompanyTripId(int companyTripId)
        {
            _logger.LogInformation("Get Seat Selected By Company Trip Id");
            try
            {
                List<string> seats = await _ticketRepository.GetSeatSelectedByCompanyTripId(companyTripId);
                return Ok(seats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
        }

        [HttpGet]
        [Route("GetTicketByTicketId")]
        public async Task<ActionResult> GetTicketByTicketId(int id)
        {
            _logger.LogInformation("Get Ticket By Ticket Id");
            try
            {
                GetTicketByTicketId ticket = await _ticketRepository.GetTicketByTicketId(id);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return NotFound(ErrorCode.REPOSITORY_ERROR);
            }
        }
    }
}
