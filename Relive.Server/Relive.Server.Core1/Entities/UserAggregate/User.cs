using Relive.Server.Core.Entities;
using Relive.Server.Core.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        [Required]
        [MaxLength(35)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(35)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(10)]
        [MinLength(10)]
        public string Phone { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public User() 
        {
        }

        public User(Guid id, string firstName, string lastName, string email,
            string phone, string password)
        {
            Id = id;
            FirstName = firstName.ToUpper();
            LastName = lastName.ToUpper();
            Email = email.ToUpper();
            Phone = phone;
            Password = password;
        }
    }
}
