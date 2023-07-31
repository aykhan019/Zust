using AutoMapper;
using Zust.Entities.Models;
using Zust.Web.DTOs;
using Zust.Web.Entities;

namespace Zust.Web.Helpers.Mappers
{
    /// <summary>
    /// Class responsible for defining AutoMapper profiles.
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        /// <summary>
        /// Initializes a new instance of the AutoMapperProfiles class.
        /// </summary>
        public AutoMapperProfiles   ()
        {
            CreateMap<User, UserProfile>().ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}