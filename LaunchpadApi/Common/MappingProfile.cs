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
            CreateMap<LaunchpadDto, LaunchpadModel>();
            CreateMap<LaunchpadSearchRequest, SearchLaunchpadDto>();
        }
    }
}
