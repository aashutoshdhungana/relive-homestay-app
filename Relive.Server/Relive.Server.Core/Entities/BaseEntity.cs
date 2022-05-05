using System;

namespace Relive.Server.Core.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; private set; }
    }
}
