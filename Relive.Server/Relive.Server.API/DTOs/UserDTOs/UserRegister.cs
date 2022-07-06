using System.ComponentModel.DataAnnotations;

namespace Relive.Server.API.DTOs.UserDTOs
{
    public class UserRegister
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
