using Relive.Server.Core.UserAggregate;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relive.Server.Core.Entities.ProfileAggregate
{
    public class Profile: BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
