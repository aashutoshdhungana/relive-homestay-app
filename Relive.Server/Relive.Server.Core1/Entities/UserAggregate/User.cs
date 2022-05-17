using Relive.Server.Core.Entities;
using Relive.Server.Core.Intefaces;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.UserAggregate
{
    public class User: BaseEntity, IAggregateRoot
    {
        [Required]
        [MaxLength(35)]
        public string FirstName { get; private set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; private set; }
        [Required]
        [RegularExpression("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; private set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10)]
        public string Phone { get; private set; }

        [Required]
        public UserTypes UserType { get; private set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; private set; }

        public User() 
        {
        }

        public User(string firstName, string lastName, string email,
            string phone, UserTypes usertype, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            UserType = usertype;
            Password = password;
        }
    }
}
