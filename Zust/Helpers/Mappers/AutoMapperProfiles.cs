using AutoMapper;
using Zust.Entities.Models;
using Zust.Web.Entities;

namespace Zust.Web.Helpers.Mappers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserProfile>().ReverseMap();
        }
    }
}
