using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Response;

namespace LexHarvester.Infrastructure.Providers.Concrete;

public class LegislationDocumentReferenceProvider(HttpClient httpClient) : BaseHttpProvider<LegislationDocumentReferenceRequest, LegislationDocumentReferenceResponse>(httpClient), ILegislationDocumentReferenceProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
    protected override string ContentType => base.ContentType;
}
