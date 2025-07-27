using System.Collections.Concurrent;
using LexHarvester.Domain.Entities;
using Navend.Core.Step.Concrete;

namespace LexHarvester.Application.Steps.CaseLawDocumentReferenceSync;

public class CaseLawDocumentReferenceContext : StepContext
{
    public List<CaseLawType> CaseLawTypes { get; set; } = new();
    public List<CaseLawDivision> CaseLawDivisions { get; set; } = new();
    public ConcurrentBag<HarvestingState> HarvestingStates { get; set; } = new();
    public ConcurrentBag<CaseLawDocumentReference> CaseLawDocumentReferences { get; set; } = new();
    public List<string> CaseLawDocumentIds { get; set; } = new();
}
    