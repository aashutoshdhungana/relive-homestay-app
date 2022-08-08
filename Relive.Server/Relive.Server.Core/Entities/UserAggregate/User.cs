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

        [MaxLength(2, ErrorMessage = "Middle Name can be atmost 2 characters long")]
        public string MiddleName { get; set; }

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
            string phone, string password, DateTime createdOn, string createdBy)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email.ToLower();
            Phone = phone;
            Password = password;
            CreatedBy = createdBy;
            CreatedOn = createdOn;
        }
    }
}
