using AutoMapper;
using Relive.Server.API.DTOs.UserDTOs;
using Relive.Server.Core.Entities;
using ProfileAggregate = Relive.Server.Core.Entities.ProfileAggregate;
using Relive.Server.Core.UserAggregate;
using Relive.Server.API.DTOs.ProfileDTOs.Traveller;

namespace Relive.Server.API.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<UserUpdate, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<TravellerDTO, ProfileAggregate.TravellerProfile>();
            CreateMap<TravellerEdit, ProfileAggregate.TravellerProfile>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<ProfileAggregate.TravellerProfile, TravellerDTO>();
        }
    }
}
