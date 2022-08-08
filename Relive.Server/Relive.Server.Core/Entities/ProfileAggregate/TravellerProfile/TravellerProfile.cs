using Relive.Server.Core.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities.ProfileAggregate
{
    public class TravellerProfile: Profile
    {
        [Required(ErrorMessage = "Required", AllowEmptyStrings = false)]
        [ReliveStringLength(Max = "50", Min = "2")]
        public string DisplayName { get; set; }

        [ReliveStringLength(Max = "150")]
        public string ProfileBio { get; set; }

        public string DisplayPicture { get; set; }
        
        [Required]
        public DateTime DateOfBirth { get; set; }
        
        public string Nationality { get; set; }
        
        [Required]
        public string Gender { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        [ReliveStringLength(Equal = "10")]
        public string MobileNumber { get; set; }
    }
}
