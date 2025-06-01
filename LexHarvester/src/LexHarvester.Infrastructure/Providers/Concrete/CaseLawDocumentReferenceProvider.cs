using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers.Concrete;

public class CaseLawDocumentReferenceProvider(HttpClient httpClient) : BaseHttpProvider<CaseLawDocumentReferenceRequest, CaseLawDocumentReferenceResponse>(httpClient), ICaseLawDocumentReferenceProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
}
