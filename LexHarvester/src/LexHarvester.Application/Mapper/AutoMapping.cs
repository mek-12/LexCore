using AutoMapper;
using LexHarvester.Domain.Entities;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Application.Mapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<LegislationDto, LegislationDocumentReference>()
            .ForMember(dest => dest.LegislationType, opt => opt.MapFrom(src => src.LegislationType))
            .ForMember(dest => dest.LegislationTypeId, opt => opt.MapFrom(src => src.LegislationTypeId))
            .ForMember(dest => dest.FilePath, opt => opt.Ignore())
            .ForMember(dest => dest.Downloaded, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.Embedded, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src =>
                ParseDateOrDefault(src.RegistrationDate)
            ))
            .ForMember(dest => dest.OfficialGazetteDate, opt => opt.MapFrom(src =>
                ParseDateOrDefault(src.OfficialGazetteDate)
            ))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // DB tarafından atanabilir
        CreateMap<CaseLawDivision, CaseLawDivisionData>().ReverseMap();
    }
    private static DateTime? ParseDateOrDefault(string? dateString)
    {
        return DateTime.TryParse(dateString, out var parsed)
            ? parsed
            : null;
    }
}