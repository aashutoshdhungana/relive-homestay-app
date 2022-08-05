using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relive.Server.Core.Entities.ProfileAggregate
{
    public class HomestayProfile: Profile
    {
        public string HomestayName { get; set; }
        public string HomestayDescription { get; set; }
        public string Address { get; set; }
        public List<string> Gallery { get; set; }
        public string DisplayPicture { get; set; }
        public string CoverPicture { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
    }
}
