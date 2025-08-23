using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers.Concrete;

public class CaseLawDocumentProvider(HttpClient httpClient) : BaseHttpProvider<CaseLawDocumentRequest, CaseLawDocumentResponse>(httpClient), ICaseLawDocumentProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
    protected override bool UseStream => true;
}