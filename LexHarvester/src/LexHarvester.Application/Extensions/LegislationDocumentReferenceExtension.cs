using LexHarvester.Domain.Entities;
using Navend.Core.Data;

namespace LexHarvester.Application.Extensions;

public static class LegislationDocumentReferenceExtension
{
    public static async Task<int> GetRemainingDownloadCount(this IRepository<LegislationDocumentReference, long> repository)
    {
        return await repository.GetCountAsync(l => !l.Downloaded);
    }
}
