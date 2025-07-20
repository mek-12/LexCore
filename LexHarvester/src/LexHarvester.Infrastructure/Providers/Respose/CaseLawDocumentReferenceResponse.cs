

using LexHarvester.Domain.DTOs;
using Newtonsoft.Json;

namespace LexHarvester.Infrastructure.Providers.Respose;

public class CaseLawDocumentReferenceResponse
{
    public CaseLawDocumentReferenceData? Data { get; set; } = new ();
}

public class CaseLawDocumentReferenceData
{
    [JsonProperty("emsalKararList")]
    public List<CaseLawDocumentReferenceDto> ListOfPrecedentDecisions { get; set; } = new();
    public int Total { get; set; }
    public int Start { get; set; }
}