using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyTubo.Models
{
    // LGA Stands for local government area
    public class LGA
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required]
        //[ForeignKey("StateId")]
        public int StateId { get; set; }
        public State? State { get;}

        public IList<Student> Student { get; } = new List<Student>();
        public IList<ApplicationUser> ApplicationUser { get; } = new List<ApplicationUser>();
    }


}

