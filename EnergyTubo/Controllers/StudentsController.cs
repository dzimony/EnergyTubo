using EnergyTubo.Interface;
using EnergyTubo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http;

namespace EnergyTubo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        

        private readonly IStateService StateService;
        private readonly ILgaService LGAService;

        public const string MyEmail = "_Email";
        public const string OtpText = "_Otp";


        public StudentsController(UserManager<ApplicationUser> userManager, IStateService StateService, 
            ILgaService lgaService)
        {
            this._userManager = userManager;
            this.StateService = StateService;
            this.LGAService = lgaService;
            
        }


        [HttpPost("[Action]")]
        public async Task<IActionResult> RegisterStudent([FromBody] Student userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest(new UserResponse { Message = "Bad Request" });
            try
            {


                var user = await _userManager.FindByEmailAsync(userForRegistration.Email);
                if (user != null && user.EmailConfirmed)
                {
                    return Unauthorized(new UserResponse { Message = "Email is in use" });
                }
                else if (user != null && !user.EmailConfirmed)
                {

                    return BadRequest(new UserResponse { Message = "Already registered, please confirmed your email" });

                }

                user = new ApplicationUser
                {
                    PhoneNumber = userForRegistration.PhoneNumber,
                    Email = userForRegistration.Email,
                    LgaId = userForRegistration.LgaId,
                    StateId = userForRegistration.StateId,
                    UserName = userForRegistration.Email

                };

                var result = await _userManager.CreateAsync(user, userForRegistration.Password);
                if (!result.Succeeded)
                {
                    return Unauthorized(new UserResponse { Message = "Error, creating the user" });
                }

              var otp =  CreateSession(userForRegistration.Email);



                return Ok(new UserResponse { Message = $"You need to verify your Phone, Copy {otp}, click on verify link " });
            }



            catch (Exception ex)
            {


                return BadRequest("Error, please try again");
                throw ex;
            }
        }

        [HttpGet("[action]")]
            public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            List<StudentDTO> studentList = new List<StudentDTO>();

            try
            {
                foreach (var user in _userManager.Users.ToList())
                {
                    var state = await StateService.GetStateById(user.StateId);
                    var lga = await LGAService.GetLgaById(user.LgaId);
                    StudentDTO StudentDTO = new StudentDTO()
                    {

                        Phone = user.PhoneNumber,
                        Email = user.Email,
                        State = state.Name,
                        Lga = lga.Name
                    };

                    studentList.Add(StudentDTO);


                }
                return Ok(studentList);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, please check your input");
            }
        }


        private string CreateSession(string Email)
        {


          Random _random = new Random();


           string otp = _random.Next(0, 9999).ToString("D4");

            HttpContext.Session.SetString(MyEmail, Email);
            HttpContext.Session.SetString(OtpText, otp);
            return otp;
        }



        [HttpPost("[action]")]
        public async Task<ActionResult<State>> VerifyPhone(string Code)
        {
            string otp = HttpContext.Session.GetString(OtpText);
            string email = HttpContext.Session.GetString(MyEmail);
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return BadRequest("Not Found");

                }

                if (Code == otp)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
                else
                {
                    return BadRequest("The Code is incorrect");
                }

                

                return Ok(new { message = "Successfully Verified" });

            }
            
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error, please check your input");
            }
        }

    }
}

