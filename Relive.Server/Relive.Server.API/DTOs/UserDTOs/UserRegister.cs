using Relive.Server.Core.UserAggregate;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.API.DTOs.UserDTOs
{
    public class UserRegister: User
    {
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords donot match")]
        public string ConfirmPassword { get; set; }
    }
}
