using System;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid OwnerId { get; set; }
    }
}
