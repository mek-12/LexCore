using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Steps.CaseLawTypeSync;

public class CaseLawTypeFetchStep(CaseLawTypeContext context) : IStep<CaseLawTypeContext>
{
    public int Order => 1;

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
