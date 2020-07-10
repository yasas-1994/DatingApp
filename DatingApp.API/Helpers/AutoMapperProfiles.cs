using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        // we must create this kind of a class to make use of that auto mapper. here, this will
        // check Users(source) and created Dtos attributes and do mapping.
        // but this can't recognise photo url and age since they are different. in that case
        // we have to add some addtional configuration under ForMember method.
        public AutoMapperProfiles()
        {
            // User: source , UserForDetailedDto: destination
            CreateMap<User , UserForDetailedDto>()
            .ForMember(dest => dest.PhotoUrl, 
             opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
             .ForMember(dest => dest.Age,
              opt => opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            CreateMap<User, UserForListDto>()
            .ForMember(dest => dest.PhotoUrl, 
             opt => opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url))
              .ForMember(dest => dest.Age,
              opt => opt.MapFrom(src =>src.DateOfBirth.CalculateAge()));
            CreateMap<Photo, PhotoForDetailedDto>();
        }
    }
}