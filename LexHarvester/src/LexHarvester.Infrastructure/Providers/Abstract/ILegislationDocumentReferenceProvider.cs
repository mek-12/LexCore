using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Response;

namespace LexHarvester.Infrastructure.Providers.Abstract;

public interface ILegislationDocumentReferenceProvider : IBaseCilent<LegislationDocumentReferenceRequest, LegislationDocumentReferenceResponse>
{ }