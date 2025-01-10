using EnergyTubo.Interface;
using EnergyTubo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EnergyTubo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        
        private readonly HttpClient httpClient;
        private readonly IBankService bankService;
        public BanksController(HttpClient HttpClient, IBankService BankService)
        {
            this.httpClient = HttpClient;
            this.bankService = BankService;
        }

        [HttpGet]
        public async Task<ActionResult<Bank>> GetBanks(string key = null)
        {
           
            Bank bank = new Bank();
            try
            {

            

            
            bank = (await bankService.GetBanks(key));
            

            if (bank != null && bank.HasError)
            {
                return BadRequest(bank.ErrorMessage);
            }

             return Ok(bank.Result);
                  
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message
                    );
            }
        }
    }
}
