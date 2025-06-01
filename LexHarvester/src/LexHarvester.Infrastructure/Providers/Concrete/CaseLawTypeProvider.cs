using LexHarvester.Infrastructure.Providers.Concrete;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers;

public class CaseLawTypeProvider(HttpClient httpClient) : BaseHttpProvider<CaseLawTypeRequest, CaseLawTypeResponse>(httpClient), ICaseLawTypeProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
    protected override string ContentType => base.ContentType;
}
