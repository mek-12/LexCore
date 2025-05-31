using AutoMapper;
using LexHarvester.Domain.DTOs;
using LexHarvester.Domain.Entities;

namespace LexHarvester.Infrastructure.Utils;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<LegislationTypeDto, LegislationType>()
            .ReverseMap();
    }
}
