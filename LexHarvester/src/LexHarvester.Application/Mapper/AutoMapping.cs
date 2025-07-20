using AutoMapper;
using LexHarvester.Domain.DTOs;
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

        CreateMap<CaseLawDocumentReferenceDto, CaseLawDocumentReference>()
            .ForMember(dest => dest.ItemTypeName, opt => opt.MapFrom(src => src.ItemType != null ? src.ItemType.Name : null))
            .ForMember(dest => dest.ItemTypeDescription, opt => opt.MapFrom(src => src.ItemType != null ? src.ItemType.Description : null))
            .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UnitName))
            .ForMember(dest => dest.DecisionNumber, opt => opt.MapFrom(src => src.DecisionNumber))
            .ForMember(dest => dest.DecisionNumberSequence, opt => opt.MapFrom(src => src.DecisionNumberSequence))
            .ForMember(dest => dest.DecisionNumberYear, opt => opt.MapFrom(src => src.DecisionNumberYear))
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
            .ForMember(dest => dest.CaseNumberYear, opt => opt.MapFrom(src => src.CaseNumberYear))
            .ForMember(dest => dest.CaseNumberSequence, opt => opt.MapFrom(src => src.CaseNumberSequence))
            .ForMember(dest => dest.DecisionDate, opt => opt.MapFrom(src => src.DecisionDate))
            .ForMember(dest => dest.DecisionDateStr, opt => opt.MapFrom(src => src.DecisionDateStr))
            .ForMember(dest => dest.FinalizationStatus, opt => opt.MapFrom(src => src.FinalizationStatus))
            .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId ?? string.Empty))
            .ForMember(dest => dest.FilePath, opt => opt.Ignore())
            .ForMember(dest => dest.Downloaded, opt => opt.Ignore())
            .ForMember(dest => dest.Embedded, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<CaseLawDocumentReference, CaseLawDocumentReferenceDto>()
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => new ItemType
            {
                Name = src.ItemTypeName,
                Description = src.ItemTypeDescription
            }))
            .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.UnitName))
            .ForMember(dest => dest.DecisionNumber, opt => opt.MapFrom(src => src.DecisionNumber))
            .ForMember(dest => dest.DecisionNumberSequence, opt => opt.MapFrom(src => src.DecisionNumberSequence))
            .ForMember(dest => dest.DecisionNumberYear, opt => opt.MapFrom(src => src.DecisionNumberYear))
            .ForMember(dest => dest.CaseNumber, opt => opt.MapFrom(src => src.CaseNumber))
            .ForMember(dest => dest.CaseNumberYear, opt => opt.MapFrom(src => src.CaseNumberYear))
            .ForMember(dest => dest.CaseNumberSequence, opt => opt.MapFrom(src => src.CaseNumberSequence))
            .ForMember(dest => dest.DecisionDate, opt => opt.MapFrom(src => src.DecisionDate))
            .ForMember(dest => dest.DecisionDateStr, opt => opt.MapFrom(src => src.DecisionDateStr))
            .ForMember(dest => dest.FinalizationStatus, opt => opt.MapFrom(src => src.FinalizationStatus))
            .ForMember(dest => dest.DocumentId, opt => opt.MapFrom(src => src.DocumentId))
            .ForMember(dest => dest.UnitId, opt => opt.Ignore()); // UnitId mapping is unknown; skip or provide logic if needed
    }
    private static DateTime? ParseDateOrDefault(string? dateString)
    {
        return DateTime.TryParse(dateString, out var parsed)
            ? parsed
            : null;
    }
}