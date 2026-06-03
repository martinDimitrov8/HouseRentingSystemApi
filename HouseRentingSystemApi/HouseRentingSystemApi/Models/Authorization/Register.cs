using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystemApi.Models.Authorization
{
    public class Register
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }

        public string Role { get; set; } = "Client";
    }
}
