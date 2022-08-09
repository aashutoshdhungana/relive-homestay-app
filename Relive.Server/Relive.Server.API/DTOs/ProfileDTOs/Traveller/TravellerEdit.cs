using Relive.Server.Core.ValidationAttributes;
using System;

namespace Relive.Server.API.DTOs.ProfileDTOs.Traveller
{
    public class TravellerEdit
    {
        public string DisplayName { get; set; }
        public string ProfileBio { get; set; }
        public string DisplayPicture { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        [ReliveStringLength(Equal = "10")]
        public string MobileNumber { get; set; }
    }
}
