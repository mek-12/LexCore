using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers.Concrete;
/// <summary>
/// İçtihat birimleri
/// </summary>
public class CaseLawDivisionProvider(HttpClient httpClient) : BaseHttpProvider<CaseLawDivisionRequest, CaseLawDivisionResponse>(httpClient), ICaseLawDivisionProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
}
