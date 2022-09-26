﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        private IAccountRepository _accountRepository;

        private readonly ILogger _logger;       

        public AccountController(IAccountRepository accountRepository, ILogger<AccountController> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
            
        }
        // Get user information by Id
        [HttpGet]
        [Route("{accountId}")]
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
        [HttpGet]
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
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> CreateNewUserAsync([FromBody] CreateUpdateUserResDto user)
        {
            _logger.LogInformation($"Create New User API");
            try
            {
                int id = 0;
                await _accountRepository.CreateUpdateUserAsync(user,id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return BadRequest(ErrorCode.ACCOUNT_NOT_FOUND);
            }
            return Ok();
        }

        //Delete user
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeleteAccountByIdAsync(int id)
        {
            try
            {
                await _accountRepository.DeleteAccountByIdAsync(id);                
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }
    }
}
