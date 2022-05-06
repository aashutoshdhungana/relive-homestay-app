using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relive.Server.Core.Entities.ProfileEntities.HostProfileEntities
{
    public class MenuItem: BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Icon { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("HostProfile")]
        public Guid HostProfileId { get; set; }
    }
}
