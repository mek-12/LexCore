using LexHarvester.Infrastructure.Providers.Respose;
using Navend.Core.Step.Concrete;

namespace LexHarvester.Application.Steps.CaseLawDivisionSync;

public class CaseLawDivisionContext : StepContext
{
    public List<string> CaseLawTypes { get; set; } = new();
    public HashSet<(string UnitId, string ItemType)> DivisionsUnitIds { get; set; } = new();
    public List<CaseLawDivisionData> CaseLawDivisionResponses { get; set; } = new();
}