using Application.Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class RegionMappingProfile : Profile
    {
        public RegionMappingProfile()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<CreateRegionCommand, Region>();
            CreateMap<UpdateRegionCommand, Region>();
        }
    }
}
