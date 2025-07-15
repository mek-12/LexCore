using LexHarvester.Infrastructure.Providers.Abstract;
using LexHarvester.Infrastructure.Providers.Request;
using Navend.Core.Step.Abstract;

namespace LexHarvester.Application.Steps.CaseLawDivisionSync;

public class CaseLawDivisionGetSourceStep(ICaseLawDivisionProvider caseLawDivisionProvider,
                                          CaseLawDivisionContext caseLawDivisionContext) : IStep<CaseLawDivisionContext>
{
    public int Order => 1;

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        caseLawDivisionContext.CaseLawTypes.ForEach(async lawType =>
        {
            var response = await caseLawDivisionProvider.SendAsync(new CaseLawDivisionRequest
            {
                Data = new DivisionCaseLawType
                {
                    ItemType = lawType
                }
            });
            if (response.Data != null && response.Data.Any())
                caseLawDivisionContext.CaseLawDivisionResponses.AddRange(response.Data);
        });
    }
}