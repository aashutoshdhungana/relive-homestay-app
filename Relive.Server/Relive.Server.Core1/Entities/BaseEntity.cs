using System;

namespace Relive.Server.Core.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
