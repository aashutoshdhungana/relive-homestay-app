using Relive.Server.Core.Entities.ProfileEntities.HostProfileEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Relive.Server.Core.Entities.ProfileEntities
{
    public class HostProfile: BaseEntity
    {
        [Required]
        public string HomeStayName { get; set; }
        [Required]
        public decimal Latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        [Required]
        public string Address { get; set; }
        public string DisplayPicture { get; set; }
        [Required]
        public string About { get; set; }
        public List<HostCategory> Categories { get; set; }
        public List<MenuItem> Menu { get; set; }
        public List<Room> Rooms { get; set; }
        public List<OfferPackage> OfferPackages { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
    }
}
