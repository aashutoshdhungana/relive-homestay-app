using Relive.Server.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities.UserEntities
{
    public class User: BaseEntity
    {
        [Required]
        [MaxLength(35)]
        public string FullName { get; set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required]
        public UserTypes UserType { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
