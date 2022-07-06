using System.ComponentModel.DataAnnotations;

namespace Relive.Server.API.DTOs.UserDTOs
{
    public class UserUpdate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
