using System.Collections.Concurrent;
using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;

namespace LexHarvester.Application.Extensions;

public static class HarvestingStateExtension
{
    public static HarvestingState Get(this ConcurrentBag<HarvestingState> states, DocumentType documentType, string subType)
    {
        // Snapshot al: LINQ operasyonları için thread-safe hale gelir
        var snapshot = states.ToList();

        var state = snapshot.FirstOrDefault(h => h.DocumentType == documentType && h.SubType == subType);

        if (state is null)
        {
            state = new HarvestingState
            {
                DocumentType = documentType,
                SubType = subType,
                Count = 0,
                CurrentPage = 0,
                CreatedAt = DateTime.Now,
                IsCompleted = false,
                Synchronized = false,
                LastErrorMessage = string.Empty,
                LastUpdated = DateTime.Now
            };

            states.Add(state); // ConcurrentBag thread-safe olduğu için doğrudan ekleyebilirsin
        }

        return state;
    }
}
