using Relive.Server.Core.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.API.DTOs.ProfileDTOs.Traveller
{
    public class TravellerDTO
    {
        [Required]
        public string DisplayName { get; set; }
        public string ProfileBio { get; set; }
        public string DisplayPicture { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [ReliveStringLength(Equal = "10")]
        public string MobileNumber { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
