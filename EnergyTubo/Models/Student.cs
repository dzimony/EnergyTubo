using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnergyTubo.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]

        [EmailAddress(ErrorMessage = "Email address is invalid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "State is required.")]
        //[ForeignKey("StateId")]
        public int StateId { get; set; }
       // public State? State { get; }
        [Required(ErrorMessage = "LGA is required.")]
        //[ForeignKey("LgaId")]
        public int LgaId { get; set; }
        //public LGA? LGA { get; }
        public IList<ApplicationUser> ApplicationUser { get; } = new List<ApplicationUser>();


    }
}
