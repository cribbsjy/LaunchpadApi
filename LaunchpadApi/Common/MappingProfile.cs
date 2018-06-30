using AutoMapper;
using Launchpad.Api.Models;
using Launchpad.Core.DTOs;

namespace Launchpad.Api.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SpaceXLaunchpadDto, LaunchpadDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Full_Name));
            CreateMap<LaunchpadDto, LaunchpadModel>()
                .ForMember(dest => dest.LaunchpadName, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.LaunchpadStatus, opts => opts.MapFrom(src => src.Status))
                .ForMember(dest => dest.LaunchpadId, opts => opts.MapFrom(src => src.Id));
            CreateMap<LaunchpadSearchRequest, SearchLaunchpadDto>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.LaunchpadName))
                .ForMember(dest => dest.Status, opts => opts.MapFrom(src => src.LaunchpadStatus));
        }
    }
}
