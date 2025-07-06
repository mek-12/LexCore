using LexHarvester.Domain.Entities;
using Navend.Core.Step.Concrete;

namespace LexHarvester.Application.Steps.LegislationTypeSync;

public class LegislationTypeStepContext : StepContext
{
    public List<LegislationType> LegislationTypes { get; set; } = new();
}