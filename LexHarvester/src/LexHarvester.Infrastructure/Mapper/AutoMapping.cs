using AutoMapper;
using LexHarvester.Domain.DTOs;
using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<SyncConfiguration, SyncConfigurationDto>().ReverseMap();
        CreateMap<CaseLawDivisionResponse, CaseLawDivision>().ReverseMap();
    }
}
