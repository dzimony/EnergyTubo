using EnergyTubo.Interface;
using EnergyTubo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnergyTubo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStateService StateService;
        //private readonly ILgaService LGAService;

        public StatesController(IStateService StateService)
        {
            this.StateService = StateService;
            
            
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<State>>> Get()
        {
            try
            {

                return Ok(await StateService.GetStates());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }




        [HttpGet ("[action]")]

        public async Task<ActionResult<State>> GetStateById(int stateId)
        {
            try
            {

                return Ok(await StateService.GetStateById(stateId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<State>> CreateState(State State)
        {
           
            try
            {
                if (State == null)
                    return BadRequest();



                var createdCandidateService = await StateService.AddState(State);

                return CreatedAtAction(nameof(Get),
                    new { id = createdCandidateService.Id }, createdCandidateService);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new Student");
            }
        }




    }
}