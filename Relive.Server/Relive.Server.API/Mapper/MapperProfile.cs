using AutoMapper;
using Relive.Server.API.DTOs.UserDTOs;
using Relive.Server.Core.UserAggregate;

namespace Relive.Server.API.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<UserUpdate, User>().ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
