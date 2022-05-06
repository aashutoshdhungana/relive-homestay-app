
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities.ProfileEntities.HostProfileEntities
{
    public class HostCategory: BaseEntity
    {
        [Required]
        public string Title { get; set; }
    }
}
