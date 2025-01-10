using System.ComponentModel.DataAnnotations;

namespace EnergyTubo.Models
{
    public class State
    {
        public int Id { get; set; }

        [Required(ErrorMessage =" Name is required.")]
        public string Name { get; set; }
        public IList<LGA> LGA { get; } = new List<LGA>();
        public IList<Student> Student { get; } = new List<Student>();
    }
}
