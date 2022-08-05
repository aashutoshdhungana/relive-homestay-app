using System;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}
