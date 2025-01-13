using EnergyTubo.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnergyTubo.Interface
{
    public interface IStudentService
    {
        Task<RegisterDTO> RegisterStudent(Student student);
    }
}

