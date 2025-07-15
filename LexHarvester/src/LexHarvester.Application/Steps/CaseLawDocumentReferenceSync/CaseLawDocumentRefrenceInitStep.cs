using Navend.Core.Step.Abstract;
using Navend.Core.UOW;

namespace LexHarvester.Application.Steps.CaseLawDocumentReferenceSync;

public class CaseLawDocumentRefrenceInitStep(IUnitOfWork unitOfWork,
                                             CaseLawDocumentReferenceContext caseLawDocumentReferenceContext) : IStep<CaseLawDocumentReferenceContext>
{
    public int Order => 0;

    public Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
