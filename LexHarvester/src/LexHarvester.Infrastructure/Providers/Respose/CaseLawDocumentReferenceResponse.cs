

using LexHarvester.Domain.DTOs;

namespace LexHarvester.Infrastructure.Providers.Respose;

public class CaseLawDocumentReferenceResponse
{
    public List<CaseLawDocumentReferenceDto> Data { get; set; } = new List<CaseLawDocumentReferenceDto>();
}
