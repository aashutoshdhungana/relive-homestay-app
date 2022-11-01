using Relive.Server.Core.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Relive.Server.Core.Entities.ProfileAggregate
{
    public class HomestayProfile: Profile
    {
        [Required]
        [ReliveStringLength(Max = "50")]
        public string HomestayName { get; set; }
        
        [ReliveStringLength(Max = "150")]
        public string HomestayDescription { get; set; }

        [Required]
        [ReliveStringLength(Max = "100")]
        public string Address { get; set; }

        //[Required] <--------------------------Remove Comment After Implementing Map--------------------->
        public Tuple<decimal, decimal> GeoLocation { get; set; }
        public List<string> Gallery { get; set; }
        public string DisplayPicture { get; set; }
        public string CoverPicture { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
    }
}
