using LexHarvester.Domain.Entities;
using LexHarvester.Domain.Enums;

namespace LexHarvester.Application.Extensions;

public static class HarvestingStateExtension
{
    public static HarvestingState Get(this List<HarvestingState> states, DocumentType documentType, string subType)
    {
        var state = states.Where(h => h.DocumentType == documentType && h.SubType == subType)?.FirstOrDefault();
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
            states.Add(state);
        }
        return state;
    }
}
