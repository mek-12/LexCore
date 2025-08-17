using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers.Concrete;

public class LegislationDocumentProvider(HttpClient httpClient) : BaseHttpProvider<LegislationDocumentRequest, LegislationDocumentResponse>(httpClient), ILegislationDocumentProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
}
