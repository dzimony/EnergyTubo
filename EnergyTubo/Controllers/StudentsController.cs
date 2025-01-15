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

        

       
        private readonly IStudentService StudentService;

        public const string MyEmail = "_Email";
        public const string OtpText = "_Otp";


        public StudentsController(UserManager<ApplicationUser> userManager,
            IStudentService studentservice, IStateService stateService, ILgaService lgaService)
        {
            _userManager = userManager;
            
            StudentService = studentservice;

            
        }


        [HttpPost("[Action]")]
        public async Task<IActionResult> RegisterStudent([FromBody] Student student)
        {
            if (student == null || !ModelState.IsValid)
                return BadRequest(new UserResponse { Message = "Bad Request" });
            try
            {


                var user = await _userManager.FindByEmailAsync(student.Email);
                if (user != null && user.EmailConfirmed)
                {
                    return Unauthorized(new UserResponse { Message = "Email is in use" });
                }
                else if (user != null && !user.EmailConfirmed)
                {

                    return BadRequest(new UserResponse { Message = "Already registered, please confirmed your email" });

                }

                var result = await StudentService.RegisterStudent(student);
               

                if (!result.IsSucceeded)
                {
                    return Unauthorized(new UserResponse { Message = "Error, creating the user" });
                }

              var otp =  CreateSession(result.Email);



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

            try
            {
                


                    var students = StudentService.GetStudent();

               

                var studentL = await StudentService.GetStudent();
                return Ok(studentL);

                
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
        public async Task<ActionResult> VerifyPhone(string Code)
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

