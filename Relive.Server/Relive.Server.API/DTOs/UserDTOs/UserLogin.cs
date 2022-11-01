using Relive.Server.Core.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.API.DTOs.UserDTOs
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
