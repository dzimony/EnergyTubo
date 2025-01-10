using EnergyTubo.Interface;
using EnergyTubo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergyTubo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LgasController : ControllerBase
    {
        private readonly ILgaService LGAService;

        public LgasController(ILgaService LGAService)
        {
            this.LGAService = LGAService;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<LGA>>> GetLGA(int stateId)
        {
            try
            {

                return Ok(await LGAService.GetLGAs( stateId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }





        [HttpPost]
        public async Task<ActionResult<LGA>> CreateLGA(LGA LGA)
        {
            
            try
            {
                if (LGA == null)
                    return BadRequest();


                var createdCandidateService = await LGAService.AddLGA(LGA);

                return CreatedAtAction(nameof(GetLGA),
                    new { id = createdCandidateService.Id }, createdCandidateService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new LGA");
            }
        }




    }
}