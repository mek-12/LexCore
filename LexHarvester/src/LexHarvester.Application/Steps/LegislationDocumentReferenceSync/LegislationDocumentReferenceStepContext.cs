using LexHarvester.Domain.Entities;
using Navend.Core.Step.Concrete;

namespace LexHarvester.Application.Steps.LegislationDocumentReferenceSync;

public class LegislationDocumentReferenceStepContext : StepContext
{
    public List<LegislationType> LegislationTypes { get; set; } = new List<LegislationType>();
    public List<HarvestingState> HarvestingStates { get; set; } = new List<HarvestingState>();
}
