using AutoMapper;
using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers.Request;

namespace LexHarvester.Application.Mapper;

public class LegislationMappingProfile : Profile
{
    public LegislationMappingProfile()
    {
        CreateMap<LegislationDto, LegislationDocumentReference>()
            .ForMember(dest => dest.LegislationType, opt => opt.MapFrom(src => src.LegislationType))
            .ForMember(dest => dest.LegislationTypeId, opt => opt.MapFrom(src => src.LegislationTypeId))
            .ForMember(dest => dest.FilePath, opt => opt.Ignore())
            .ForMember(dest => dest.Downloaded, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Embedded, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // DB tarafından atanabilir
    }
}