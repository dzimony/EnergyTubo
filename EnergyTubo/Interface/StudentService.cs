using EnergyTubo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnergyTubo.Interface
{

    public class StudentService : IStudentService
    {
        private readonly AppDBContext _dbContext;
        private readonly IStateService StateService;
        private readonly ILgaService LGAService;
        private readonly UserManager<ApplicationUser> _userManager;
        private bool Issucceed = false;
        public StudentService(UserManager<ApplicationUser> userManager, IStateService stateService, ILgaService lgaService, AppDBContext dbContext)
        {
            _userManager = userManager;
            StateService = stateService;
            LGAService = lgaService;
            _dbContext = dbContext;
        }
        public async Task<RegisterDTO> RegisterStudent(Student student)
        {
            RegisterDTO registerDTO = new RegisterDTO();
            //var user = await _userManager.FindByEmailAsync(student.Email);
            var user = new ApplicationUser

            {
                PhoneNumber = student.PhoneNumber,
                Email = student.Email,
                LgaId = student.LgaId,
                StateId = student.StateId,
                UserName = student.Email

            };

            var result = await _userManager.CreateAsync(user, student.Password);
            if (result.Succeeded)
            {
                registerDTO.Email = student.Email;
                registerDTO.IsSucceeded = true;
                return registerDTO;
            }

            return null;


        }

        
    }
}
