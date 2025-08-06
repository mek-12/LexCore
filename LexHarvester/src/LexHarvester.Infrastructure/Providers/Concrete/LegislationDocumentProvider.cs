using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using LexHarvester.Infrastructure.Providers.Respose;

namespace LexHarvester.Infrastructure.Providers.Concrete;

public class LegislationDocumentProvider : ILegislationDocumentProvider
{
    public Task<LegislationDocumentResponse> SendAsync(LegislationDocumentRequest request)
    {
        throw new NotImplementedException();
    }
}
