﻿using Microsoft.AspNetCore.Authorization;
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
    }
}