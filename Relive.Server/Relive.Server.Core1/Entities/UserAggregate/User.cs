using Relive.Server.Core.Entities;
using Relive.Server.Core.Intefaces;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.UserAggregate
{
    public class User: BaseEntity, IAggregateRoot
    {
        [Required]
        [MaxLength(35)]
        public string FirstName { get; set; }
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

        public bool HasHostProfile { get; private set; }

        public bool HasTravellerProfile { get; private set; }

        public bool IsAdmin { get; private set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User() 
        {
        }

        public User(string firstName, string lastName, string email,
            string phone, bool isTraveller, bool isHost, bool isAdmin, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            if (isTraveller)
            {
                HasHostProfile = false;
                HasTravellerProfile = true;
                IsAdmin = false;
            }
            else if (isHost)
            {
                HasHostProfile = false;
                HasTravellerProfile = true;
                IsAdmin = false;
            }
            else if (isAdmin)
            {
                HasHostProfile = false;
                HasTravellerProfile = false;
                IsAdmin = true;
            }
            Password = password;
        }

        public void AddHostProfile()
        {
            if (IsAdmin) return;
            HasHostProfile = true;
        }

        public void AddTravellerProfile()
        {
            if (IsAdmin) return;
            HasTravellerProfile = true;
        }
    }
}
