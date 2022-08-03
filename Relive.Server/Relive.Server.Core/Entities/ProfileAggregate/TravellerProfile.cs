using System;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities.ProfileAggregate
{
    public class TravellerProfile: Profile
    {
        [Required]
        public string DisplayName { get; set; }
        public string ProfileBio { get; set; }
        public string DisplayPicture { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
    }
}
