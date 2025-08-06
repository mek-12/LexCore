using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Steps.LexDocumentSync;

public class LexDocumentInitStep : IStep<LegislationDocumentStepContext>
{
    public int Order => 0;

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {

    }
}
