using LexHarvester.Infrastructure.Providers.Concrete;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers;

public class LegislationTypeProvider(HttpClient httpClient) : BaseHttpProvider<LegislationTypeRequest, LegislationTypeResponse>(httpClient), ILegislationTypeProvider
{
    protected override HttpMethod HttpMethod => HttpMethod.Post;
    protected override string ContentType => base.ContentType;
}

