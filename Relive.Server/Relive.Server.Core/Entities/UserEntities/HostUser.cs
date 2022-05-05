using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities.UserEntities
{
    public class HostUser: User
    {
        [Required]
        public string HomestayName { get; set; }
    }
}
