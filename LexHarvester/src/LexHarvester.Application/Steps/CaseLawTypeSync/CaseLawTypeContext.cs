using LexHarvester.Domain.Entities;
using Navend.Core.Step.Concrete;

namespace LexHarvester.Application.Steps.CaseLawTypeSync;

public class CaseLawTypeContext : StepContext
{
    internal List<CaseLawType> CaseLawTypes { get; set; } = new();
    internal List<HarvestingState> HarvestingStates { get; set; } = new();
}
