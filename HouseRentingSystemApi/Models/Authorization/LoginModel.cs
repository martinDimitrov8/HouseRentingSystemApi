using System.ComponentModel.DataAnnotations;

namespace HouseRentingSystemApi.Models.Authorization
{
    public class LoginModel
    {
        [Required]
        [StringLength(20,MinimumLength =3)]
        public required  string Username { get; set; }

        [Required]
        [StringLength(50,MinimumLength = 6)]
        public required string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        

       
    }
}
