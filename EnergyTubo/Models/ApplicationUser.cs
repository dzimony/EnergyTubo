using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EnergyTubo.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }
        public State? State { get; }
        [Required(ErrorMessage = "lga is required.")]
        public int LgaId { get; set; }
        public LGA? LGA { get; }
        [Required(ErrorMessage = "Phone is required.")]
        public string PhoneNumber { get; set; }
    }
}


