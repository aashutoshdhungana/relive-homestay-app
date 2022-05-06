using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities
{
    public class BaseEntity
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [DefaultValue(false)]
        public bool IsDeleted { get; private set; }
        [DefaultValue(false)]
        public bool IsVerified { get; set; }
    }
}
