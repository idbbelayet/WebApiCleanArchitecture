using Application.Commands;
using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Tests
{
    public static class TestHelper
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateRegionCommand, Region>();
                cfg.CreateMap<UpdateRegionCommand, Region>();
                cfg.CreateMap<Region, RegionDto>();
            });
            return config.CreateMapper();
        }
    }
}
