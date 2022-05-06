
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relive.Server.Core.Entities.ProfileEntities.HostProfileEntities
{
    public class OfferPackage: BaseEntity
    {
        [Required]
        public string PackageName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ForeignKey("HostProfile")]
        public Guid HostProfileId { get; set; }
    }
}
